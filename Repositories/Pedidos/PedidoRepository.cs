using LancheTCE.Context;
using LancheTCE_Back.models;
using Microsoft.EntityFrameworkCore;

namespace LancheTCE_Back.Repositories;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
  public PedidoRepository(AppDbContext context) : base(context)
  {
  }

  public PagedList<Pedido> GetPedidos(PedidoParameters pedidoParameters)
  {
      var query = _context.Pedidos.Include(s => s.Status).AsQueryable();

      if (!string.IsNullOrWhiteSpace(pedidoParameters.Status)){
        query = query.Where(p => p.Status.Contains(pedidoParameters.Status));
      }

      return PagedList<Pedido>.ToPagedList(query, pedidoParameters.PageNumber, pedidoParameters.PageSize);
  }
}