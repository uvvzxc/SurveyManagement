using Microsoft.AspNetCore.Mvc;
using SurveyManagementProject.Application.Abstractions.IServises;
using SurveyManagementProject.Domain.Entities.DTOs;
using SurveyManagementProject.Domain.Entities.Models;

namespace SurveyManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class bUserController : ControllerBase
    {
        private readonly IUserService _userServise;
        public bUserController(IUserService userServise)
        {
            _userServise = userServise;
        }
        [HttpGet]
        public async Task<User> GetByName(string name)
        {
            var res = await _userServise.GetByName(name);
            return res;
        }
        [HttpGet]
        public async Task<User> GetById(int Id)
        {
            var res = await _userServise.GetById(Id);
            return res;
        }
        [HttpGet]
        public async Task<User> GetBtEmail(string Email)
        {
            var res = await _userServise.GetByEmail(Email);
            return res;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            var res = await _userServise.GetAll();
            return res;
        }
        [HttpGet]
        public async Task<IActionResult> DownloadFile()
        {
            var filePath = await _userServise.GetPdfPath();

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");


            var fileBytes = System.IO.File.ReadAllBytes(filePath);


            var contentType = "application/octet-stream";

            var fileExtension = Path.GetExtension(filePath).ToLowerInvariant();

            return File(fileBytes, contentType, Path.GetFileName(filePath));
        }
        [HttpPost]
        public async Task<string> Create([FromForm] UserDTO userDTO)
        {
            var res = await _userServise.Create(userDTO);
            return res;
        }
        [HttpPut]
        public async Task<ActionResult<string>> UpdateUser(int id, [FromForm] UserDTO userDTO)
        {
            return await _userServise.Update(id, userDTO);
        }
        [HttpDelete]
        public async Task<string> Delete(int Id)
        {
            var res = await _userServise.Delete(Id);
            return res;
        }
    }
}
