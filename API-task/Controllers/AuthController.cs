using Business.Dtos.Auth;
using Business.Services.Abstract;
using Business.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace API_task.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		#region Documentation
		/// <summary>
		/// User registration
		/// </summary>
		/// <param name="model"></param>
		[ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
		#endregion
		[HttpPost("register")]
		public async Task<Response> RegisterAsync(AuthRegisterDto model)
		=> await _authService.RegisterAsync(model);

		#region Documentation
		/// <summary>
		/// User login
		/// </summary>
		/// <param name="model"></param>
		[ProducesResponseType(typeof(Response<AuthLoginResponseDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
		#endregion
		[HttpPost("login")]
		public async Task<Response<AuthLoginResponseDto>> LoginAsync(AuthLoginDto model)
		=> await _authService.LoginAsync(model);
	}
}
