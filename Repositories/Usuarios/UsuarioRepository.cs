using LancheTCE.Context;
using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories
{
  public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
  {
    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Usuario> GetUsuarios(UserParameters userParameters)
    {
      var query = _context.Usuarios.AsQueryable();

      if (!string.IsNullOrWhiteSpace(userParameters.Nome))
      {
        query = query.Where(u => u.Nome.Contains(userParameters.Nome));
      }

      if (!string.IsNullOrWhiteSpace(userParameters.Email))
      {
        query = query.Where(u => u.Email.Contains(userParameters.Email));
      }

      return PagedList<Usuario>.ToPagedList(query,
          userParameters.PageNumber,
          userParameters.PageSize);
    }

    public PagedList<Usuario> GetUsuariosFiltro(UsuarioFiltroParameters usuarioFiltroParameters)
    {
      var query = _context.Usuarios.AsQueryable();

      if (!string.IsNullOrWhiteSpace(usuarioFiltroParameters.Nome))
      {
        query = query.Where(u => u.Nome.Contains(usuarioFiltroParameters.Nome));
      }

      if (!string.IsNullOrWhiteSpace(usuarioFiltroParameters.Email))
      {
        query = query.Where(u => u.Email.Contains(usuarioFiltroParameters.Email));
      }

      return PagedList<Usuario>.ToPagedList(query,
          usuarioFiltroParameters.PageNumber,
          usuarioFiltroParameters.PageSize);
    }
  }
}
