using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories;

public interface IPedidoRepository : IRepository<Pedido>
{
  PagedList<Pedido> GetPedidos(PedidoParameters pedidoParameters);
}
