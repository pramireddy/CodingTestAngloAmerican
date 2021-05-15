namespace AngloAmerican.Account.Services.Interfaces
{
    public interface IBalanceChecker
    {
        bool Process(int amount, string lastName);
    }
}