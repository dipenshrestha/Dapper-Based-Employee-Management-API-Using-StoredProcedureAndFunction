using Dapper;
using DapperAPI_usingFunctionAndStoredProcedure.IRepository;
using DapperAPI_usingFunctionAndStoredProcedure.Models;
using System.Data;

namespace DapperAPI_usingFunctionAndStoredProcedure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;
        private readonly IDepartmentRepository _depart;
        private readonly IDesignationRepository _desig;

        public EmployeeRepository(DapperContext context, IDepartmentRepository depart, IDesignationRepository desig)
        {
            _context = context;
            _depart = depart;
            _desig = desig;
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            //using sql function
            var sql = $"SELECT * FROM FgetEmployee()";
            return await connection.QueryAsync<Employee>(sql);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            using var connection = _context.CreateConnection();
            //using sql stored procedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);
            var result = await connection.QueryAsync<Employee>("selectEmployeeById",parameters,commandType:CommandType.StoredProcedure);
            return result.FirstOrDefault() ?? throw new KeyNotFoundException($"Employee with ID {id} not found.");
        }

        public async Task AddItemAsync(Employee obj)
        {
            using var connection = _context.CreateConnection();
            //Check if employee with the id already exists
            bool hasDepartId = await _depart.DepartmentIdExistsAsync(obj.DepartmentId);
            bool hasDesigId = await _desig.DesignationIdExistsAsync(obj.DesignationId);
            //if anyone is false or both is false then this statement runs
            if (!hasDepartId || !hasDesigId)
            {
                throw new InvalidOperationException("An Department or Designation Id doesnot exists.");
            }
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("fname",obj.FirstName);
            parameters.Add("lname",obj.LastName);
            parameters.Add("email",obj.Email);
            parameters.Add("phnum",obj.PhoneNumber);
            parameters.Add("departId",obj.DepartmentId);
            parameters.Add("desigId",obj.DesignationId);
            await connection.ExecuteAsync("insertEmployeeData",parameters,commandType:CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _context.CreateConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id",id);
            await connection.ExecuteAsync("deleteEmployeeData",parameters,commandType:CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(Employee obj)
        {
            using var connection = _context.CreateConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id",obj.EmployeeId);
            parameters.Add("fname", obj.FirstName);
            parameters.Add("lname", obj.LastName);
            parameters.Add("email", obj.Email);
            parameters.Add("phnum", obj.PhoneNumber);
            parameters.Add("departId", obj.DepartmentId);
            parameters.Add("desigId", obj.DesignationId);
            await connection.ExecuteAsync("updateEmployeeData",parameters,commandType:CommandType.StoredProcedure);
        }
    }
}
