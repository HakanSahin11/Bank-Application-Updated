using Bank_Api.Models;
using Bank_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _TransactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _TransactionService = transactionService;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<List<TransactionResponse>>> GetAccountByUserId(int UserId)
        {
            var transactions = await _TransactionService.GetAllTransactionsFromUser(UserId);
            return Ok(transactions);
        }

        [HttpPost("{UserId}")]
        public async Task<ActionResult<TransactionResponse>> CreateUser(TransactionRequest NewTransaction, int UserId)
        {
            var transaction = await _TransactionService.CreateTranscation(NewTransaction, UserId);
            if (transaction != null)
            {
                return Ok(transaction);
            }

            return BadRequest("Transaction failed");

        }
    }
}
