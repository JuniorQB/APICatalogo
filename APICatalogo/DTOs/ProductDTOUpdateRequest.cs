using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class ProductDTOUpdateRequest : IValidatableObject
{
    [Range(1,9999, ErrorMessage ="The stock must be bettween 1 and 9999")]
    public float Stock { get; set; }
    public DateTime CreatedAt { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(CreatedAt.Date <= DateTime.Now.Date)
        {
            yield return new ValidationResult("The date must be greater than actual date",
            new[] { nameof(this.CreatedAt)});
        }
    }
}
