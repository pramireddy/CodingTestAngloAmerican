namespace AngloAmerican.Account.Services.Interfaces
{
    public interface IExternalApi
    {
        bool CheckAccountBalance(int amount, string lastName);
    }
}