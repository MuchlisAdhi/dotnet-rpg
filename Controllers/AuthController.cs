using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.User;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    /// <summary>
    /// An controller to perform register and login for user
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        /// <summary>
        /// Create a user and save password hash.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/register
        ///     {
        ///        "username": "Muchlis",
        ///        "password": 123456
        ///     }
        ///
        /// Sample response body:
        /// 
        ///     {
        ///        "data": 1,
        ///        "success": true,
        ///        "message": null
        ///     }
        /// 
        /// Sample response header:
        /// 
        ///     content-type: application/json; charset=utf-8 
        ///     location: http://localhost:5000/auth/register 
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>A newly created User</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If user is already exists</response>
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)  
        {
            ServiceResponse<int> response = await _authRepo.Register(
                new User {Username = request.Username }, request.Password
            );
            if (!response.Success) 
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Create a token to authenticated a user for access API and verify password user saved before.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/login
        ///     {
        ///        "username": "Muchlis",
        ///        "password": 123456
        ///     }
        ///
        /// Sample response body:
        ///
        ///     {
        ///        "data": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJXaXJhdGFtYSIsIm5iZiI6MTU5NjEwMTQ3OSwiZXhwIjoxNTk2MTg3ODc5LCJpYXQiOjE1OTYxMDE0Nzl9.QiVo-mXgGFjYPSTS9jW_yxiZzDtMZLow-fSshYUgwgv7ZlyScEGzwnmdyACfNsW17jEP4ZXPXmweqIJsATpC4g",
        ///        "success": true,
        ///        "message": null
        ///     }
        /// 
        /// Sample response header:
        /// 
        ///     content-type: application/json; charset=utf-8 
        ///     location: http://localhost:5000/auth/login 
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>A newly created token for a User</returns>
        /// <response code="200">Returns the newly created token</response>
        /// <response code="400">If user is not found</response>
        /// <response code="400">If wrong password</response>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)  
        {
            ServiceResponse<string> response = await _authRepo.Login(
                request.Username, request.Password
            );
            if (!response.Success) 
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}