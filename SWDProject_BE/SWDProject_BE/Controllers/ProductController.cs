using BusinessLayer.Services;
using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SWDProject_BE.Controllers
{
    [Route("api/Product/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService ProductService { get; set; }


        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllProduct()
        {
            try 
            {
                List<Product> list = ProductService.GetAllProducts();
                return Ok(list);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
