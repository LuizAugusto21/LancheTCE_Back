using LancheTCE_Back.models;
using AutoMapper;
using LancheTCE_Back.DTOs.PedidoDTO;

namespace LancheTCE_Back.Mapping;

public class PedidoDTOMappingProfile : Profile
{
  public PedidoDTOMappingProfile()
  {
    CreateMap<Pedido, PedidoDTO>().ReverseMap();
  }
}
