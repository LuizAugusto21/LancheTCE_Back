using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LancheTCE.Context;
using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories.Produtos
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        // public object GetProdutos(ProdutoParameters produtosParameters)
        // {
        //     return GetAll().OrderBy(p => p.Nome)
        //     .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
        //     .Take(produtosParameters.PageSize).ToList();
        // }

        // public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        // {
        //     return GetAll().Where(c => c.CategoriaId == id);
        // }
    }
}