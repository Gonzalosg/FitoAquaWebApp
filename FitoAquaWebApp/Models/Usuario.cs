using System.ComponentModel.DataAnnotations;

namespace FitoAquaWebApp.Models
{

    public enum RolUsuario
    {
        Administrador,
        Empleado,
        Cliente
    }

    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ContraseniaHash { get; set; }

        [Required]
        public RolUsuario Rol { get; set; }

        public ICollection<Obra>? ObrasCliente { get; set; } // Si es cliente
        public ICollection<Albaran>? Albaranes { get; set; } // Si es empleado
        public ICollection<ParteAveria>? PartesAveria { get; set; }
    }
}
