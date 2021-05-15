using AngloAmerican.Account.Services.Interfaces;
using System;

namespace AngloAmerican.Account.Services
{
    public class BalanceChecker : IBalanceChecker
    {
        private readonly IExternalApi _externalApi;
        private readonly INotification _notification;

        public BalanceChecker(IExternalApi externalApi, INotification notification)
        {
            _externalApi = externalApi;
            _notification = notification;
        }

        public bool Process(int amount, string lastName)
        {
            var emailTitle =
                DateTime.Now.Day < 15 ? "<h1>Info about days till Middle of the month</h1>"
                                      : "<h1>Info about days till End of the month</h1>";
            var emailBody = "<p>Body placeholder<p>";
            if (amount < 10000)
            {
                _notification.SendEmail(emailTitle, emailBody);
            }

            if (amount > 10000)
            {
                _notification.SendMessage($"{emailTitle}\n{emailBody}");
                return _externalApi.CheckAccountBalance(amount, lastName);
            }

            return true;
        }
    }
}