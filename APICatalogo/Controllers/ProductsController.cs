
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
namespace APICatalogo.Controllers;


[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly IUnitOfWork _repository;
    private readonly IMapper _mapper;
    

    public ProductsController(IUnitOfWork repository,IMapper mapper )
    {
        _repository = repository;
        _mapper = mapper;
        
    }

    [HttpGet("proucts/{id}")]
    public ActionResult<IEnumerable<ProductDTO>> GetProductByCategory(int id)
    {
        var products = _repository.ProductRepository.GetProductsByCategory(id);

        if (products is null) return NotFound("Products not found");

        // var 
        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

        return Ok(productsDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductDTO>> Get()
    {
        var products = _repository.ProductRepository.GetAll();

        if (products is null) return NotFound("Products not found");
        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return Ok(productsDto);
    }

   [HttpGet("{id:int:min(1)}", Name="GetResult")]
    public ActionResult<Product> Get(int id)
    {
        var product = _repository.ProductRepository.Get(p => p.ProductId == id);

        if (product is null) return NotFound("Product not found");
        var productDto = _mapper.Map<ProductDTO>(product);
        return Ok(productDto);
    }
   
    [HttpPost]
    public ActionResult<ProductDTO> Post(ProductDTO productDTO)
    {
        if (productDTO is null) return BadRequest();

        var product = _mapper.Map<Product>(productDTO);
        var nProduct = _repository.ProductRepository.Create(product);
        _repository.Commit();

        var newProductDto = _mapper.Map<ProductDTO>(nProduct);
        return new CreatedAtRouteResult("GetResult", new { id = newProductDto.ProductId }, newProductDto);
    }


    [HttpPut("{id:int}")]
    public ActionResult<ProductDTO> Put(int id, ProductDTO productDTO) { 

        if(id != productDTO.ProductId)
        {
            return BadRequest();
        }
        var product = _mapper.Map<Product>(productDTO);
        var productUpdated = _repository.ProductRepository.Update(product);
        _repository.Commit();
        var productUpdatedDto = _mapper.Map<ProductDTO>(productUpdated);

        return Ok(productUpdatedDto);
       
    }

    [HttpPatch("{id}/UpdatePartial")]
    public ActionResult<ProductDTOUpdateResponse> Patch(int id, JsonPatchDocument<ProductDTOUpdateRequest> patchProductDTO)
    {
        if (patchProductDTO is null || id <= 0)
        {
            return BadRequest();
        }

        var product = _repository.ProductRepository.Get(c => c.ProductId == id);
        if(product is null) return NotFound();

        var productUpdatedRequest = _mapper.Map<ProductDTOUpdateRequest>(product);

        patchProductDTO.ApplyTo(productUpdatedRequest, ModelState);

        if (!ModelState.IsValid || TryValidateModel(productUpdatedRequest))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(productUpdatedRequest, product);
        _repository.ProductRepository.Update(product);

        return Ok(_mapper.Map<ProductDTOUpdateResponse>(product));
    }
    
    [HttpDelete("{id:int}")]
    public ActionResult<ProductDTO> Delete(int id)
    {
        var product = _repository.ProductRepository.Get(p => p.ProductId == id);

        if (product is null) return NotFound();

        var productDeleted = _repository.ProductRepository.Delete(product);
        _repository.Commit();
        var productDeletedDto = _mapper.Map<ProductDTO>(productDeleted);
        return Ok(productDeletedDto);
    }
}
