using DapperAPI_usingFunctionAndStoredProcedure.Models;

namespace DapperAPI_usingFunctionAndStoredProcedure.IRepository
{
    public interface IDesignationRepository
    {
        Task<bool> DesignationExistsAsync(string name); //to check if the Department exists or not
        Task<bool> DesignationIdExistsAsync(int id);
        Task<IEnumerable<Designation>> GetAllAsync();
        Task<Designation> GetDesignationById(int id);
        Task AddItemAsync(Designation item);
        Task DeleteAsync(Designation department);
        Task UpdateAsync(Designation department);
    }
}
