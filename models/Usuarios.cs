using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
    [Table("Usuarios")]
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string? Email { get; set; }

        [Required]
        [StringLength(20)]
        public string? Senha { get; set; }

        [Required]
        [StringLength(20)]
        public string? Perfil { get; set; }
    }
}