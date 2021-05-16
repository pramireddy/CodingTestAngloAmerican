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
        /// <returns>true if person is in list and the balance is valid</returns>
        public bool CheckAccountBalance(int amount, string lastName)
        {
            bool isFalse = false;

            foreach (var n in _names)
            {
                isFalse = true;
                if (n == lastName)
                {
                    return amount <= 10000;
                }
            }
            return isFalse;
        }
    }
}