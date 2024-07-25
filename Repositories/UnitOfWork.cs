using APICatalogo.Repositories;
using LancheTCE_Back.Repositories.Produtos;

namespace LancheTCE_Back.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository? _produtoRepo;
        private IUsuarioRepository? _usuarioRepo;
        private IPedidoRepository? _pedidoRepo;
        private IPedidoProdutoRepository? _pedidoProdutoRepo;
        private IEnderecoRepository? _enderecoRepo;

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            }
        }

        public IUsuarioRepository UsuarioRepository
        {
            get
            {
                return _usuarioRepo = _usuarioRepo ?? new UsuarioRepository(_context);
            }
        }

        public IPedidoRepository PedidoRepository
        {
            get
            {
                return _pedidoRepo = _pedidoRepo ?? new PedidoRepository(_context);
            }
        }

        public IProdutoPedidoRepository ProdutoPedidoRepository
        {
            get
            {
                return _pedidoProdutoRepo = _pedidoProdutoRepo ?? new ProdutoPedidoRepository(_context);
            }
        }

        public IEnderecoRepository EnderecoRepository
        {
            get
            {
                return _enderecoRepo = _enderecoRepo ?? new EnderecoRepository(_context);
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

    internal interface IPedidoProdutoRepository
    {
    }
}
