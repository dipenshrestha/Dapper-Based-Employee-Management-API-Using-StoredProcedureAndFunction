🛠️ Using Stored Procedures with Input Parameters in Dapper
This repository demonstrates how to execute SQL Server stored procedures with input parameters using Dapper. Follow these steps to integrate stored procedures in your Dapper-powered application.

📌 Steps to Use a Stored Procedure with Input Parameters
1️⃣ Create the Stored Procedure in Your Database
Create a stored procedure in your SQL Server database that accepts input parameters. Here's an example:

CREATE PROCEDURE MyStoredProcedure
    @InputParam1 INT,
    @InputParam2 INT
AS
BEGIN
    -- Your stored procedure logic here
    SELECT * FROM Customers WHERE CustomerId = @InputParam1 AND Status = @InputParam2;
END

2️⃣ Execute the Stored Procedure Using Dapper
Once the stored procedure is created, you can execute it using Dapper's Query<T>() or Execute() method.
Option 1: Using an Anonymous Object
This method is straightforward for simple parameter passing. Here's how you do it:

using (var connection = new SqlConnection(connectionString))
{
    // Define parameters
    var parameters = new DynamicParameters();
    parameters.Add("@InputParam1", inputParam1Value);
    parameters.Add("@InputParam2", inputParam2Value);
    
    // Execute the stored procedure
    var result = connection.Query<Customer>(
        "MyStoredProcedure",
        new { InputParam1 = inputParam1Value, InputParam2 = inputParam2Value },
        commandType: CommandType.StoredProcedure
    ).ToList();
}
Option 2: Using DynamicParameters
For more flexibility (e.g., adding output or optional parameters), you can use DynamicParameters:

using (var connection = new SqlConnection(connectionString))
{
    // Define parameters using DynamicParameters
    var parameters = new DynamicParameters();
    parameters.Add("@InputParam1", inputParam1Value);
    parameters.Add("@InputParam2", inputParam2Value);

    // Execute the stored procedure
    var result = connection.Query<Customer>(
        "MyStoredProcedure",
        parameters,
        commandType: CommandType.StoredProcedure
    ).ToList();
}

