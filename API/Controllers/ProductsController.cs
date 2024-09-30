using Core.Entity;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController (IGenericRepository<Product> repo): ControllerBase
    {
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string ? brand,string ? type,string ? sort)
        {
            var spec= new ProductSpecification(brand,type,sort);
            var products= await repo.ListAsync(spec);
            return Ok(products);
            
        }

          [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec=new BrandListSpecification();
            var brands=await repo.ListAsync(spec);
            return Ok(brands);
            
        }
           [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec=new TypeListSpecification();
            return Ok(await repo.ListAsync(spec));
            
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product= await repo.GetByIdAsync(id);
            if(product==null) return NotFound();
            return product;

        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
        {
           repo.Add(product);
           if(await repo.SaveAllAsync()){
            return CreatedAtAction("GetProduct",new {id=product.Id},product);
           }
           return BadRequest("Problem Creating Product");

        }

         [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id,Product product)
        {
          if(product.Id!=id || !ProductExists(id))
          return BadRequest("Can not update this product");
          repo.Update(product);
          if(await repo.SaveAllAsync()){
            return NoContent();
           }
           return BadRequest("Problem Updating Product");

        }

           [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
          var product= await repo.GetByIdAsync(id);
          if(product==null) return NotFound();
          repo.Remove(product);
          if(await repo.SaveAllAsync()){
            return NoContent();
           }
           return BadRequest("Problem Deleting Product");

        }
        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }



         
    }
}
