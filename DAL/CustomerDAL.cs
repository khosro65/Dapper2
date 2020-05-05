using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using COM;


namespace DAL
{
    public class CustomerDAL
    {
        string sqlConnection = System.Configuration.ConfigurationManager.ConnectionStrings["Dapper"].ConnectionString;

        public int AddCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(sqlConnection))
            {
                connection.Open();
                var affectedRow = connection.Execute("INSERT INTO dbo.Customer (Name, Age, Tel, Address) VALUES(@name, @age, @tel, @address)",
                    new { Name = customer.Name, Age = customer.Age, Tel = customer.Tel, Address = customer.Address });
                connection.Close();
                return affectedRow;
            }
        }
        public int UpdateCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(sqlConnection))
            {
                connection.Open();
                var affectedRow = connection.Execute("UPDATE dbo.Customer SET Name = @name, Age = @age, Tel = @tel, Address = @address WHERE Id = @id)",
                    new { Id = customer.Id, Name = customer.Name, Age = customer.Age, Tel = customer.Tel, Address = customer.Address });
                connection.Close();
                return affectedRow;
            }
        }

        public int DeleteCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(sqlConnection))
            {
                connection.Open();
                var affectedRow = connection.Execute("DELETE dbo.Customer WHERE Id = @id)",
                    new { Id = customer.Id });
                connection.Close();
                return affectedRow;
            }
        }
        public List<Customer> GetAllCustomer()
        {
            List<Customer> customers = new List<Customer>();
            using (var connection = new SqlConnection(sqlConnection))
            {
                connection.Open();
                customers = connection.Query<Customer>("SELECT Id, Name, Age, Tel, Address FROM dbo.Customer").ToList();
                connection.Close();
            }
            return customers;
        }
    }
}
