using AngloAmerican.Account.Services.Interfaces;
using System;

namespace AngloAmerican.Account.Services
{
    /* TODO
        - Refactor the class and add Unit Tests
    */

    public class BalanceChecker : IBalanceChecker
    {
        public bool Process(int amount, Notification notification, ExternalApi eA, string lastName)
        {
            var emailTitle =
                DateTime.Now.Day < 15 ? "<h1>Info about days till Middle of the month</h1>"
                                      : "<h1>Info about days till End of the month</h1>";
            var emailBody = "<p>Body placeholder<p>";
            var m = emailTitle + "\n" + emailBody;

            if (amount < 10000)
            {
                notification.SendEmail(emailTitle, emailBody);
            }
            if (amount > 10000)
            {
                notification.SendMessage(m);
                return eA.CheckAccountBalance(amount, lastName);
            }

            return true;
        }
    }
}