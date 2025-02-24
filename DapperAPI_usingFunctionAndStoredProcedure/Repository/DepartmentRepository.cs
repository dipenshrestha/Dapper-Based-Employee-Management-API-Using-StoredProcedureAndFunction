using Dapper;
using DapperAPI_usingFunctionAndStoredProcedure.IRepository;
using DapperAPI_usingFunctionAndStoredProcedure.Models;
using System.Data;

namespace DapperAPI_usingFunctionAndStoredProcedure.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DapperContext _context;

        public DepartmentRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            //using Stored procedure
            //return await connection.QueryAsync<Department>("selectDepartment",commandType: CommandType.StoredProcedure);

            //using sql function
            return await connection.QueryAsync<Department>("SELECT * FROM FgetDepartment()");
        }

        //It returns a Task<Department>, meaning it will return a Department object asynchronously
        public async Task<Department> GetDepartmentById(int id)
        {
            using var connection = _context.CreateConnection();

            //Set up DynamicParameters object to pass parameters  
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", @id);
            var result = await connection.QueryAsync<Department>("selectDepartmentById", parameters, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault() ?? throw new($"Department with ID {id} not found.");
        }
        public async Task<bool> DepartmentExistsAsync(string name)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
            SELECT COUNT(*) 
            FROM Department 
            WHERE DepartmentName = @name";

            int count = await connection.ExecuteScalarAsync<int>(sql, new { name });

            return count > 0;
        }
        public async Task<bool> DepartmentIdExistsAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
            SELECT COUNT(*) 
            FROM Department 
            WHERE DepartmentId = @id";

            int count = await connection.ExecuteScalarAsync<int>(sql, new { id });
            return count > 0;
        }
        public async Task AddItemAsync(Department item)
        {
            using var connection = _context.CreateConnection();
            //Check if name already exists or not
            bool exists = await DepartmentExistsAsync(item.DepartmentName);
            if (exists)
            {
                throw new("An Department with Name already exists.");
            }
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("name", item.DepartmentName);
            await connection.ExecuteAsync("insertDepartmentData", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task DeleteAsync(Department department)
        {
            bool exists = await DepartmentIdExistsAsync(department.DepartmentId);
            if (!exists)
            {
                throw new("An Department with Id doesnt exists.");
            }
            using var connection = _context.CreateConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", department.DepartmentId);
            await connection.ExecuteAsync("deleteDepartmentData", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(Department department)
        {
            using var connection = _context.CreateConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", department.DepartmentId);
            parameters.Add("name", department.DepartmentName);
            await connection.ExecuteAsync("updateDepartmentData", parameters, commandType: CommandType.StoredProcedure);
        }

    }
}
