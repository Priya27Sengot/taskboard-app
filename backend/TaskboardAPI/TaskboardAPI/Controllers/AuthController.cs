using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskboardAPI.Models;
using TaskboardAPI.Services;

namespace TaskboardAPI.Controllers
{
    [Route("api/[controller]")]
   
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public  async Task<IActionResult> Register(RegisterUser newUser)
        {            
           var result= await _authService.RegisterUser(newUser.UserName,newUser.Email,newUser.Password);
            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var result = await _authService.Login(loginUser.Email,loginUser.Password);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
