using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AngloAmerican.Account.Api.Controllers
{
    [ApiController]
    [Route("accounttype")]
    public class AccountTypeController : ControllerBase
    {
        // GET: /accounttype
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        public IEnumerable<AccountType> Get()
        {
            return GetTypes();
        }

        private List<AccountType> GetTypes()
                                => new List<AccountType>
                                {
                                    new AccountType {Id = 1, Name = "Bronze"},
                                    new AccountType {Id = 2, Name = "Silver"},
                                    new AccountType {Id = 3, Name = "Gold"}
                                };
    }
}