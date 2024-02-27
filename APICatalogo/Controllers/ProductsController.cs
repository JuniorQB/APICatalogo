
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Models;
using APICatalogo.Repositories;
namespace APICatalogo.Controllers;


[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly IProductRepository _productRepository;
    private readonly IRepository<Product> _repository;

    public ProductsController(IRepository<Product> repository, IProductRepository productRepository )
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    [HttpGet("proucts/{id}")]
    public ActionResult<IEnumerable<Product>> GetProductByCategory(int id)
    {
        var products = _productRepository.GetProductsByCategory(id);

        if (products is null) return NotFound("Products not found");

        return Ok(products);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _repository.GetAll();

        if (products is null) return NotFound("Products not found");

        return Ok(products);
    }

   [HttpGet("{id:int:min(1)}", Name="GetResult")]
    public ActionResult<Product> Get(int id)
    {
        var product = _repository.Get(p => p.ProductId == id);

        if (product is null) return NotFound("Product not found");

        return Ok(product);
    }
   
    [HttpPost]
    public ActionResult Post(Product product)
    {
        if (product is null) return BadRequest();
        var nProduct = _repository.Create(product);
       

        return new CreatedAtRouteResult("GetResult", new { id = nProduct.ProductId }, nProduct);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product) { 

        if(id != product.ProductId)
        {
            return BadRequest();
        }

        var productUpdated = _repository.Update(product);

            return Ok(productUpdated);
       
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var product = _repository.Get(p => p.ProductId == id);

        if (product is null) return NotFound();

        _repository.Delete(product);
         return Ok("Product deleted");
    }
}
