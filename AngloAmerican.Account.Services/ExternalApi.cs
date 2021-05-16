using AngloAmerican.Account.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AngloAmerican.Account.Services
{
    public class ExternalApi : IExternalApi
    {
        private readonly List<string> _lastNames = new List<string> { "Rene", "Kirk", "Escarole" };

        /// <summary>
        /// if thr person is not in the list return true
        /// If the person is in the list and balance is greater than 10,000 return false
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="lastName"></param>
        /// <returns>true if balance is valid</returns>
        public bool CheckAccountBalance(int amount, string lastName)
        {
            return !_lastNames.Any(x => x.Contains(lastName)) ||
                  _lastNames.Any(x => x.Contains(lastName) && amount <= 10000);
        }
    }
}