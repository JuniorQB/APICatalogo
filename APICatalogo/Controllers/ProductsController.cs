
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Models;
using APICatalogo.Repositories;
namespace APICatalogo.Controllers;


[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly IUnitOfWork _repository;
    

    public ProductsController(IUnitOfWork repository )
    {
        _repository = repository;
        
    }

    [HttpGet("proucts/{id}")]
    public ActionResult<IEnumerable<Product>> GetProductByCategory(int id)
    {
        var products = _repository.ProductRepository.GetProductsByCategory(id);

        if (products is null) return NotFound("Products not found");

        return Ok(products);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _repository.ProductRepository..GetAll();

        if (products is null) return NotFound("Products not found");

        return Ok(products);
    }

   [HttpGet("{id:int:min(1)}", Name="GetResult")]
    public ActionResult<Product> Get(int id)
    {
        var product = _repository.ProductRepository..Get(p => p.ProductId == id);

        if (product is null) return NotFound("Product not found");

        return Ok(product);
    }
   
    [HttpPost]
    public ActionResult Post(Product product)
    {
        if (product is null) return BadRequest();
        var nProduct = _repository.ProductRepository.Create(product);
        _repository.Commit();

        return new CreatedAtRouteResult("GetResult", new { id = nProduct.ProductId }, nProduct);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product) { 

        if(id != product.ProductId)
        {
            return BadRequest();
        }

        var productUpdated = _repository.ProductRepository.Update(product);
        _repository.Commit();
        return Ok(productUpdated);
       
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var product = _repository.ProductRepository.Get(p => p.ProductId == id);

        if (product is null) return NotFound();

        _repository.ProductRepository.Delete(product);
        _repository.Commit();
         return Ok("Product deleted");
    }
}
