using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Application.Interfaces.IServicies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace solvexTecnical.Presentation.API.Controllers.v1
{
    public class BrandController : BaseApiController
    {
        private readonly IBrandService _brandService;
        private readonly ISuperMarketService _superMarketService;
        public BrandController(IBrandService brandService, ISuperMarketService
            superMarketService)
        {
            _brandService = brandService;
            _superMarketService = superMarketService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(BrandDTO brand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(brand);
                }

                var superMarket = await _superMarketService.GetByIdDTO(brand.SuperMarketId);
                if (superMarket == null)
                {
                    ModelState.AddModelError("Error", $"Super Market with id {brand.SuperMarketId} don't exist");
                    return BadRequest(ModelState);
                }

                var CreatedBrand = await _brandService.Add(brand);

                if (CreatedBrand != null)
                    return Ok(CreatedBrand);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<BrandDTO> Brands = await _brandService.GetAllWithIncludesAsync(new List<string> { "SuperMarket"});

                if (Brands.Count == 0)
                    return NoContent();

                return Ok(Brands);
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
        public async Task<IActionResult> Put([FromRoute] int id, BrandDTO brand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(brand);
                }

                var brandFound = await _brandService.GetByIdDTO(id);
                if (brandFound == null)
                {
                    ModelState.AddModelError("Error", $"Brand with id {id} don't exist");
                    return BadRequest(ModelState);
                }
                brand.Id = id;
                brand.SuperMarketId = brandFound.SuperMarketId;

                await _brandService.Update(brand, id);

                return Ok(brand);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var brandFound = await _brandService.GetByIdDTO(id);
                if (brandFound == null)
                {
                    ModelState.AddModelError("Error", $"Brand with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                return Ok(brandFound);
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
                var brandFound = await _brandService.GetByIdDTO(id);
                if (brandFound == null)
                {
                    ModelState.AddModelError("Error", $"SuperMarket with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                await _brandService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
