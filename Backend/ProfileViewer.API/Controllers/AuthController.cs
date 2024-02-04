using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileViewer.Domain.DTOs.Auth;
using ProfileViewer.Domain.Services.Base;

namespace ProfileViewer.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController(IServiceManager services) : ControllerBase
    {
        private readonly IServiceManager _services = services;

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <returns>The ID of the created user.</returns>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _services.AuthService.Register(dto);
            return CreatedAtAction(nameof(Register), new { result.Token });
        }

        /// <summary>
        /// Refreshes a JWT token.
        /// </summary>
        /// <param name="token">Expired token to refresh.</param>
        /// <returns>A new JWT token.</returns>
        [HttpPost]
        [Route("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
        {
            var newToken = await _services.AuthService.RefreshToken(dto);
            return Ok(new { Token = newToken });
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <returns>The JWT token upon successful login.</returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _services.AuthService.Login(dto);
            return Ok(new { Token = token });
        }
    }
}
