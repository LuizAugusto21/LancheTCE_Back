using LancheTCE.Context;
using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
  public UsuarioRepository(AppDbContext context) : base(context)
  {
  }
}