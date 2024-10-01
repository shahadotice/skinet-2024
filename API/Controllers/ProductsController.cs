using API.RequestHelpers;
using Core.Entity;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    
    public class ProductsController (IGenericRepository<Product> repo): BaseApiController
    {
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]ProductSpecParam specParam)
        {
            var spec= new ProductSpecification(specParam);
            // var products= await repo.ListAsync(spec);
            // var count=await repo.CountAsync(spec);
            // var pagingnation=new Pagination<Product>(specParam.PageIndex,specParam.PageSize,count,products);
            // return Ok(pagingnation);
              return await CreatePagedResult(repo, spec, specParam.PageIndex, specParam.PageSize);
            
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
