using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Filters;

namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("products")]
    public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
    {
        return _context.Categories.Include(p=>p.Products).ToList();
    }

   
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Category>> Get()
    {
        try
        {
            return _context.Categories.AsNoTracking().ToList();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error mesage");
        }
        
    }

    [HttpGet("{id:int}", Name="GetCategory")]
    public ActionResult<Category> Get(int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

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

        _context.Categories.Add(category);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetCategory",
            new { id = category.CategoryId }, category);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }
        _context.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

        if (category == null)
        {
            return NotFound("\"Category not found.");
        }
        _context.Categories.Remove(category);
        _context.SaveChanges();
        return Ok(category);
    }
}
