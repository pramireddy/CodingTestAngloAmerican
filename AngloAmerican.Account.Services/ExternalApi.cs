using AngloAmerican.Account.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AngloAmerican.Account.Services
{
    public class ExternalApi : IExternalApi
    {
        private readonly List<string> _names = new List<string> { "Rene", "Kirk", "Escarole" };

        /// <summary>
        /// If the person is in the list and balance is greater than 10,000 return false
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="lastName"></param>
        /// <returns>true if balance is valid</returns>
        public bool CheckAccountBalance(int amount, string lastName)
        {
            return _names.Any(x => x.Contains(lastName) && amount <= 10000);
        }
    }
}