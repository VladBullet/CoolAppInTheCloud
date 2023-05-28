using CoolAppInTheCloud.Data;
using CoolAppInTheCloud.Data.Models;
using CoolAppInTheCloud.Helpers_Extensions;
using CoolAppInTheCloud.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoolAppInTheCloud.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _config;
        private MockDatabase _db = MockDatabase.Instance;
        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        [Route("/security/createToken")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginVM login)
        {
            {
                IActionResult response = Unauthorized();
                var user = await AuthenticateUserAsync(login);
                if (user == null)
                {
                    return BadRequest("Username or password incorrect!");
                }

                if (user != null)
                {
                    var tokenString = GenerateJSONWebToken(user);
                    response = Ok(new { token = tokenString });
                }

                return response;
            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim("UserId",userInfo.Id.ToString()),
                new Claim("Username",userInfo.Username),
                new Claim(ClaimTypes.Role,userInfo.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> AuthenticateUserAsync(LoginVM login)
        {
            User user = null;

            //Validate the User Credentials    
            User? dbUser = _db.Users.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password.ToMd5());
            if (dbUser != null)
            {
                user = new User { Username = dbUser.Username, Id = dbUser.Id, Role = dbUser.Role };
            }

            return user;
        }
    }
}
