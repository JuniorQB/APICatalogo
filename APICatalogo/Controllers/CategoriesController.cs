using Microsoft.AspNetCore.Mvc;
using APICatalogo.Models;
using APICatalogo.Filters;
using APICatalogo.Repositories;

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
    public ActionResult<IEnumerable<Category>> Get()
    {
       var categories =  _repository.CategorieRepository.GetAll();

        return Ok(categories);
        
    }

    [HttpGet("{id:int}", Name="GetCategory")]
    public ActionResult<Category> Get(int id)
    {
        var category = _repository.CategorieRepository.Get(c=> c.CategoryId == id);

        if (category == null)
        {
            return NotFound("Category not found");
        }
        return Ok(category);
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        if (category is null)
            return BadRequest();

        var createdCategory = _repository.CategorieRepository.Create(category);
        _repository.Commit();

        return new CreatedAtRouteResult("GetCategory",
            new { id = createdCategory.CategoryId }, createdCategory);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }
        _repository.CategorieRepository.Update(category);
        _repository.Commit();
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var category = _repository.CategorieRepository.Get(c => c.CategoryId == id);

        if (category == null)
        {
            return NotFound("\"Category not found.");
        }
       var deletedCategory = _repository.CategorieRepository.Delete(category);
        _repository.Commit();
        return Ok(category);
    }
}
