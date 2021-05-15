using System.Threading.Tasks;

namespace AngloAmerican.Account.Services.Interfaces
{
    public interface IAddressService
    {
       Task<string> GetAddress();
    }
}