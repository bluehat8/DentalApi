using DentalApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DentalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JwtUserController : Controller
    {
        private IConfiguration _config;
        public JwtUserController(IConfiguration configuration)
        {
            _config = configuration;
        }

        private JwtUser AuthenticateUser(JwtUser user)
        {
            JwtUser _user = null;
            if (user.UserName == "userAgustin" && user.Password == "test")
            {
                _user = new JwtUser { UserName = "Administrador del Sistema" };

            }
            return _user;

        }

        private string GenerateToken()
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        // [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(JwtUser user)
        {

            IActionResult response = Unauthorized();
            var user_ = AuthenticateUser(user);
            if (user_ != null)
            {

                var token = GenerateToken();
                response = Ok(new { token = token });

            }
            return response;


        }
    }
}
