namespace AngloAmerican.Account.Services.Interfaces
{
    public interface IBalanceChecker
    {
        bool Process(int amount, Notification notification, ExternalApi eA, string lastName);
    }
}