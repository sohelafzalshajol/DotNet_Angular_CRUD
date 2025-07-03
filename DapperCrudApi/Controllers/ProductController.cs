using DapperCrudApi.Models;
using DapperCrudApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DapperCrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            return product is not null ? Ok(product) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var data = await _repo.AddAsync(product);
            //product.Id = id;
            //return CreatedAtAction(nameof(Get), new { data.Id }, data);
            return Ok(new { status = data.Id>0 ? true : false, message = "Product Saved!", data = data });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            var stat = await _repo.UpdateAsync(product);
            return Ok(new { status = stat ? true : false, message = "Product Updated!", data = product });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            return Ok(new { status = deleted?true:false, message = "Product Deleted!", data = id });
        }
    }
}
