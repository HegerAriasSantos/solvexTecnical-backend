using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Application.Interfaces.IServicies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace solvexTecnical.Presentation.API.Controllers.v1
{
    public class ShoppingListController : BaseApiController
    {
        private readonly IShoppingList _shoppingList;
        private readonly ISuperMarketService _superMarketService;
        private readonly IUserServices _userService;
        
        public ShoppingListController(IShoppingList shoppingList, ISuperMarketService superMarketService, IUserServices userServices)
        {
            _shoppingList = shoppingList;
            _superMarketService = superMarketService;
            _userService = userServices;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(ShoppingListDTO shoppingList)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(shoppingList);
                }

                var superMarket = await _superMarketService.GetByIdDTO(shoppingList.SuperMarketId);
                if (superMarket == null)
                {
                    ModelState.AddModelError("Error", $"Super Market with id {shoppingList.SuperMarketId} don't exist");
                    return BadRequest(ModelState);
                }
                var user = await _userService.GetByIdDTO(shoppingList.UserId);
                if (user == null)
                {
                    ModelState.AddModelError("Error", $"User with id {shoppingList.UserId} don't exist");
                    return BadRequest(ModelState);
                }

                if (shoppingList.ProductsIds == null)
                {
                    ModelState.AddModelError("Error", $"ProductsIds is required and can't be 0");
                    return BadRequest(ModelState);
                }
                shoppingList.SuperMarketName = superMarket.Name;
                var CreatedShoppingList = await _shoppingList.AddShoppingList(shoppingList);

                if (CreatedShoppingList != null)
                    return Ok(CreatedShoppingList);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingListDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<ShoppingListDTO> shoppingLists = await _shoppingList.GetAll();

                if (shoppingLists.Count == 0)
                    return NoContent();

                return Ok(shoppingLists);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingListDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] int id, ShoppingListDTO shoppingList)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(shoppingList);
                }

                var shoppingListFound = await _shoppingList.GetByIdDTO(id);
                if (shoppingListFound == null)
                {
                    ModelState.AddModelError("Error", $"Brand with id {id} don't exist");
                    return BadRequest(ModelState);
                }
                shoppingList.Id = id;
                shoppingList.SuperMarketId = shoppingListFound.SuperMarketId;

                await _shoppingList.Update(shoppingList, id);

                return Ok(shoppingList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingListDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetByIdDTO(id);
                if (user == null)
                {
                    ModelState.AddModelError("Error", $"User with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                List<ShoppingListDTO> shoppingLists = await _shoppingList.GetAllByUser(id);
                
                if (shoppingLists.Count == 0)
                    return NoContent();

                return Ok(shoppingLists);
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
                var shoppingListFound = await _shoppingList.GetByIdDTO(id);
                if (shoppingListFound == null)
                {
                    ModelState.AddModelError("Error", $"Shopping List with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                await _shoppingList.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
