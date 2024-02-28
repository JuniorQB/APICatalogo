using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class CategoryDTO
{
    public int CategoryId { get; set; }
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImageURL { get; set; }
}
