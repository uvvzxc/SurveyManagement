using Microsoft.AspNetCore.Mvc;
using SurveyManagementProject.Application.Abstractions.IServices;
using SurveyManagementProject.Domain.Entities.DTOs;
using SurveyManagementProject.Domain.Entities.Models;

namespace SurveyManagementProject.API.Controllers.Identity
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authSevice;
        public AuthController(IAuthService authService)
        {
            _authSevice = authService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponceLogin>> Login([FromForm] RequestLogin model)
        {
            var result = await _authSevice.GenerateToken(model);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<string>> SignUp([FromForm] UserDTO model)
        {
            var result = await _authSevice.SignUp(model);

            return Ok(result);
        }
    }
}
