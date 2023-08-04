using Dapper;
using Farma_plus.Interfaces;
using Farma_plus.Models;
using Microsoft.Extensions.Configuration;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Farma_plus.Repositories
{
    public class TratamientoRepository : ITratamientoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionManager _connectionManager;
        public TratamientoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionManager = new ConnectionManager(_configuration);
        }

        public async Task<int> AddAsync(Tratamiento entity)
        {
            // Set the time to the current moment

            IDbConnection conn = _connectionManager.GetConnection();
            // Basic SQL statement to insert a product into the products table
            var sql = "Insert into PC_TRATAMIENTO (Name,Description,Barcode,Price,Added) VALUES (@Name,@Description,@Barcode,@Price,@Added)";

            // Pass the product object and the SQL statement into the Execute function (async)
            var result = await conn.ExecuteAsync(sql, entity);

            _connectionManager.CloseConnection(conn);

            return result;
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<Tratamiento>> GetAllAsync()
        {
            IDbConnection conn = _connectionManager.GetConnection();

            var sql = "SELECT * FROM PC_TRATAMIENTOS";

            // Map all products from database to a list of type Product defined in Models.
            // this is done by using Async method which is also used on the GetByIdAsync
            var result = await conn.QueryAsync<Tratamiento>(sql);

            _connectionManager.CloseConnection(conn);

            return result.ToList();
        }

        public async Task<Tratamiento> GetByIdAsync(int id)
        {
            IDbConnection conn = _connectionManager.GetConnection();
            var sql = "SELECT * FROM Products WHERE Id = @Id";

            var result = await conn.QuerySingleOrDefaultAsync<Tratamiento>(sql, new { Id = id });

            _connectionManager.CloseConnection(conn);
            return result;
        }

        public Task<int> UpdateAsync(Tratamiento entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
