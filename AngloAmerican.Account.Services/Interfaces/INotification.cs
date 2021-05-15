namespace AngloAmerican.Account.Services.Interfaces
{
    public interface INotification
    {
        void SendEmail(string title, string body);
        void SendMessage(string message);
    }
}