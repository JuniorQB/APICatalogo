using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _context.Products.ToList();

        if (products is null) return NotFound("Products not found");

        return products;
    }

    [HttpGet("{id:int}", Name = "GetResult")]
    public ActionResult<Product> Get(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

        if (product is null) return NotFound("Product not found");

        return product;
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        if (product is null) return BadRequest();
        _context.Products.Add(product);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetResult", new { id = product.ProductId }, product);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product) { 

        if(id != product.ProductId)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;

        return Ok(product);
    }
}
