using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Application.Interfaces.IServicies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace solvexTecnical.Presentation.API.Controllers.v1
{
    public class UserController : BaseApiController
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(user);
                }
                
                if (user.Cedula == null)
                {
                    ModelState.AddModelError("Error", $"Cedula is required");
                    return BadRequest(ModelState);
                }
                if (user.Name == null)
                {
                    ModelState.AddModelError("Error", $"Name is required");
                    return BadRequest(ModelState);
                }

                var CreatedUser = await _userServices.Add(user);

                if (CreatedUser != null)
                    return Ok(CreatedUser);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<UserDTO> users = await _userServices.GetAllDTO();

                if (users.Count == 0)
                    return NoContent();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] int id, UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(user);
                }

                var userFound = await _userServices.GetByIdDTO(id);
                if (userFound == null)
                {
                    ModelState.AddModelError("Error", $"User with id {id} don't exist");
                    return BadRequest(ModelState);
                }
                user.Id = id;
                await _userServices.Update(user, id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var userFound = await _userServices.GetByIdDTO(id);
                if (userFound == null)
                {
                    ModelState.AddModelError("Error", $"User with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                return Ok(userFound);
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
                var userFound = await _userServices.GetByIdDTO(id);
                if (userFound == null)
                {
                    ModelState.AddModelError("Error", $"User with id {id} don't exist");
                    return BadRequest(ModelState);
                }

                await _userServices.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
