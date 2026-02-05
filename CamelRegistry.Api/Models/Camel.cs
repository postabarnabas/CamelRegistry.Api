using System.ComponentModel.DataAnnotations;

namespace CamelRegistry.Api.Models;

public class Camel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Color { get; set; }

    [Range(1, 2, ErrorMessage = "HumpCount csak 1 vagy 2 lehet.")]
    public int HumpCount { get; set; }

    public DateTime? LastFed { get; set; }
}
