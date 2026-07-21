// Models/Multa.cs
using System.ComponentModel.DataAnnotations;

namespace TarijaReadApp.Models;

public class Multa
{
    public int Id { get; set; }

    [Range(0, 99999)]
    public decimal Monto { get; set; } // ODS 8 -> decimal para dinero

    [Required]
    public bool PagoRealizado { get; set; } = false;

    // FK 1:1 con Prestamo
    public int PrestamoId { get; set; }
    public virtual Prestamo Prestamo { get; set; } = null!;
}