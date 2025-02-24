using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace DapperAPI_usingFunctionAndStoredProcedure.Models
{
    public class DapperContext
    {
        private readonly string? _connectionstring;

        public DapperContext(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionstring);
    }
}
