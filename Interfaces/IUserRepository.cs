using Farma_plus.Models;
using System.Threading.Tasks;

namespace Farma_plus.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByIdAsync(string username, string password);
    }
}
