using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Dtos;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        IMapper mapper;
        private readonly IConfiguration configuration;
        public UserController(IUnitOfWork uow, IConfiguration configuration, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
            this.configuration = configuration; 

        }

        [HttpPost]
        [Route("api/user/login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.Email, loginReq.Password);
            ApiError apiError = new ApiError();

            if (user == null)
            {
                apiError.ErrorCode = Unauthorized().StatusCode;
                apiError.ErrorMessage = "Invalid user name or password";
                apiError.ErrorDetails = "This error appear when provided user id or password does not exists";
                return Unauthorized(apiError);
            }

            var loginRes = new LoginResDto();
            loginRes.Email = user.Email;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);
        }

        [HttpPost]
        [Route("api/user/register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            ApiError apiError = new ApiError();
            
            if (loginReq.Email.IsNullOrEmpty() || loginReq.Password.IsNullOrEmpty())
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User name or password can not be blank";
                return BadRequest(apiError);
            }

            if (await uow.UserRepository.UserAlreadyExists(loginReq.Email))
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User already exists, please try different user name";
                return BadRequest(apiError);
            }

            uow.UserRepository.Register(loginReq.Email, loginReq.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        [Route("api/users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var userDTOs = await uow.UserRepository.GetAllAsync();

            return Ok(mapper.Map<IEnumerable<User>>(userDTOs));
        }

        [HttpGet]
        [Route("api/users/{userId}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] int userId)
        {
            var userDTO = await uow.UserRepository.GetByIdAsync(userId);

            return Ok(mapper.Map<User>(userDTO));
        }

        [HttpGet]
        [Route("api/users/by-user-name/{userName}")]
        public async Task<ActionResult<User>> GetUserByUserName([FromRoute][Required] string userName)
        {
            var userDTO = await uow.UserRepository.GetByUserNameAsync(userName);

            return Ok(mapper.Map<User>(userDTO));
        }


        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("JWT:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Email == "admin" ? "Admin" : "User")
            };

            var signingCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
