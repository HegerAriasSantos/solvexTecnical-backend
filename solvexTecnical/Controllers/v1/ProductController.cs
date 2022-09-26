using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Application.Interfaces.IServicies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace solvexTecnical.Presentation.API.Controllers.v1
{
    public class ProductController : BaseApiController
    {
        private readonly IProductsServices _productsServices;
        private readonly IBrandService _brandServices;
        private readonly ISuperMarketService _superMarketServices;
        public ProductController(IProductsServices productsServices, IBrandService brandService, ISuperMarketService superMarketService)
        {
            _productsServices = productsServices;
            _brandServices = brandService;
            _superMarketServices = superMarketService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(FinalProductDTO product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(product);
                }
                var brandFound = await _brandServices.GetByIdDTO(product.BrandId);
                if (brandFound == null)
                {
                    ModelState.AddModelError("Error", $"Brand with id {product.BrandId} don't exist");
                    return BadRequest(ModelState);
                }

                if (product.Name == null)
                {
                    ModelState.AddModelError("Error", $"Name is Required");
                    return BadRequest(ModelState);
                }

                var CreatedProduct = await _productsServices.AddProduct(product);

                CreatedProduct.SuperMarketId = CreatedProduct.SuperMarketId;
                CreatedProduct.Brand = brandFound;
                
                if (CreatedProduct == null)
                    return NoContent();
                
                return Ok(CreatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FinalProductDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<FinalProductDTO> Products = await _productsServices.GetAllProducts();

                if (Products.Count == 0)
                    return NoContent();

                return Ok(Products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute]int id, FinalProductDTO finalProductDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(finalProductDTO);
                }

                var productFound = await _productsServices.GetByIdDTO(id);
                if (productFound == null)
                {
                    ModelState.AddModelError("Error", $"Product with id {id} don't exist");
                    return BadRequest(ModelState);
                }
                
                finalProductDTO.Id = productFound.Id;
                finalProductDTO.ProductId = productFound.ProductId;
                finalProductDTO.BrandId = productFound.BrandId;
                finalProductDTO.SuperMarketId = productFound.SuperMarketId;

                if (finalProductDTO.Name != null)  
                    await _productsServices.SetName(finalProductDTO);

                await _productsServices.Update(finalProductDTO, id);

                return Ok(finalProductDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FinalProductDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var productFound = await _productsServices.GetByIdDTO(id);
                if (productFound == null)
                {
                    ModelState.AddModelError("Error", $"Product with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                return Ok(await _productsServices.GetOne(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var productFound = await _productsServices.GetByIdDTO(id);
                if (productFound == null)
                {
                    ModelState.AddModelError("Error", $"Product with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                await _productsServices.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
