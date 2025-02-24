using System.ComponentModel.DataAnnotations;

namespace DapperAPI_usingFunctionAndStoredProcedure.Models
{
    public class Department
    {
        //Data Annotations
        [Key] //Marks a property as the Primary Key in EF Core.
        public int DepartmentId { get; set; }

        [Required] //Ensures that the field cannot be null or empty
        [StringLength(50, MinimumLength = 3)] //Sets the minimum and maximum length for a string property.
        public string DepartmentName { get; set; }
    }
}
