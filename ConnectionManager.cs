using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Farma_plus
{
    public class ConnectionManager
    {
        private readonly IConfiguration _config;

        public ConnectionManager(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IDbConnection GetConnection()
        {
            var conn = new OracleConnection(_config.GetConnectionString("OrConn"));
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
            {
                conn.Close();
            }
        }
    }
}
