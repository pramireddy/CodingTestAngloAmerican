using System.Collections.Generic;

namespace AngloAmerican.Account.Services
{
    // TODO: Improve and make the code more readable.
    public class ExternalApi
    {
        private List<string> _names = new List<string> {"Rene", "Kirk", "Escarole"};

        // returns true if balance is valid
        public bool CheckAccountBalance(int amount, string lastName)
        {
            bool isFalse = false;

            // if the person is in the list and balance is greater than 10,000 return false
            foreach (var n in _names)
            {
                isFalse = true;
                if (n == lastName)
                {
                    if (amount > 10000)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                return isFalse;
            }

            return isFalse;
        }
    }
}