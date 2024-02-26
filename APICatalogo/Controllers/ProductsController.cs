
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Models;
using APICatalogo.Repositories;
namespace APICatalogo.Controllers;


[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _repository.GetProducts().ToList();

        if (products is null) return NotFound("Products not found");

        return Ok(products);
    }

   [HttpGet("{id:int:min(1)}", Name="GetResult")]
    public ActionResult<Product> Get(int id)
    {
        var product = _repository.GetProduct(id);

        if (product is null) return NotFound("Product not found");

        return Ok(product);
    }
   
    [HttpPost]
    public ActionResult Post(Product product)
    {
        if (product is null) return BadRequest();
        var nProduct = _repository.CreateProduct(product);
       

        return new CreatedAtRouteResult("GetResult", new { id = nProduct.ProductId }, nProduct);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product) { 

        if(id != product.ProductId)
        {
            return BadRequest();
        }

        
        if(_repository.UpdateProduct(product))
        {
            return Ok(product);
        } else
        {
            return StatusCode(500, $"Error to update the product with ID: {id}");
        }

       
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
      bool deleted = _repository.DeleteProduct(id);
        if (deleted)
        {
            return Ok("Product deleted");
        } else
        {
            return StatusCode(500, $"Error to delete the product with ID: {id}");
        }
    }
}
