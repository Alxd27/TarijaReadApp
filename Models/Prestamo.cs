using System.ComponentModel.DataAnnotations;

namespace TarijaReadApp.Models;

public class Prestamo
{
    public int Id {get; set;}

    [Required]
    public DateTime FechaSalida {get; set;} = DateTime.Now;

    [Required]
    public DateTime FechaLimite {get; set;}

    public DateTime? FechaDevolucion {get; set;}
    
    public int EjemplarId {get; set;}
    public Ejemplar Ejemplar {get; set;} = null!;

    public int SocioId { get; set; }
    public virtual Socio Socio { get; set; } = null!;

    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Multa? Multa { get; set; }
}