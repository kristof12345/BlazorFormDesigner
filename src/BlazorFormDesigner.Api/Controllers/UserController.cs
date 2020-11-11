using System.Threading.Tasks;
using AutoMapper;
using BlazorFormDesigner.Api.Converters;
using BlazorFormDesigner.BusinessLogic.Exceptions;
using BlazorFormDesigner.BusinessLogic.Services;
using BlazorFormDesigner.Web.Models;
using LoginApp.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFormDesigner.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : AppController
    {
        private readonly UserService UserService;

        public UserController(UserService userService, IMapper mapper) : base(mapper)
        {
            UserService = userService;
        }

        [HttpPut]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            BusinessLogic.Models.User user;
            try
            {
                user = await UserService.ValidatePassword(request.Username, request.Password);
            }
            catch (InvalidUsernameException)
            {
                return NotFound(new ErrorResponse("Invalid username."));
            }
            catch (InvalidPasswordException)
            {
                return Unauthorized(new ErrorResponse("Invalid password."));
            }

            var token = TokenService.GenerateToken(user.Username);

            return Ok(user.ToDTO(mapper, token));
        }

        [HttpPost]
        public async Task<User> Create(UserRequest request)
        {
            var result = await UserService.Create(request.ToModel(mapper), request.Password);
            return result.ToDTO(mapper);
        }

        [HttpPut]
        public async Task<User> Update(UserRequest request)
        {
            var result = await UserService.Update(request.ToModel(mapper));
            return result.ToDTO(mapper);
        }

        [HttpDelete]
        [Route("{username}")]
        public async Task<User> Delete([FromRoute] string username)
        {
            var result = await UserService.Delete(username);
            return result.ToDTO(mapper);
        }
    }
}
