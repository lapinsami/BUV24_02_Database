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
}