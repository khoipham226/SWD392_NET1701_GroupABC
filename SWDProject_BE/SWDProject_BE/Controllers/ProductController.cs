using BusinessLayer.RequestModels.Product;
using BusinessLayer.Services;
using DataLayer.Dto.Product;
using DataLayer.Model;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<IActionResult> GetAllProduct()
        {
            try 
            {
               var product = await ProductService.GetAllProducts();
                return Ok(product);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("getProductDetails/{id}")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            try
            {
                var producmodel = await ProductService.GetProductDetailsResponseModel(id);
                if (producmodel != null)
                {
                    return Ok(producmodel);
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

        [HttpGet]
        [Route("getProductByUserId/{userId}")]
        public async Task<IActionResult> getProductByUserId(int userId)
        {
            try
            {
                var producModel = await ProductService.GetProductByUserId(userId);
                if (!producModel.IsNullOrEmpty())
                {
                    return Ok(producModel);
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
