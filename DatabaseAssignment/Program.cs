namespace DatabaseAssignment;

class Program
{
    static void Main(string[] args)
    {
        string windowsConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Archer;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        string linuxConnectionString = File.ReadAllText("/home/sami/mssql.txt");
        Archer archer = new(linuxConnectionString);
        

        //Customer jen = new("Jennifer", "Frandsen", "jenfran@mail.com");
        //Product cookingCream = new("Oatly", "Cream", 1.05m);
        //Order order = new(jen, cookingCream);
        
        //archer.AddCustomer(jen);
        //archer.AddProduct(cookingCream);
        //archer.AddOrder(order);

        Customer? customer = archer.GetCustomerById(2);
        if (customer != null)
        {
            Console.WriteLine(customer.FirstName);
        }
        
        Product? product = archer.GetProductById(2);
        if (product != null)
        {
            Console.WriteLine(product.Name);
        }
        
        Order? order = archer.GetOrderById(4);
        if (order != null)
        {
            Console.WriteLine(order.ProductId);
            Console.WriteLine(order.CustomerId);
        }
    }
}