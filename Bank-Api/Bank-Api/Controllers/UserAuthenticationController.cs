using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank_Api.Context;
using Bank_Api.Models;
using Bank_Api.Services;

namespace Bank_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserAuthenticationController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginRequest>> PostUserAuthentication(LoginRequest userAuthentication)
        {
            try
            {
                var userWithToken = await _userService.VerifyLogin(userAuthentication);
                if (userWithToken != null)
                    return Ok(userWithToken);
            }
            catch (Exception ex)
            {
                //Add logging
            }

            return BadRequest("User not found");
        }
    }
}
