namespace DatabaseAssignment;

class Program
{
    static void Main(string[] args)
    {
        string windowsConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Archer;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        string linuxConnectionString = File.ReadAllText("/home/sami/mssql.txt");
        Archer archer = new(linuxConnectionString);
        

        PrintContents();
        
        archer.DeleteCustomer(1);
        
        PrintContents();
        
        return;

        void PrintContents()
        {
            Console.WriteLine("Database contents:");
            List<Customer> customerList = archer.GetCustomers();

            foreach (Customer c in customerList)
            {
                Console.WriteLine(c.FirstName);
            }

            Console.WriteLine("------");
        
            List<Product> productList = archer.GetProducts();

            foreach (Product p in productList)
            {
                Console.WriteLine(p.Name + ", " + p.Category + " : " + p.Price + " eur");
            }
        
            Console.WriteLine("------");

            List<Order> orderList = archer.GetOrders();

            foreach (Order o in orderList)
            {
                Customer c = archer.GetCustomerById(o.CustomerId);
                Product p = archer.GetProductById(o.ProductId);

                Console.WriteLine($"{c.FirstName} {c.LastName} - {p.Name}");
            }
        }
    }
}