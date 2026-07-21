using System.ComponentModel.DataAnnotations;

namespace TarijaReadApp.Models;

public class Ejemplar
{
    public int Id {get; set;}

    [Required, StringLength(30)]
    public string Codigo {get; set;} = string.Empty;

    [Required]
    public EstadoEjemplar Estado {get; set;} = EstadoEjemplar.Disponible;

    public int LibroId {get; set;}
    public virtual Libro Libro {get; set;} = null!;
    public virtual ICollection<Prestamo> Prestamos {get; set;} = new HashSet<Prestamo>();
}