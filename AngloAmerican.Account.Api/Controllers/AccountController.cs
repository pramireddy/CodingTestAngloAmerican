using AngloAmerican.Account.Services;
using AngloAmerican.Account.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngloAmerican.Account.Api.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IAddressService _addressService;
        private readonly IBalanceChecker _balanceChecker;

        public AccountController(ILogger<AccountController> logger,IAccountRepository accountRepository, IAddressService addressService,IBalanceChecker balanceChecker)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _addressService = addressService;
            _balanceChecker = balanceChecker;
        }

        // GET: /accounts
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountResponse>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<AccountResponse>> Get()
        {
            var accounts = _accountRepository.GetAllAccounts();
            var response = await MapToAccountResponse(accounts);

            return response;
        }

        // POST /accounts
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AccountRequest accountRequest)
        {
            var account = new AccountModel
            {
                FirstName = accountRequest.FirstName,
                LastName = accountRequest.LastName,
                Balance = accountRequest.Balance
            };

            if(_balanceChecker.Process(accountRequest.Balance, accountRequest.LastName))
            {
                _accountRepository.Add(account);
            }
            else
            {
                // TO DO : Log Information
                _logger.LogInformation("Failed to add balance");
            }
            return NoContent();
        }

        /// <summary>
        /// TO DO: To Improve Peformance
        /// 1. Apply Parallel Process
        /// 2. Cache - Addesses
        /// 3. Include Address if it's required
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        private async Task<IEnumerable<AccountResponse>> MapToAccountResponse(IEnumerable<AccountModel> accounts)
        {
            var result = new List<AccountResponse>();

            foreach (var account in accounts)
            {
                var accountResponse = await MapAccountToAccountResponse(account);
                result.Add(accountResponse);
            }

            return result;
        }

        private async Task<AccountResponse> MapAccountToAccountResponse(AccountModel account)
        {
            return new AccountResponse
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Balance = account.Balance,
                TypeId = MapToAccountType(account.Balance),
                Address = await _addressService.GetAddress()
            };
        }

        private int MapToAccountType(int balance)
        {
            if (balance <= 5000)
            {
                return 1;
            }
            else if (balance > 5001 && balance <= 10000)
            {
                return 2;
            }
            else if (balance > 10001)
            {
                return 3;
            }

            _logger.LogError("Invalid AccountType");
            // TO DO : Throw custom exception
            throw new Exception("Invalid Balance");
        }
    }
}