using APICatalogo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImageURL { get; set; }
   

    public int CategoryId { get; set; }

   
}
