﻿namespace DatabaseAssignment;

class Program
{
    static void Main(string[] args)
    {
        string windowsConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Archer;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        string linuxConnectionString = File.ReadAllText("/home/sami/mssql.txt");
        Archer archer = new(linuxConnectionString);
        

        Customer jen = new("Jennifer", "Frandsen", "jenfran@mail.com");
        Product cookingCream = new("Oatly", "Cream", 1.05m);
        
        archer.AddCustomer(jen);
        archer.AddProduct(cookingCream);

        Order order = new(jen, cookingCream);
        archer.AddOrder(order);
    }
}