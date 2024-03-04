using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SurveyManagementProject.Application.Abstractions.IRepositories;
using SurveyManagementProject.Application.Abstractions.IServices;
using SurveyManagementProject.Domain.Entities.DTOs;
using SurveyManagementProject.Domain.Entities.Exceptions;
using SurveyManagementProject.Domain.Entities.Models;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SurveyManagementProject.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepo;

        public AuthService(IConfiguration configuration, IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<ResponceLogin> GenerateToken(RequestLogin request)
        {
            if (request == null)
            {
                return new ResponceLogin()
                {
                    Token = "User Not Found"
                };
            }
            var findUser = await FindUser(request);
            if (findUser != null)
            {

                var permission = new List<int>();

                if (findUser.Role.ToLower() != "admin")
                {
                    permission = new List<int> { 2, 3, 4, 8, 10, 11, 12 };
                }
                else
                {
                    permission = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                }
                var jsonContent = JsonSerializer.Serialize(permission);
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, findUser.Role),
                    new Claim("email", findUser.Email),
                    new Claim("UserID", findUser.Id.ToString()),
                    new Claim("CreatedDate", DateTime.UtcNow.ToString()),
                    new Claim("Permissions", jsonContent)
                };

                return await GenerateToken(claims);
            }

            return new ResponceLogin()
            {
                Token = "Un Authorize"
            };
        }
        public async Task<string> SignUp(UserDTO request)
        {
            if (request == null)
            {
                return "User should not be null";
            }
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };
            await _userRepo.Create(user);

            return "Succesfully registered";
        }

        public async Task<ResponceLogin> GenerateToken(IEnumerable<Claim> additionalClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var exDate = Convert.ToInt32(_configuration["JWT:ExpireDate"] ?? "10");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            };

            if (additionalClaims?.Any() == true)
                claims.AddRange(additionalClaims);


            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(exDate),
                signingCredentials: credentials);

            return new ResponceLogin()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
        public async Task<User> FindUser(RequestLogin user)
        {

            var result = await _userRepo.GetByAny(x => x.Email == user.email);
            if (result != null)
            {
                if (user.email == result.Email)
                {
                    if (user.password == result.Password)
                        return result;
                    throw new UserNotFoundException("Incorrect password! Try again!");
                }
                throw new UserNotFoundException("Incorrect login! Try again!");
            }
            throw new UserNotFoundException("User not found!");
        }
    }
}
