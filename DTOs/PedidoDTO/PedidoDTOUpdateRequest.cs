using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LancheTCE_Back.DTOs.ProdutoDTO
{
    public class PedidoDTOUpdateRequest
    {
        public int Preco {get; set;}
        public int Quantidade{get; set;}
    }
}