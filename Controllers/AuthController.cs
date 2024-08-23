using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using CRUDmvc.DataAccessLayer;
using CRUDmvc.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace YourNamespace.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthController> _logger;
        private readonly ILoginDL? _loginDL;

        public AuthController(ILoginDL loginDL, IConfiguration config, HttpClient httpClient, ILogger<AuthController> logger)
        {
            _config = config;
            _httpClient = httpClient;
            _logger = logger;
            _loginDL = loginDL;
        }

        [HttpGet]
        public IActionResult Authorise()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetToken()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(LoginModel login)
        {
            ResponseLogin response = new ResponseLogin();
            response.IsSuccess = true;

            try
            {
                HasAccess hasAccess = new HasAccess();
                hasAccess = await _loginDL.Validate(login);
                if (hasAccess.IsSuccess)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, login.Username),
                            new Claim(ClaimTypes.Role, login.Role)
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                        Issuer = _config["JwtSettings:Issuer"],
                        Audience = _config["JwtSettings:Audience"]
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    response.Token = tokenString;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Token = null;
                    response.Message = "Wrong Credentials";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Token = null;
                response.Message = e.Message;
            }
            _logger.LogInformation("Token: " + response.Token);
            return View(response);
        }

        [HttpGet]
        public IActionResult ValidateToken()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> ValidateToken(string token)
        {
            _logger.LogInformation("Received token: {Token}", token);

            if (ValidateTokenHelper(token))
            {
                // Set up session or cookie
                HttpContext.Session.SetString("AuthToken", token);

                // Add this to decode the Token to read your Token-Role
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                var roleClaim = securityToken.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;
                _logger.LogInformation("Role: ", roleClaim);

                // Add token to the headers of an outgoing HTTP request
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                if (roleClaim == "Admin")
                {
                    return RedirectToAction("AdminIndex", "Auth");
                }
                else if (roleClaim == "User")
                {
                    return RedirectToAction("UserIndex", "CrudOperation");
                }
                else if (roleClaim == "student")
                {
                    return RedirectToAction("StudentIndex", "CrudOperation");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                _logger.LogWarning("Token validation failed.");
                ViewData["ErrorMessage"] = "Token validation failed.";
                return View();
            }
        }

        private bool ValidateTokenHelper(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyString = _config["JwtSettings:Key"];
                if (string.IsNullOrEmpty(keyString))
                {
                    throw new InvalidOperationException("JWT Key is not set in the configuration.");
                }
                var key = Encoding.UTF8.GetBytes(keyString); // Convert the key to byte array
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _config["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["JwtSettings:Audience"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token validation failed.");
                return false;
            }
        }
        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public IActionResult AdminIndex()
        //{
        //    return View();
        //}

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            LogReadRecordResponse response2 = new LogReadRecordResponse();
            try
            {
                response2 = await _loginDL.ReadRecord();
                response2.IsSuccess = true;
                response2.Message = "Data Read!";

            }
            catch (Exception e)
            {
                response2.IsSuccess = false;
                response2.Message = e.Message;
            }
            //return (Json(response2));
            return View(response2);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/en/Auth/InsertRecord")]
        public async void InsertRecord([FromBody] LoginModel request)
        {
            _logger.LogInformation("request sent: " + request.ToString());
            try
            {
                await _loginDL.InsertRecord(request);
                _logger.LogInformation("request sent: " + request.ToString());
            }
            catch (Exception e)
            {
                _logger.LogInformation("Exception: " + e);
            }

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/en/Auth/UpdateRecord")]
        public async Task<IActionResult> UpdateRecord([FromBody] LoginModel request)
        {
            LogUpdateRecordResponse response = new LogUpdateRecordResponse();
            response.Message = "Data Updated Succesfully!";
            response.IsSuccess = true;
            _logger.LogInformation("request sent: " + request.ToString());
            try
            {
                await _loginDL.UpdateRecord(request);
                _logger.LogInformation("request sent: " + request.ToString());
            }
            catch (Exception e)
            {
                response.Message = "Exception: " + e;
                response.IsSuccess = false;
            }
            return Json(response);

        }

    }
}
