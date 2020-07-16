using System.Threading.Tasks;
using mapp.api.Models;

namespace mapp.api.Data
{
    public interface IAuthRepository
    {
         Task<Users> Register(Users users, string password);
         Task<Users> LogIn(string userName, string password);
         Task<bool> UserExists(string userName);
    }
}