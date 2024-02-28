using Microsoft.AspNetCore.Mvc;
using APICatalogo.Models;
using APICatalogo.Filters;
using APICatalogo.Repositories;
using APICatalogo.DTOs;

namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _repository;

    private readonly ILogger _logger;

    public CategoriesController(IUnitOfWork repository, ILogger<CategoriesController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("products")]
    public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
    {
        _logger.LogInformation("======================= GETCATEGORIESPRODUCTS==================");

        return Ok(_repository.CategorieRepository.GetCategoriesWithProducts());
    }

   
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<CategoryDTO>> Get()
    {
       var categories =  _repository.CategorieRepository.GetAll();

        if(categories is null)
        {
            return NotFound();

        }

        var categoriesDto = new List<CategoryDTO>();
        foreach (var category in categories)
        {
            var categoryDTO = new CategoryDTO()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ImageURL = category.ImageURL
            };

            categoriesDto.Add(categoryDTO);
        }

        return Ok(categoriesDto);
        
    }

    [HttpGet("{id:int}", Name="GetCategory")]
    public ActionResult<CategoryDTO> Get(int id)
    {
        var category = _repository.CategorieRepository.Get(c=> c.CategoryId == id);

        if (category == null)
        {
            return NotFound("Category not found");
        }

        var categoryDTO = new CategoryDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            ImageURL = category.ImageURL
        };

        return Ok(categoryDTO);
    }

    [HttpPost]
    public ActionResult<CategoryDTO> Post(CategoryDTO categoryDTO)
    {
        if (categoryDTO is null)
            return BadRequest();

        var category = new Category()
        {
            CategoryId = categoryDTO.CategoryId,
            Name = categoryDTO.Name,
            ImageURL = categoryDTO.ImageURL
        };

        var createdCategory = _repository.CategorieRepository.Create(category);
        _repository.Commit();

        var newcategoryDTO = new CategoryDTO()
        {
            CategoryId = createdCategory.CategoryId,
            Name = createdCategory.Name,
            ImageURL = createdCategory.ImageURL
        };



        return new CreatedAtRouteResult("GetCategory",
            new { id = newcategoryDTO.CategoryId }, newcategoryDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.CategoryId)
        {
            return BadRequest();
        }

        var category = new Category()
        {
            CategoryId = categoryDTO.CategoryId,
            Name = categoryDTO.Name,
            ImageURL = categoryDTO.ImageURL
        };

        _repository.CategorieRepository.Update(category);
        _repository.Commit();
        var newcategoryDTO = new CategoryDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            ImageURL = category.ImageURL
        };
        return Ok(newcategoryDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoryDTO> Delete(int id)
    {
        var category = _repository.CategorieRepository.Get(c => c.CategoryId == id);

        if (category == null)
        {
            return NotFound("\"Category not found.");
        }
       var deletedCategory = _repository.CategorieRepository.Delete(category);
        _repository.Commit();
        var deletedCategoryDTO = new CategoryDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            ImageURL = category.ImageURL
        };
        return Ok(deletedCategoryDTO);
    }
}
