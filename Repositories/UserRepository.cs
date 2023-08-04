using Dapper;
using Farma_plus.Interfaces;
using Farma_plus.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Farma_plus.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionManager _connectionManager;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionManager = new ConnectionManager(configuration);
        }
        public Task<int> AddAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<User>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(string username, string password)
        {
            IDbConnection conn = _connectionManager.GetConnection();
            var sql = "SELECT USUARIO," +
                " CONTRASEÑA" +
                " FROM USUARIOS_FARMACIA " +
                $" WHERE USUARIO = '{username}'"+
                $" AND CONTRASEÑA = '{password}'";

            var result = await conn.QuerySingleOrDefaultAsync<User>(sql);

            _connectionManager.CloseConnection(conn);
            return result;
        }

        public Task<int> UpdateAsync(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
