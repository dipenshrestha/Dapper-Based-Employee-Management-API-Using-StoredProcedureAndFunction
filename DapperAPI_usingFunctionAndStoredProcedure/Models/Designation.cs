using System.ComponentModel.DataAnnotations;

namespace DapperAPI_usingFunctionAndStoredProcedure.Models
{
    public class Designation
    {
        //Data Annotations
        [Key] //Marks a property as the Primary Key in EF Core.
        public int DesignationId { get; set; }

        [Required] //Ensures that the field cannot be null or empty
        [StringLength(50, MinimumLength = 3)] //Sets the minimum and maximum length for a string property.

        //✅ Use string? if null is a valid state (e.g., optional fields).
        //✅ Use string (non-nullable) when you want to enforce that the property always has a value.
        public string DesignationName { get; set; }
        //or set a default value
        //in our case this field cannot be empty so we dont put anything
        //public string DesignationName { get; set; } = string.Empty;
    }
}
