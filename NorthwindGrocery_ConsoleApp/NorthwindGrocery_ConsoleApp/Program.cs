using System;
using Microsoft.Data.SqlClient; // Updated namespace

namespace NorthwindGrocery_ConsoleApp
{
    class Program
    {
      
       // static string connectionString = "Server=WSAMZN-7OSQGH5B;Database=demo;Trusted_Connection=True;TrustServerCertificate=True;";
        static string connectionString = "Server=localhost;Database=demo;User Id=sa;Password=Password@123;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1 - List all categories");
                Console.WriteLine("2 - List products in a specific category");
                Console.WriteLine("3 - List products in a price range");
                Console.WriteLine("4 - List all suppliers");
                Console.WriteLine("5 - List products from a specific supplier");
                Console.WriteLine("6 - Quit");
                Console.Write("Please select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListCategories();
                        break;
                    case "2":
                        ListProductsInCategory();
                        break;
                    case "3":
                        ListProductsInPriceRange();
                        break;
                    case "4":
                        ListSuppliers();
                        break;
                    case "5":
                        ListProductsBySupplier();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void ListCategories()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Categories:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["CategoryID"]}: {reader["CategoryName"]}");
                }
            }
        }

        static void ListProductsInCategory()
        {
            Console.Write("Enter the category name: ");
            string categoryName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.ProductID, p.ProductName
                    FROM Products p
                    JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE c.CategoryName = @CategoryName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Products in Category:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProductID"]}: {reader["ProductName"]}");
                }
            }
        }

        static void ListProductsInPriceRange()
        {
            Console.Write("Enter minimum price: ");
            decimal minPrice = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter maximum price: ");
            decimal maxPrice = Convert.ToDecimal(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT ProductID, ProductName
                    FROM Products
                    WHERE UnitPrice BETWEEN @MinPrice AND @MaxPrice";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MinPrice", minPrice);
                command.Parameters.AddWithValue("@MaxPrice", maxPrice);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Products in Price Range:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProductID"]}: {reader["ProductName"]}");
                }
            }
        }

        static void ListSuppliers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT SupplierID, CompanyName FROM Suppliers";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Suppliers:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["SupplierID"]}: {reader["CompanyName"]}");
                }
            }
        }

        static void ListProductsBySupplier()
        {
            Console.Write("Enter the supplier ID: ");
            int supplierId = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT ProductID, ProductName
                    FROM Products
                    WHERE SupplierID = @SupplierID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SupplierID", supplierId);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Products by Supplier:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProductID"]}: {reader["ProductName"]}");
                }
            }
        }
    }
}
