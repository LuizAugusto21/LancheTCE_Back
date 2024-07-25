using LancheTCE.Context;
using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories;

public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
{
  public EnderecoRepository(AppDbContext context) : base(context)
  {
  }
}