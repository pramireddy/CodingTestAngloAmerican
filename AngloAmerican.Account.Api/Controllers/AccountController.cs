using AngloAmerican.Account.Services;
using AngloAmerican.Account.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngloAmerican.Account.Api.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAddressService _addressService;
        public AccountController(IAccountRepository accountRepository,IAddressService addressService)
        {
            _accountRepository = accountRepository;
            _addressService = addressService;
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

        // POST api/<ValuesController>
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

           _accountRepository.Add(account);

            return NoContent();
        }

        /* TODO
            - Create a REST API to get all the accounts
                For every account you need to use AddressService to load an address (City and PostCode)
                You can use AccountResponse class
                
            - Create a REST API to save an account 
                Call BalanceChecker to verify if you can save
                You can use AccountRequest class as a payload
         */

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
                var accountResponse = await GetAccountResponse(account);
                result.Add(accountResponse);
            }

            return result;
        }

        private async Task<AccountResponse> GetAccountResponse(AccountModel account)
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

            // TO DO : Throw custom exception
            throw new Exception("Invalid Balance");
        }
    }
}