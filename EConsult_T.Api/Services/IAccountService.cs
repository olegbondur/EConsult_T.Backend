using EConsult_T.Api.Models;
using System.Threading.Tasks;

namespace EConsult_T.Api.Services
{
    public interface IAccountService
    {
        Task RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<string> CreateJwtTokenAsync(string email, string password);
    }
}
