using Bank_Api.Models;
using Bank_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserInfo>>> GetAllUsers()
        {
            var users = await _userService.GetAllUserInfos();
            return Ok(users);

        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<UserInfo>> GetUserById(int Id)
        {
            var user = await _userService.GetUser(Id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserAuthentication>> CreateUser(CreateUser NewUserRequest)
        {
            //add validation for all fields
            try
            {
                var user = await _userService.CreateUser(NewUserRequest);
                if (user != null)
                {
                    return Ok(user);
                }

                return StatusCode(500);
            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid user");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
