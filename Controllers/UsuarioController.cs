using LancheTCE_Back.models;
using LancheTCE_Back.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LancheTCE_Back.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public UsuarioController(IUnitOfWork uof, IMapper mapper)
    {
      _uof = uof;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UsuarioGETDTO>> Get()
    {
      //var usuarios = _uof.UsuarioRepository.GetAll();
      var usuarios = _uof.UsuarioRepository.GetUsuarios(new UserParameters());

      if (usuarios == null)
        return NotFound();

      var usuariosDto = _mapper.Map<IEnumerable<UsuarioGETDTO>>(usuarios);

      return Ok(usuariosDto);
    }

    [HttpGet("{id}", Name = "ObterUsuario")]
    public ActionResult<UsuarioGETDTO> Get(int id)
    {
      var usuario = _uof.UsuarioRepository.GetUsuarioComEndereco(id);

      if (usuario == null)
        return NotFound("Usuário não encontrado...");

      var usuarioDto = _mapper.Map<UsuarioGETDTO>(usuario);

      return Ok(usuarioDto);
    }

    [HttpPost]
    public ActionResult<UsuarioGETDTO> Post(UsuarioPOSTDTO usuarioDto)
    {
      if (usuarioDto == null)
        return BadRequest();

      var usuario = _mapper.Map<Usuario>(usuarioDto);

      var novoUsuario = _uof.UsuarioRepository.Create(usuario);
      _uof.Commit();

      var novoUsuarioDto = _mapper.Map<UsuarioGETDTO>(novoUsuario);

      return new CreatedAtRouteResult("ObterUsuario",
          new { id = novoUsuarioDto.UsuarioId }, novoUsuarioDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<UsuarioGETDTO> Put(int id, UsuarioPUTDTO usuarioDto)
    {
      if (id != usuarioDto.UsuarioId)
        return BadRequest();

      var usuario = _uof.UsuarioRepository.GetUsuarioComEndereco(id);

      if (usuario == null)
        return NotFound("Usuário não encontrado...");

      // Atualize o usuário
      usuario.Nome = usuarioDto.Nome;
      usuario.Email = usuarioDto.Email;
      usuario.Senha = usuarioDto.Senha;
      usuario.Perfil = usuarioDto.Perfil;

      // Verifique se o endereço é nulo
      if (usuarioDto.Endereco == null)
      {
        return BadRequest("O endereço não pode ser nulo ao atualizar um usuário.");
      }

      if (usuario.Endereco.EnderecoId == usuarioDto.Endereco.EnderecoId)
      {
        // Atualize o endereço existente do usuário
        usuario.Endereco.Andar = usuarioDto.Endereco.Andar;
        usuario.Endereco.Sala = usuarioDto.Endereco.Sala;
        usuario.Endereco.Departamento = usuarioDto.Endereco.Departamento;
      }
      else if (usuario.Endereco == null)
      {
        // Verifique se o endereço já existe
        var enderecoExistente = _uof.EnderecoRepository.Get(e => e.EnderecoId == usuarioDto.Endereco.EnderecoId);
        if (enderecoExistente != null)
        {
          return BadRequest("Não é possível atualizar um endereço que já pertence a outro usuário.");
        }

        // Crie ou atualize o endereço
        var endereco = _mapper.Map<Endereco>(usuarioDto.Endereco);
        var enderecoAtualizado = _uof.UsuarioRepository.CreateOrUpdateEndereco(endereco);
        usuario.Endereco = enderecoAtualizado;
      }
      else
      {
        return BadRequest("Não é possível criar um endereço pois já existe um endereço cadastrado para este usuário.");
      }

      // Atualize o usuário no repositório
      var usuarioAtualizado = _uof.UsuarioRepository.Update(usuario);
      _uof.Commit();

      var usuarioAtualizadoDto = _mapper.Map<UsuarioGETDTO>(usuarioAtualizado);

      return Ok(usuarioAtualizadoDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<UsuarioGETDTO> Delete(int id)
    {
      var usuario = _uof.UsuarioRepository.Get(u => u.UsuarioId == id);

      if (usuario == null)
        return NotFound("Usuário não encontrado...");

      var usuarioDeletado = _uof.UsuarioRepository.Delete(usuario);
      _uof.Commit();

      var usuarioDeletadoDto = _mapper.Map<UsuarioGETDTO>(usuarioDeletado);

      return Ok(usuarioDeletadoDto);
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<UsuarioGETDTO>> Get([FromQuery] UserParameters userParameters)
    {
      var usuarios = _uof.UsuarioRepository.GetUsuarios(userParameters);

      var metadata = new
      {
        usuarios.TotalCount,
        usuarios.PageSize,
        usuarios.CurrentPage,
        usuarios.TotalPages,
        usuarios.HasNext,
        usuarios.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

      var usuariosDTO = _mapper.Map<IEnumerable<UsuarioGETDTO>>(usuarios);

      return Ok(usuariosDTO);
    }

    [HttpGet("filter/nome/pagination")]
    public ActionResult<IEnumerable<UsuarioGETDTO>> GetUsuariosFilter([FromQuery] UsuarioFiltroParameters usuarioFiltroParameters)
    {
      var usuarios = _uof.UsuarioRepository.GetUsuariosFiltro(usuarioFiltroParameters);

      var metadata = new
      {
        usuarios.TotalCount,
        usuarios.PageSize,
        usuarios.CurrentPage,
        usuarios.TotalPages,
        usuarios.HasNext,
        usuarios.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

      var usuariosDTO = _mapper.Map<IEnumerable<UsuarioGETDTO>>(usuarios);

      return Ok(usuariosDTO);
    }
  }
}
