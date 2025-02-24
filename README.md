# 🛠️ Using Stored Procedures with Input Parameters in Dapper

This repository demonstrates how to execute **SQL Server stored procedures** with input parameters using **Dapper**. Follow these steps to integrate stored procedures in your Dapper-powered application.

---

## 📌 Steps to Use a Stored Procedure with Input Parameters

### 1️⃣ **Create the Stored Procedure in Your Database**  

Create a stored procedure in your SQL Server database that accepts input parameters. Here's an example:

```sql
CREATE PROCEDURE MyStoredProcedure
    @InputParam1 INT,
    @InputParam2 INT
AS
BEGIN
    -- Your stored procedure logic here
    SELECT * FROM Customers WHERE CustomerId = @InputParam1 AND Status = @InputParam2;
END
```

### 2️⃣ **Use a querying or execute method with an anonymous types for your parameters**  
```sql
using(var connection = new SqlConnection(connectionString))
{
// Define parameters including your output parameters
var parameters = new DynamicParameters();
parameters.Add("@InputParam1", inputParam1Value);
parameters.Add("@InputParam2", inputParam2Value);

// Execute the stored procedure
var result = connection.Query<Customer>(
"MyStoredProcedure",
new { InputParam1 = inputParam1Value, InputParam2 = inputParam2Value},
commandType: CommandType.StoredProcedure
).ToList();
}
```
### 3️⃣ **Or create a DynamicParameters object and add parameter to it.**  
```sql
using(var connection = new SqlConnection(connectionString))
{
// Define parameters including your output parameters
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
```

