using System.Collections.Generic;
using System.Threading.Tasks;
namespace AngloAmerican.Account.Services.Interfaces
{
    public interface IAccountRepository
    {
        Task Add(AccountModel accountModel);
        List<AccountModel> GetAllAccounts();
    }
}
