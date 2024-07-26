
using LancheTCE_Back.DTOs;
using LancheTCE_Back.models;
using AutoMapper;

namespace LancheTCE_Back.Mapping;

public class UsuarioDTOMappingProfile : Profile
{
  public UsuarioDTOMappingProfile()
  {

    CreateMap<Usuario, UsuarioDTO>().ReverseMap();
    CreateMap<Usuario, UsuarioGETDTO>().ReverseMap();
    CreateMap<Usuario, UsuarioPOSTDTO>().ReverseMap();
  }
}
