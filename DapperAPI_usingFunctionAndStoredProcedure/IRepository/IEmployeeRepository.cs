using DapperAPI_usingFunctionAndStoredProcedure.Models;

namespace DapperAPI_usingFunctionAndStoredProcedure.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task AddItemAsync(Employee obj);
        Task UpdateAsync(Employee obj);
        Task DeleteAsync(int id);
    }
}
