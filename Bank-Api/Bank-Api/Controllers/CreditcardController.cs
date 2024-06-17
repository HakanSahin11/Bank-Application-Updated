using Bank_Api.Models;
using Bank_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditcardController : BaseController
    {
        private ICreditcardService _CreditcardService;
        public CreditcardController(ICreditcardService creditcardService)
        {
            _CreditcardService = creditcardService;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<List<Account>>> GetCreditcardByUserId(int UserId)
        {
            var accounts = await _CreditcardService.GetAccountsByUserId(UserId);
            return Ok(accounts);
        }
    }
}
