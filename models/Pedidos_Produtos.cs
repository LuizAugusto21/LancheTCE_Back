using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
    [Table("Pedidos_Produtos")]
    public class Pedidos_Produtos
    {
        [Required]
        public int Quantidade { get; set; }
    }
}