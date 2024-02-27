using Microsoft.AspNetCore.Mvc;
using APICatalogo.Models;
using APICatalogo.Filters;
using APICatalogo.Repositories;

namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IRepository<Category> _repository;
    private readonly ICategorieRepository _categoryRepository;
    private readonly ILogger _logger;

    public CategoriesController(IRepository<Category> repository, ICategorieRepository categoryRepository, ILogger<CategoriesController> logger)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    [HttpGet("products")]
    public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
    {
        _logger.LogInformation("======================= GETCATEGORIESPRODUCTS==================");

        return Ok(_categoryRepository.GetCategoriesWithProducts());
    }

   
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Category>> Get()
    {
       var categories =  _repository.GetAll();

        return Ok(categories);
        
    }

    [HttpGet("{id:int}", Name="GetCategory")]
    public ActionResult<Category> Get(int id)
    {
        var category = _repository.Get(c=> c.CategoryId == id);

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

        var createdCategory = _repository.Create(category);

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
       _repository.Update(category);
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var category = _repository.Get(c => c.CategoryId == id);

        if (category == null)
        {
            return NotFound("\"Category not found.");
        }
       var deletedCategory = _repository.Delete(category);
        return Ok(category);
    }
}
