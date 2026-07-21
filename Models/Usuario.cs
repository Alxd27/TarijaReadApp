// Models/Usuario.cs
using System.ComponentModel.DataAnnotations;

namespace TarijaReadApp.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public RolUsuario Rol { get; set; } = RolUsuario.OperadorDeAtencion;

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new HashSet<Prestamo>();
}