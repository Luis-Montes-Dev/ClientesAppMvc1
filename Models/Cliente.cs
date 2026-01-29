

using System.ComponentModel.DataAnnotations;

namespace ClientesAppMvc.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Nombre { get; set; }
    [Required, MaxLength(100)]
    [EmailAddress]
    public string Correo { get; set; }

    public string? Telefono { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public bool Activo { get; set; }
}
