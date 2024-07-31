using EmployeesInformation.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace EmployeesInformation.Data
{
    public static class EmployeesDataStore
    {
    
        private static readonly string ConnectionString = "Server=localhost;Database=EmployeesDatabase;User Id=sa;Password=Password@123;TrustServerCertificate=True;";

        public static List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, Name, JobTitle, Salary FROM Employee", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new Employee
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            JobTitle = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Salary = reader.GetDecimal(3)
                        };
                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }

        public static Employee GetEmployeeById(int id)
        {
            Employee employee = null;

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, Name, JobTitle, Salary FROM Employee WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            JobTitle = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Salary = reader.GetDecimal(3)
                        };
                    }
                }
            }

            return employee;
        }

        public static bool DeleteEmployee(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Employee WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public static bool InsertEmployee(string name, string jobTitle, decimal salary)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Employee (Name, JobTitle, Salary) VALUES (@Name, @JobTitle, @Salary)", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@JobTitle", (object)jobTitle ?? DBNull.Value);
                command.Parameters.AddWithValue("@Salary", salary);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
