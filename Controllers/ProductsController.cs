using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManagementAPI.Models;
using ProductManagementAPI.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;

        private readonly IProductRepo _repo;


        public ProductsController(ILogger<ProductsController> logger, IProductRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = _repo.GetAllProducts();
                return Ok(products);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _repo.GetProduct(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]Product model)
        {
            try
            {
                if (model == null) return BadRequest();
                var product = await _repo.CreateProduct(model);

                if (product == -1) return BadRequest("Product Already Exists");

                return Ok(product);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateProduct([FromBody]Product product, int id)
        //{
        //    try
        //    {
        //        var existingItem = await _repo.GetProduct(id);

        //        if (existingItem == null) return BadRequest();

        //        {
        //            existingItem.Name = existingItem.Name;
        //            existingItem.Description = existingItem.Description;
        //            existingItem.DateUpdated = existingItem.DateUpdated;
        //            existingItem.DateDeleted = existingItem.DateDeleted;
        //            existingItem.Status = existingItem.Status;
        //            existingItem.Price = existingItem.Price;

        //            //Product updatedProduct = new Product();
                    

        //            await _repo.UpdateProduct(product);

        //            return Ok();
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
    }
}
