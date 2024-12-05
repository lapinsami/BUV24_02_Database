﻿namespace DatabaseAssignment;

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
        
        archer.UpdateOrderProduct(1, 4);
        
        archer.DeleteOrder(3);

        List<Order> orderList = archer.GetOrders();

        foreach (Order o in orderList)
        {
            Customer c = archer.GetCustomerById(o.CustomerId);
            Product p = archer.GetProductById(o.ProductId);

            Console.WriteLine($"{c.FirstName} {c.LastName} - {p.Name}");
        }
        
        archer.UpdateProductPrice(2, 0.8m);
    }
}