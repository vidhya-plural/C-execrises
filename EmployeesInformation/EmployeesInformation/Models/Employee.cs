namespace EmployeesInformation.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public decimal Salary { get; set; } = 0;
    }
}
