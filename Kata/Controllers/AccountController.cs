using System.ComponentModel;
using Kata.Models;
using Kata.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kata.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICsvAccountReaderService _csvAccountReaderService;

        private readonly ILogger<AccountController> _logger;

        /// <inheritdoc />
        public AccountController(IAccountService accountService, ICsvAccountReaderService csvAccountReaderService,
            ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _csvAccountReaderService = csvAccountReaderService;
            _logger = logger;
        }


        /// <summary>
        /// You can get account by page you will see all the transaction ordered by date.
        /// Page size is fixed to 100 transactions.
        /// </summary>
        [HttpGet("/account")]
        public IActionResult GetAccount(int page)
        {
            try
            {
                //since the transaction number is big I want to implement pagination system with sorted transactions
                Account account = _csvAccountReaderService.DeserializeCsv();

                //TODO : add sorted transaction to show ordered transaction by date pages to optimize the orderBy
                int pageSize = 100;
                int startIndex = (page - 1) * pageSize;
                int endIndex = startIndex + pageSize;
                List<Transaction> transactionsResult = account.Transactions.OrderBy(t => t.Date)
                    .Skip(startIndex)
                    .Take(pageSize)
                    .ToList();
                account.Transactions = transactionsResult;

                
                _logger.LogInformation("Account retrieved successfully");
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting account");
                return StatusCode(500, "Internal Server Error");
            }

        }
        
        /// <summary>
        /// You can get account value by target date, Date format should be MM/dd/yyyy to input.
        /// </summary>
        [HttpGet("/account/value")]
        public IActionResult GetAccountValue(DateTime targetDate)
        {
            try
            {
                //check that the input date is between 1 jan 2022 and 1 March 2023
                if (targetDate >= new DateTime(2022, 1, 1) && targetDate <= new DateTime(2023, 3, 1))
                {
                    Account account = _csvAccountReaderService.DeserializeCsv();

                    decimal accountValue = _accountService.GetAccountValueForDate(targetDate, account);
                    _logger.LogInformation("Account value retrieved successfully");
                    return Ok($"Account value on date {targetDate} is {accountValue} EUR");
                }
                else
                {
                    return BadRequest("Invalid target date");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting account value");
                return StatusCode(500, "Internal Server Error");
            }

        }


    }
}
