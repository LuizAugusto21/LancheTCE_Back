using LancheTCE.Context;
using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
  public PedidoRepository(AppDbContext context) : base(context)
  {
  }
}