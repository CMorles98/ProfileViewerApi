using ProfileViewer.Domain.DTOs.Users;
using ProfileViewer.Domain.Services.Base;
using Microsoft.AspNetCore.Mvc;
using ProfileViewer.Application.Authorization;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace ProfileViewer.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/users")]
    [Authorize(AppPermissions.UserPermissions.USERS)]
    public class UsersController(IServiceManager service) : ControllerBase
    {
        private readonly IServiceManager _service = service;

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <returns>The updated user data.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(AppPermissions.UserPermissions.EDIT)]
        public async Task<IActionResult> Update(Guid id, [FromBody] EditUserDto dto)
        {
            var updatedUser = await _service.UserService.Update(dto, id);
            return Ok(updatedUser);
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <returns>The user data.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _service.UserService.GetById(id);
            return Ok(user);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>The list of users.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] UserFiltersDto filters)
        {
            var users = await _service.UserService.GetAll(filters);
            return Ok(users);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(AppPermissions.UserPermissions.DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.UserService.Delete(id);
            return NoContent();
        }
    }
}
