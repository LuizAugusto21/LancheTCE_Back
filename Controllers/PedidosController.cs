using AutoMapper;
using LancheTCE_Back.DTOs.PedidoDTO;
using LancheTCE_Back.DTOs.ProdutoDTO;
using LancheTCE_Back.models;
using LancheTCE_Back.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LancheTCE_Back.Controllers;

[Route("[controller]")]
[ApiController]
public class PedidosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public PedidosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof= uof;
        _mapper = mapper;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Pedido>> Get(){
        var pedidos = _uof.PedidoRepository.GetAll();
        var pedidosDto = _mapper.Map<IEnumerable<Pedido>>(pedidos);
        return Ok(pedidosDto);
    }

    [HttpGet("{id}", Name = "ObterPedido")]
    public ActionResult<PedidoDTO> Get(int id)
    {
        var pedido = _uof.PedidoRepository.Get(p => p.PedidoId == id);
        if(pedido is null)
            return NotFound("Pedido não encontrado...");
        
        var pedidoDTO = _mapper.Map<PedidoDTO>(pedido);
        return Ok(pedidoDTO);
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<PedidoDTO>> Get([FromQuery] PedidoParameters pedidoParameters)
    {
        var pedidos = _uof.PedidoRepository.GetPedidos(pedidoParameters);

        var metadata = new{
            pedidos.TotalCount,
            pedidos.PageSize,
            pedidos.CurrentPage,
            pedidos.TotalPages,
            pedidos.HasNext,
            pedidos.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        var pedidosDTO = _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);

        return Ok(pedidosDTO);
    }

    [HttpPost]
    public ActionResult<PedidoDTO> Post(PedidoDTO pedidoDTO)
    {
        if (pedidoDTO is null)
            return BadRequest();
        
        var pedido = _mapper.Map<Pedido>(pedidoDTO);

        var novoPedido = _uof.PedidoRepository.Create(pedido);
        _uof.Commit();

        var novoPedidoDTO = _mapper.Map<PedidoDTO>(novoPedido);

        return new CreatedAtRouteResult("ObterPedido",
            new {id = novoPedidoDTO.PedidoId}, novoPedidoDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<PedidoDTO> Put(int id, PedidoDTO pedidoDTO)
    {
        if (id != pedidoDTO.PedidoId)
            return BadRequest();
        var pedido = _mapper.Map<Pedido>(pedidoDTO);
        var pedidoAtualizado = _uof.PedidoRepository.Update(pedido);
        _uof.Commit();

        var pedidoAtualizadoDTO = _mapper.Map<PedidoDTO>(pedidoAtualizado);

        return Ok(pedidoAtualizadoDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<PedidoDTO> Delete(int id)
    {
        var pedido = _uof.PedidoRepository.Get(p => p.PedidoId == id);
        if (pedido is null)
            return NotFound("Pedido não encontrado...");
        
        var pedidoDeletado = _uof.PedidoRepository.Delete(pedido);
        _uof.Commit();
        
        var pedidoDeletadoDTO = _mapper.Map<PedidoDTO>(pedidoDeletado);

        return Ok(pedidoDeletadoDTO);
    }
}