using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.DTOs;

public class ProductDTOUpdateResponse
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageURL { get; set; }
    public float Stock { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CategoryId { get; set; }
}
