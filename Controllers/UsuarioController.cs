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
      var usuarios = _uof.UsuarioRepository.GetAll();

      if (usuarios == null)
        return NotFound();

      var usuariosDto = _mapper.Map<IEnumerable<UsuarioGETDTO>>(usuarios);

      return Ok(usuariosDto);
    }

    [HttpGet("{id}", Name = "ObterUsuario")]
    public ActionResult<UsuarioGETDTO> Get(int id)
    {
      var usuario = _uof.UsuarioRepository.Get(u => u.UsuarioId == id);

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

      var novoUsuarioDto = _mapper.Map<UsuarioDTO>(novoUsuario);

      return new CreatedAtRouteResult("ObterUsuario",
          new { id = novoUsuarioDto.UsuarioId }, novoUsuarioDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<UsuarioDTO> Put(int id, UsuarioDTO usuarioDto)
    {
      if (id != usuarioDto.UsuarioId)
        return BadRequest();

      var usuario = _mapper.Map<Usuario>(usuarioDto);

      var usuarioAtualizado = _uof.UsuarioRepository.Update(usuario);
      _uof.Commit();

      var usuarioAtualizadoDto = _mapper.Map<UsuarioDTO>(usuarioAtualizado);

      return Ok(usuarioAtualizadoDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<UsuarioDTO> Delete(int id)
    {
      var usuario = _uof.UsuarioRepository.Get(u => u.UsuarioId == id);

      if (usuario == null)
        return NotFound("Usuário não encontrado...");

      var usuarioDeletado = _uof.UsuarioRepository.Delete(usuario);
      _uof.Commit();

      var usuarioDeletadoDto = _mapper.Map<UsuarioDTO>(usuarioDeletado);

      return Ok(usuarioDeletadoDto);
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<UsuarioDTO>> Get([FromQuery] UserParameters userParameters)
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

      var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

      return Ok(usuariosDTO);
    }

    [HttpGet("filter/nome/pagination")]
    public ActionResult<IEnumerable<UsuarioDTO>> GetUsuariosFilter([FromQuery] UsuarioFiltroParameters usuarioFiltroParameters)
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

      var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

      return Ok(usuariosDTO);
    }
  }
}
