using DapperAPI_usingFunctionAndStoredProcedure.Models;

namespace DapperAPI_usingFunctionAndStoredProcedure.IRepository
{
    public interface IDepartmentRepository
    {
        Task<bool> DepartmentExistsAsync(string name); //to check if the Department exists or not
        Task<bool> DepartmentIdExistsAsync(int id); //to checek if department id exists or not

        Task<IEnumerable<Department>> GetAllAsync(); //to get all the department details
        Task<Department> GetDepartmentById(int id); //to get one department list i.e. using id
        Task AddItemAsync(Department item);
        Task DeleteAsync(Department department);
        Task UpdateAsync(Department department); //no return type as we dont need in this
    }
}
