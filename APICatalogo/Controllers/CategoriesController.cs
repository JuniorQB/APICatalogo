using Microsoft.AspNetCore.Mvc;
using APICatalogo.Models;
using APICatalogo.Filters;
using APICatalogo.Repositories;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;

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

        var categoriesDto = categories.ToCategoryDtoList();
      

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

        var categoryDTO = category.ToCategoryDTO();
        return Ok(categoryDTO);
    }

    [HttpPost]
    public ActionResult<CategoryDTO> Post(CategoryDTO categoryDTO)
    {
        if (categoryDTO is null)
            return BadRequest();

        var category = categoryDTO.ToCategory();

        var createdCategory = _repository.CategorieRepository.Create(category);
        _repository.Commit();

        var newcategoryDTO = category.ToCategoryDTO();



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

        var category = categoryDTO.ToCategory();

        _repository.CategorieRepository.Update(category);
        _repository.Commit();
        var newcategoryDTO = category.ToCategoryDTO();

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
        var deletedCategoryDTO = category.ToCategoryDTO();

        return Ok(deletedCategoryDTO);
    }
}
