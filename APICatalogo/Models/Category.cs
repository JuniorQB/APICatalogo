using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImageURL { get; set; }

    [JsonIgnore]
    public ICollection<Product> Products { get; set; }

    public Category()
    {
        Products = new Collection<Product>();
    }
}
