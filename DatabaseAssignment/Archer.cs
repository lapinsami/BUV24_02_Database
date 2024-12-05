using Microsoft.Data.SqlClient;
using System.Data;

namespace DatabaseAssignment;

public class Archer(string connectionString)
{
    string _connectionString = connectionString;
    
    public void AddCustomer(Customer customer)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "AddCustomer";
        
        command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 32).Value = customer.FirstName;
        command.Parameters.Add("@LastName", SqlDbType.NVarChar, 64).Value = customer.LastName;
        command.Parameters.Add("@Email", SqlDbType.NVarChar, 64).Value = customer.Email;
        
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
        
        connection.Open();
        command.ExecuteNonQuery();
        
        customer.Id = (int)command.Parameters["@ID"].Value;
    }
    
    public void UpdateCustomerEmail(int customerId, string newEmail)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "UpdateCustomerEmail";
        
        command.Parameters.Add("@newEmail", SqlDbType.NVarChar, 64).Value = newEmail;
        command.Parameters.Add("@ID", SqlDbType.Int).Value = customerId;
        
        connection.Open();
        command.ExecuteNonQuery();
    }
    
    public void AddProduct(Product product)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "AddProduct";
        
        command.Parameters.Add("@Name", SqlDbType.NVarChar, 32).Value = product.Name;
        command.Parameters.Add("@Category", SqlDbType.NVarChar, 32).Value = product.Category;
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
    
    public void UpdateProduct(int productId, string newName, string newCategory, decimal newPrice)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "UpdateProduct";
        
        command.Parameters.Add("@newName", SqlDbType.NVarChar, 32).Value = newName;
        command.Parameters.Add("@newCategory", SqlDbType.NVarChar, 32).Value = newCategory;
        command.Parameters.Add(new SqlParameter("@newPrice", SqlDbType.Decimal)
        {
            Precision = 19,
            Scale = 4
        }).Value = newPrice;
        
        
        command.Parameters.Add("@ID", SqlDbType.Int).Value = productId;
        
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void UpdateProductPrice(int productId, decimal newPrice)
    {
        Product product = GetProductById(productId);

        if (product == null)
        {
            return;
        }
        
        UpdateProduct(productId, product.Name, product.Category, newPrice);
    }

    public void AddOrder(Order order)
    {
        using SqlConnection connection = new(_connectionString);
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
    
    public void UpdateOrderProduct(int orderId, int newProductId)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "UpdateOrderProduct";
        
        command.Parameters.Add("@newProductId", SqlDbType.Int).Value = newProductId;
        command.Parameters.Add("@ID", SqlDbType.Int).Value = orderId;
        
        connection.Open();
        command.ExecuteNonQuery();
    }
    
    public Customer? GetCustomerById(int id)
    {
        Customer? customer = null;

        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"select * from Customer where ID = @ID";
        command.CommandType = CommandType.Text;
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            customer = new Customer(
                (string)reader["FirstName"],
                (string)reader["LastName"],
                (string)reader["Email"]
            )
            {
                Id = id
            };
        }

        return customer;
    }
    
    public Product? GetProductById(int id)
    {
        Product? product = null;

        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"select * from Product where ID = @ID";
        command.CommandType = CommandType.Text;
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            product = new Product(
                (string)reader["Name"],
                (string)reader["Category"],
                (decimal)reader["Price"]
            )
            {
                Id = id
            };
        }

        return product;
    }
    
    public Order? GetOrderById(int id)
    {
        Order? order = null;

        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"select * from [Order] where ID = @ID";
        command.CommandType = CommandType.Text;
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            order = new Order(
                (int)reader["CustomerID"],
                (int)reader["ProductID"]
            )
            {
                Id = id
            };
        }

        return order;
    }
    
    public List<Customer> GetCustomers()
    {
        List<Customer> customers = [];

        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"select * from Customer";
        command.CommandType = CommandType.Text;
        
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows) return customers;

        while (reader.Read())
        {
            Customer customer = new(
                (string)reader["FirstName"],
                (string)reader["LastName"],
                (string)reader["Email"]
            )
            {
                Id = (int)reader["ID"]
            };

            customers.Add(customer);
        }

        return customers;
    }
    
    public List<Product> GetProducts()
    {
        List<Product> products = [];

        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"select * from Product";
        command.CommandType = CommandType.Text;
        
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows) return products;

        while (reader.Read())
        {
            Product product = new(
                (string)reader["Name"],
                (string)reader["Category"],
                (decimal)reader["Price"]
            )
            {
                Id = (int)reader["ID"]
            };

            products.Add(product);
        }

        return products;
    }
    
    public List<Order> GetOrders()
    {
        List<Order> orders = [];

        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"select * from [Order]";
        command.CommandType = CommandType.Text;
        
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows) return orders;

        while (reader.Read())
        {
            Order order = new(
                (int)reader["CustomerID"],
                (int)reader["ProductID"]
            )
            {
                Id = (int)reader["ID"]
            };

            orders.Add(order);
        }

        return orders;
    }
}