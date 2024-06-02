using BusinessLayer.RequestModels.Product;
using BusinessLayer.Services;
using DataLayer.Dto.Product;
using DataLayer.Model;
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

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDto dto)
        {
            try
            {
                String message = await ProductService.addProduct(dto);
                return Ok(message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            try
            {
                String message = await ProductService.updateProduct(id,dto);
                if(message != null)
                {
                    return Ok(message);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                String message = await ProductService.deleteProduct(id);
                if (message != null)
                {
                    return Ok(message);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
