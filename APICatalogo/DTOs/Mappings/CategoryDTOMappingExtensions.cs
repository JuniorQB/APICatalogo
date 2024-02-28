using APICatalogo.Models;
using System.Xml.Linq;

namespace APICatalogo.DTOs.Mappings;

public static class CategoryDTOMappingExtensions
{
    public static CategoryDTO? ToCategoryDTO(this CategoryDTO category)
    {
        if (category is null)
        {
            return null;
        }
        return new CategoryDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            ImageURL = category.ImageURL
        };
    }

    public static Category? ToCategory(this CategoryDTO categoryDTO)
    {
        if (categoryDTO is null) { return null;  }

        return new Category()
        {
            CategoryId = categoryDTO.CategoryId,
            Name = categoryDTO.Name,
            ImageURL = categoryDTO.ImageURL
        };
    }

    public static IEnumerable<CategoryDTO> ToCategoryDtoList(this IEnumerable<Category> categories)
    {
        if (categories is null || !categories.Any()) { return new List<CategoryDTO>(); }

        return categories.Select(category => new CategoryDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            ImageURL = category.ImageURL,
        }).ToList();


    
    }
}
