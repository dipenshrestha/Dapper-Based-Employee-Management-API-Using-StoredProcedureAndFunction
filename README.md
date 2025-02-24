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

# Stored Procedure Commands for Department, Designation, and Employee

This repository provides a set of **SQL stored procedures** for managing data operations (Select, Insert, Update, Delete) on the `Department`, `Designation`, and `Employee` tables. Follow the instructions below to execute these procedures in your database.

---

## 📌 Commands for Stored Procedures

### 1️⃣ **Select Department**
Create the following stored procedure to select all records from the `Department` table:
```sql
CREATE PROCEDURE selectDepartment
AS
BEGIN
    SELECT * FROM Department;
END
```

2️⃣ Select Department using Function
You can also use a function to retrieve all departments:
```sql
-- Create Function
CREATE FUNCTION FgetDepartment ()
RETURNS TABLE AS
RETURN (
    SELECT * FROM Department
);
```
To Call the Function
```sql
SELECT * FROM FgetDepartment();
```

3️⃣ Select Department by ID
Create this stored procedure to select a department by its DepartmentId:
```sql
CREATE PROCEDURE selectDepartmentById
    @id INT
AS
BEGIN
    SELECT * FROM Department WHERE DepartmentId = @id;
END
```

4️⃣ Select Department by Name
To select a department by its name:
```sql
CREATE PROCEDURE selectDepartmentByName
    @name NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Department WHERE DepartmentName = @name;
END
```

5️⃣ Insert Department Data
This stored procedure inserts a new department into the Department table:
```sql
CREATE PROCEDURE insertDepartmentData
    @name NVARCHAR(100)
AS
BEGIN
    INSERT INTO Department (DepartmentName) 
    VALUES (@name);
END
```

6️⃣ Update the Department Details
To update the details of a department:
```sql
CREATE PROCEDURE updateDepartmentData
    @id INT,
    @name NVARCHAR(100)
AS
BEGIN
    UPDATE Department
    SET DepartmentName = @name
    WHERE DepartmentId = @id;
END
```
To execute this procedure
```sql
EXEC updateDepartmentData @id = 2, @name = 'Frontend Developer';
```

7️⃣ Delete the Department Details
This stored procedure deletes a department by its DepartmentId:
```sql
CREATE PROCEDURE deleteDepartmentData
    @id INT
AS
BEGIN
    DELETE FROM Department WHERE DepartmentId = @id;
END
```
To execute the procedure:
```sql
EXEC deleteDepartmentData @id = 5;
```

8️⃣ Select Designation
Use this stored procedure to select all records from the Designation table:
```sql
CREATE PROCEDURE selectDesignation
AS
BEGIN
    SELECT * FROM Designation;
END
```

9️⃣ Select Designation using Function
Create a function to select all designations:
```sql
-- Create Function
CREATE FUNCTION FgetDesignation ()
RETURNS TABLE AS
RETURN (
    SELECT * FROM Designation
);
```
To Call the Function
```sql
SELECT * FROM FgetDesignation();
```

🔟 Select Designation by ID
This stored procedure selects a designation by its DesignationId:
```sql
CREATE PROCEDURE selectDesignationById
    @id INT
AS
BEGIN
    SELECT * FROM Designation WHERE DesignationId = @id;
END
```

1️⃣1️⃣ Select Designation by Name
Select a designation by its name using this stored procedure:
```sql
CREATE PROCEDURE selectDesignationByName
    @name NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Designation WHERE DesignationName = @name;
END
```

1️⃣2️⃣ Insert Designation Data
Insert a new designation into the Designation table:
```sql
CREATE PROCEDURE insertDesignationData
    @name NVARCHAR(100)
AS
BEGIN
    INSERT INTO Designation (DesignationName)
    VALUES (@name);
END
```

1️⃣3️⃣ Update the Designation Details
To update the details of a designation:
```sql
CREATE PROCEDURE updateDesignationData
    @id INT,
    @name NVARCHAR(100)
AS
BEGIN
    UPDATE Designation
    SET DesignationName = @name
    WHERE DesignationId = @id;
END
```
--To execute the procedure:
```sql
EXEC updateDesignationData @id = 5, @name = 'Mid Level';
```

1️⃣4️⃣ Delete Designation Details
Delete a designation by its DesignationId:
```sql
CREATE PROCEDURE deleteDesignationData
    @id INT
AS
BEGIN
    DELETE FROM Designation WHERE DesignationId = @id;
END
```
To execute the procedure:
```sql
EXEC deleteDesignationData @id = 6;
```

1️⃣5️⃣ Select Employee
This stored procedure selects all records from the Employee table:
```sql
CREATE PROCEDURE selectEmployee
AS
BEGIN
    SELECT * FROM Employee;
END
```

1️⃣6️⃣ Select Employee Function
Use a function to retrieve all employee records:
```sql
CREATE FUNCTION FgetEmployee ()
RETURNS TABLE AS
RETURN (
    SELECT * FROM Employee
);
```

1️⃣7️⃣ Select Employee By ID
This procedure selects an employee by their EmployeeId:
```sql
CREATE PROCEDURE selectEmployeeById
    @id INT
AS
BEGIN
    SELECT * FROM Employee WHERE EmployeeId = @id;
END
```

1️⃣8️⃣ Insert Employee Data
Insert new employee data into the Employee table:
```sql
CREATE PROCEDURE insertEmployeeData
    @fname NVARCHAR(50),
    @lname NVARCHAR(50),
    @email NVARCHAR(255),
    @phnum VARCHAR(15),
    @departId INT,
    @desigId INT
AS
BEGIN
    INSERT INTO Employee (FirstName, LastName, Email, PhoneNumber, DepartmentId, DesignationId)
    VALUES (@fname, @lname, @email, @phnum, @departId, @desigId);
END
```

1️⃣9️⃣ Update Employee Details
This stored procedure updates employee details:
```sql
CREATE PROCEDURE updateEmployeeData
    @id INT,
    @fname NVARCHAR(50),
    @lname NVARCHAR(50),
    @email NVARCHAR(255),
    @phnum VARCHAR(15),
    @departId INT,
    @desigId INT
AS
BEGIN
    UPDATE Employee
    SET FirstName = @fname, LastName = @lname, Email = @email, PhoneNumber = @phnum, DepartmentId = @departId, DesignationId = @desigId
    WHERE EmployeeId = @id;
END
```
To execute the procedure:
```sql
EXEC updateEmployeeData @id = 2, @fname = 'Ram', @lname = 'Sima', @email = 'ramsima@gmail.com', @phnum = '9874444637', @departId = 1, @desigId = 2;
```

2️⃣0️⃣ Delete Employee Details
This stored procedure deletes an employee by their EmployeeId:
```sql
CREATE PROCEDURE deleteEmployeeData
    @id INT
AS
BEGIN
    DELETE FROM Employee WHERE EmployeeId = @id;
END
```
To execute the procedure:
```sql
EXEC deleteEmployeeData @id = 2;
```

