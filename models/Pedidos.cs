using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
    [Table("Pedidos")]
    public class Pedidos
    {
        [Key]
        public int PedidoId { get; set; }

        [Required]
        [StringLength(20)]
        public string? Status { get; set; }
    }
}