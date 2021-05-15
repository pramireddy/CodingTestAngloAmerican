using AngloAmerican.Account.Services.Interfaces;

namespace AngloAmerican.Account.Services
{
    public class Notification : INotification
    {
        public void SendEmail(string title, string body)
        {
            // it sends an email
        }

        public void SendMessage(string message)
        {
            // it sends a message
        }
    }
}