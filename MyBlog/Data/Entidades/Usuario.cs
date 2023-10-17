using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Data.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string NombreUsuario { get; set; } = null!;

        public string? URLFotoPerfil { get; set; }

        public string Correo { get; set; } = null!;

        public string Clave { get; set; } = null!;
    }
}
