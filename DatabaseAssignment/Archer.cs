using Microsoft.Data.SqlClient;
using System.Data;

namespace DatabaseAssignment;

public class Archer(string connectionString)
{
    string _connectionString = connectionString;
    
    public void AddCustomer(Customer customer)
    {
        using SqlConnection connection = new (_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "AddCustomer";
        
        command.Parameters.Add("@FirstName", SqlDbType.VarChar, 32).Value = customer.FirstName;
        command.Parameters.Add("@LastName", SqlDbType.VarChar, 64).Value = customer.LastName;
        command.Parameters.Add("@Email", SqlDbType.VarChar, 64).Value = customer.Email;
        
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
        
        connection.Open();
        command.ExecuteNonQuery();
        
        customer.Id = (int)command.Parameters["@ID"].Value;
    }
    
    public void AddProduct(Product product)
    {
        using SqlConnection connection = new (_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "AddProduct";
        
        command.Parameters.Add("@Name", SqlDbType.VarChar, 32).Value = product.Name;
        command.Parameters.Add("@Category", SqlDbType.VarChar, 32).Value = product.Category;
        command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal)
        {
            Precision = 19,
            Scale = 4
        }).Value = product.Price;
        
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
        connection.Open();
        command.ExecuteNonQuery();
        
        product.Id = (int)command.Parameters["@ID"].Value;
    }

    public void AddOrder(Order order)
    {
        using SqlConnection connection = new (_connectionString);
        using SqlCommand command = connection.CreateCommand();
        
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "AddOrder";
        
        command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = order.CustomerId;
        command.Parameters.Add("@ProductId", SqlDbType.Int).Value = order.ProductId;
        
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
        connection.Open();
        command.ExecuteNonQuery();
        
        order.Id = (int)command.Parameters["@ID"].Value;
    }
}