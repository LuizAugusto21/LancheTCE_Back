

using LancheTCE_Back.Repositories.Produtos;

namespace LancheTCE_Back.Repositories;

public interface IUnitOfWork
{
    IProdutoRepository ProdutoRepository { get; }

    void Commit();
}
