
using LancheTCE.Context;
using LancheTCE_Back.Repositories.Produtos;

namespace LancheTCE_Back.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository? _produtoRepo;

    public AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            //if (_produtoRepo == null)
            //{
            //    _produtoRepo = new ProdutoRepository(_context);
            //}
            //return _produtoRepo;
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

        public void Dispose()
    {
        _context.Dispose();
    }
}
