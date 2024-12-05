namespace DatabaseAssignment;

class Program
{
    static void Main(string[] args)
    {
        string windowsConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Archer;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        string linuxConnectionString = File.ReadAllText("/home/sami/mssql.txt");
        Archer archer = new(linuxConnectionString);
        
        // print the initial contents
        Console.WriteLine("initial database contents: ");
        PrintContents();
        
        // update the email for sami
        archer.UpdateCustomerEmail(1, "samisnewemail@email.tld");
        
        // reclassify carrot as a berry and change the price
        archer.UpdateProduct(3, "Carrot", "Berry", 0.5m);
        
        // raise the price of mustard
        archer.UpdateProductPrice(4, 3m);
        
        // sami wants to order the new berry-carrots instead of apples so we change his apple order
        archer.UpdateOrderProduct(1, 3);
        
        // a new customer just registered to try out the berry-carrots as well (she wants three)
        Customer jen = new("Jennifer", "Frandsen", "jenfran@mail.tld");
        archer.AddCustomer(jen);
        Order jensOrder = new (3, 3);
        archer.AddOrder(jensOrder);
        archer.AddOrder(jensOrder);
        archer.AddOrder(jensOrder);
        
        // the berry carrots were such a hit we decide to add other berries into our selection
        Product watermelonBerry = new Product("Watermelon", "Berry", 1.7m);
        archer.AddProduct(watermelonBerry);
        Product potatoBerry = new Product("Potato", "Berry", 1.1m);
        archer.AddProduct(potatoBerry);
        
        // Marko starts caring about privacy and deletes his account (this causes his orders to be deleted as well)
        archer.DeleteCustomer(2);
        
        // after all that, let's print the database contents again
        Console.WriteLine();
        Console.WriteLine("new database contents: ");
        PrintContents();
        
        return;

        void PrintContents()
        {
            Console.WriteLine();
            List<Customer> customerList = archer.GetCustomers();

            Console.WriteLine("Customers:");
            foreach (Customer c in customerList)
            {
                Console.WriteLine(c.FirstName + " " + c.LastName + " " + c.Email);
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Products:");
            List<Product> productList = archer.GetProducts();

            foreach (Product p in productList)
            {
                Console.WriteLine(p.Name + ", " + p.Category + " : " + p.Price + " eur");
            }
        
            Console.WriteLine("---------------------------");
            Console.WriteLine("Orders:");

            List<Order> orderList = archer.GetOrders();

            foreach (Order o in orderList)
            {
                Customer c = archer.GetCustomerById(o.CustomerId);
                Product p = archer.GetProductById(o.ProductId);

                Console.WriteLine($"{c.FirstName} {c.LastName} - {p.Name}");
            }
            Console.WriteLine("---------------------------");
        }
    }
}