using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Core.Application.Interfaces.IServicies;
using solvexTecnical.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace solvexTecnical.Presentation.API.Controllers.v1
{
    public class SuperMarketController : BaseApiController
    {
        private readonly ISuperMarketService _superMarketServices;
        public SuperMarketController(ISuperMarketService superMarketServices)
        {
            _superMarketServices = superMarketServices;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SuperMarketDTO superMarketDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(superMarketDTO);
                }

                var CreatedSuperMarket = await _superMarketServices.Add(superMarketDTO);

                if (CreatedSuperMarket != null)
                    return Ok(CreatedSuperMarket);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuperMarketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<SuperMarketDTO> superMarkets = await _superMarketServices.GetAllWithIncludesAsync(new List<string> { "Products", "ProductsBrands" });

                if (superMarkets.Count == 0)
                    return NotFound();

                return Ok(superMarkets);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuperMarketDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] int id, SuperMarketDTO superMarket)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(superMarket);
                }

                var superMarketFound = await _superMarketServices.GetByIdDTO(id);
                if (superMarketFound == null)
                {
                    ModelState.AddModelError("Error", $"SuperMarket with id {id} don't exist");
                    return BadRequest(ModelState);
                }
                superMarket.Id = id;

                await _superMarketServices.Update(superMarket, id);

                return Ok(superMarket);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuperMarketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            try
            {
                var superMarketFound = await _superMarketServices.GetByIdDTO(id);
                if (superMarketFound == null)
                {
                    ModelState.AddModelError("Error", $"SuperMarket with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                return Ok(superMarketFound);
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
                var superMarketFound = await _superMarketServices.GetByIdDTO(id);
                if (superMarketFound == null)
                {
                    ModelState.AddModelError("Error", $"SuperMarket with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                await _superMarketServices.Delete(id);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
