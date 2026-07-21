using System.ComponentModel.DataAnnotations;

namespace TarijaReadApp.Models;

public class Categoria
{
    public int Id {get; set;}
    [Required, StringLength(50)]
    public string Nombre {get; set;} = string.Empty;

    public virtual ICollection<Libro> Libros {get; set;} = new HashSet<Libro>();
}