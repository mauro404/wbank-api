using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WBank.API.Data;
using WBank.API.Models.Domain;
using WBank.API.Models.DTO;
using WBank.API.Repositories.Interface;

namespace WBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankRepository bankRepository;
        public BankController(IBankRepository bankRepository)
        {
            this.bankRepository = bankRepository;
        }

        // Registration endpoint
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountRequestDto request)
        {
            var account = new Account
            {
                AccountNumber = new Random().Next(10000, 99999),
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Balance = 0,
                AccountTransactions = new List<AccountTransaction>()
            };

            await bankRepository.CreateAsync(account);

            var response = new AccountDto
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Name = account.Name,
                Email = account.Email,
                Password = account.Password,
                Balance = account.Balance,
                AccountTransactions = account.AccountTransactions
            };

            return Ok(response);
        }

        // Login endpoint
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto request)
        {
                var account = await bankRepository.LoginAsync(request);

                if (account == null)
                    return NotFound();

                var response = new AccountDto
                {
                    Id = account.Id,
                    AccountNumber = account.AccountNumber,
                    Name = account.Name,
                    Email = account.Email,
                    Password = account.Password,
                    Balance = account.Balance,
                    AccountTransactions = account.AccountTransactions
                };
                return Ok(response);
        }

        // Get account endpoint
        [HttpGet]
        [Route("user/{id:Guid}")]
        public async Task<IActionResult> GetAccount([FromRoute] Guid id)
        {
            var account = await bankRepository.GetAccountAsync(id);

            if (account == null)
                return NotFound();

            var response = new AccountInfoDto
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Name = account.Name,
                Email = account.Email,
                Balance = account.Balance,
            };

            return Ok(response);
        }

        // New transaction endpoint
        [HttpPost]
        [Route("transfer")]
        public IActionResult TransferFunds([FromBody] TransferRequestDto request)
        {
            if (bankRepository.TransferFunds(request))
                return Ok();
           
            return BadRequest();
        }

        // Add funds endpoint
        [HttpPost("deposit")]
        public IActionResult AddFunds([FromBody] AddFundsRequestDto request)
        {
            if (bankRepository.AddFunds(request))
                return Ok();

            return BadRequest();
        }

        [HttpGet("transfers/{accountId}")]
        public IActionResult GetTransferHistory(Guid accountId)
        {
            var transferHistory = bankRepository.GetTransferHistory(accountId);

            if (transferHistory == null)
                return NotFound();

            return Ok(transferHistory);
        }
    }
}
