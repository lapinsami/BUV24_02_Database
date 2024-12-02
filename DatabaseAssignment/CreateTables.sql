create database Archer

use Archer

create table Customer (
    ID int primary key identity not null,
    FirstName nvarchar(32) not null,
    LastName nvarchar(64) not null,
    Email nvarchar(64) not null unique
)

create table Product (
    ID int primary key identity not null,
    name nvarchar(32) not null,
    category nvarchar(32) not null,
    price Decimal(19, 4) not null,
    unique (name, category)
)

create table [Order] (
    ID int primary key identity not null,
    CustomerID int references Customer,
    OrderDate date
)

create table OrderProduct (
    OrderID int references [Order],
    ProductID int references Product,
    Quantity int not null,
    primary key (OrderID, ProductID)
)

insert into Customer (FirstName, LastName, Email)
values
    ('Sami', 'Lappalainen', 'samilappalainen@gmail.com'),
    ('Marko', 'Leinikka', 'markoleinikka@gmail.com')

insert into Product (name, category, price)
values 
    ('Apple', 'Fruit', 0.45),
    ('Orange', 'Fruit', 0.75),
    ('Carrot', 'Vegetable', 0.32),
    ('Mustard', 'Condiment', 2.7)

insert into [Order] (CustomerID, OrderDate)
values
    (1, getdate()),
    (1, getdate()),
    (1, getdate()),
    (2, getdate())

insert into OrderProduct (OrderID, ProductID, Quantity)
values 
    (1, 1, 7),
    (1, 2, 7),
    (1, 3, 7),
    (1, 4, 7),
    (2, 1, 1),
    (3, 2, 1),
    (4, 3, 1)

select Customer.FirstName, Customer.LastName, Product.name, OrderProduct.Quantity from Customer
join [Order] on Customer.ID = [Order].CustomerID
join OrderProduct on [Order].ID = OrderProduct.ProductID
join Product on OrderProduct.ProductID = Product.ID
