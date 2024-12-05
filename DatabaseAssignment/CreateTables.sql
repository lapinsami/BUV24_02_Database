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
    CustomerID int references Customer on delete cascade,
    ProductID int references Product on delete cascade
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

insert into [Order] (CustomerID, ProductID)
values
    (1, 1),
    (2, 2),
    (1, 3),
    (2, 4),
    (1, 4)