using Microsoft.Data.SqlClient;
using System.Data;

namespace DatabaseAssignment;

public class Archer(string connectionString)
{
    string _connectionString = connectionString;
    
    /// <summary>
    /// Adds a new customer
    /// </summary>
    /// <param name="customer">The customer you want to add</param>
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
    
    /// <summary>
    /// Deletes the customer with the specified ID
    /// </summary>
    /// <param name="customerId">The primary key ID of the customer</param>
    public void DeleteCustomer(int customerId)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();
        
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "DeleteCustomer";
        
        command.Parameters.Add("@ID", SqlDbType.Int).Value = customerId;
        
        connection.Open();
        command.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Updates the email of the customer with the specified ID
    /// </summary>
    /// <param name="customerId">The primary key ID of the customer</param>
    /// <param name="newEmail">The new email for the customer</param>
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
    
    /// <summary>
    /// Returns a customer matching the specified ID.
    /// </summary>
    /// <param name="id">The primary key ID of the customer</param>
    /// <returns>The requested customer</returns>
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
    
    /// <summary>
    /// Returns all the customers
    /// </summary>
    /// <returns>a list of the customers</returns>
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
    
    /// <summary>
    /// Adds a new product
    /// </summary>
    /// <param name="product">The product you want to add</param>
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
    
    /// <summary>
    /// Deletes the product with the specified ID
    /// </summary>
    /// <param name="productId">The primary key ID of the product</param>
    public void DeleteProduct(int productId)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();
        
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "DeleteProduct";
        
        command.Parameters.Add("@ID", SqlDbType.Int).Value = productId;
        
        connection.Open();
        command.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Updates the specified product
    /// </summary>
    /// <param name="productId">The ID of the product you want to update</param>
    /// <param name="newName">The new name for the product</param>
    /// <param name="newCategory">The new category for the product</param>
    /// <param name="newPrice">The new price for the product</param>
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
    
    /// <summary>
    /// Updates the price of the specified product
    /// </summary>
    /// <param name="productId">The ID of the product you want to update</param>
    /// <param name="newPrice">The new price for the product</param>
    public void UpdateProductPrice(int productId, decimal newPrice)
    {
        Product product = GetProductById(productId);

        if (product == null)
        {
            return;
        }
        
        UpdateProduct(productId, product.Name, product.Category, newPrice);
    }
    
    /// <summary>
    /// Returns a product matching the specified ID.
    /// </summary>
    /// <param name="id">The primary key ID of the product</param>
    /// <returns>The requested product</returns>
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
    
    /// <summary>
    /// Returns all the products
    /// </summary>
    /// <returns>a list of the products</returns>
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
    
    /// <summary>
    /// Adds a new order
    /// </summary>
    /// <param name="order">The order you want to add</param>
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
    
    /// <summary>
    /// Deletes the order with the specified ID
    /// </summary>
    /// <param name="orderId">The primary key ID of the order</param>
    public void DeleteOrder(int orderId)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();
        
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "DeleteOrder";
        
        command.Parameters.Add("@ID", SqlDbType.Int).Value = orderId;
        
        connection.Open();
        command.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Updates the product in a specifed order
    /// </summary>
    /// <param name="orderId">The ID of the order you want to update</param>
    /// <param name="newProductId">The ID of the product you want to put in the order</param>
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
    
    /// <summary>
    /// Returns an order matching the specified ID.
    /// </summary>
    /// <param name="id">The primary key ID of the order</param>
    /// <returns>The requested order</returns>
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
    
    /// <summary>
    /// Returns all the orders
    /// </summary>
    /// <returns>a list of the orders</returns>
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