using System.ComponentModel.DataAnnotations;

namespace DapperAPI_usingFunctionAndStoredProcedure.Models
{
    public class Employee
    {
        [Key] //sets EmployeeId as Primary key
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)] //used to specify that a property should be treated as an email address but doesnt validate its
        [EmailAddress] //Validates that the input is a proper email format.
        public string Email { get; set; }

        [Phone]
        [MinLength(10, ErrorMessage = "Phone number must be at least 10 digits.")]
        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 digits.")]
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
    }
}
