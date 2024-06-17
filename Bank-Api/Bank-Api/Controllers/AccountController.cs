using Bank_Api.Models;
using Bank_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController 
    {
        private readonly IAccountService _AccountService;
        public AccountController(IAccountService accountService) 
        {
            _AccountService = accountService;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<List<Account>>> GetAccountByUserId(int UserId)
        {
            var accounts = await _AccountService.GetAccountsByUserId(UserId);
            return Ok(accounts);
        }
    }
}
