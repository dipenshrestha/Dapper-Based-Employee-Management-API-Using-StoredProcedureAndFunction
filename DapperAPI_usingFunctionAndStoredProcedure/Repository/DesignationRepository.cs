using Dapper;
using DapperAPI_usingFunctionAndStoredProcedure.IRepository;
using DapperAPI_usingFunctionAndStoredProcedure.Models;
using System.Data;

namespace DapperAPI_usingFunctionAndStoredProcedure.Repository
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly DapperContext _context;
        public DesignationRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Designation>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            //using function
            return await connection.QueryAsync<Designation>("SELECT * FROM FgetDesignation()");
        }
        public async Task<Designation> GetDesignationById(int id)
        {
            using var connection = _context.CreateConnection();
            //using stored procedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", @id);
            var result = await connection.QueryAsync<Designation>("selectDesignationById", parameters, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault() ?? throw new KeyNotFoundException($"Designation with ID {id} not found.");
        }
        public async Task<bool> DesignationExistsAsync(string name)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
            SELECT COUNT(*) 
            FROM Designation 
            WHERE DesignationName = @name";
            int count = await connection.ExecuteScalarAsync<int>(sql, new { name });
            return count > 0;
        }
        public async Task<bool> DesignationIdExistsAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
            SELECT COUNT(*) 
            FROM Designation 
            WHERE DesignationId = @id";
            int count = await connection.ExecuteScalarAsync<int>(sql, new { id });
            return count > 0;
        }
        public async Task AddItemAsync(Designation item)
        {
            using var connection = _context.CreateConnection();
            //Check if employee already exists
            bool exists = await DesignationExistsAsync(item.DesignationName);
            if (exists)
            {
                throw new InvalidOperationException("An Department with the same Name already exists.");
            }
            
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("name",item.DesignationName);
            await connection.ExecuteAsync("insertDesignationData",parameters,commandType:CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(Designation designation)
        {
            using var connection = _context.CreateConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id",designation.DesignationId);
            await connection.ExecuteAsync("deleteDesignationData",parameters,commandType:CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(Designation designation)
        {
            using var connection = _context.CreateConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id",designation.DesignationId);
            parameters.Add("name", designation.DesignationName);
            await connection.ExecuteAsync("updateDesignationData",parameters,commandType:CommandType.StoredProcedure);
        }
    }
}
