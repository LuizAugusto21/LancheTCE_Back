
using LancheTCE_Back.DTOs.ProdutoDTO;
using LancheTCE_Back.models;
using AutoMapper;
namespace LancheTCE_Back.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
    }
}
