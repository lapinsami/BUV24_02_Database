use Archer
go

create procedure AddCustomer(
    @FirstName nvarchar(32),
    @LastName nvarchar(64),
    @Email nvarchar(64),
    @ID int output
)
as begin
    insert into 
        Customer
    values
        (@FirstName, @LastName, @Email)

    set @ID = SCOPE_IDENTITY();
end
go

create procedure UpdateCustomerEmail(
    @newEmail nvarchar(64),
    @ID int
)
as begin
    update Customer
    set email = @newEmail
    where ID = @ID
end
go

create procedure AddProduct(
    @Name nvarchar(32),
    @Category nvarchar(32),
    @Price decimal(19, 4),
    @ID int output
)
as begin
    insert into
        Product
    values
        (@Name, @Category, @Price)

    set @ID = SCOPE_IDENTITY();
end
go

create procedure UpdateProduct(
    @newName nvarchar(32),
    @newCategory nvarchar(32),
    @newPrice decimal(19, 4),
    @ID int
)
as begin
    update Product
    set name = @newName, category = @newCategory, price = @newPrice
    where ID = @ID
end
go

create procedure AddOrder(
    @CustomerId int,
    @ProductId int,
    @ID int output
)
as begin
    insert into
        [Order]
    values
        (@CustomerId, @ProductId)

    set @ID = SCOPE_IDENTITY();
end
go

create procedure UpdateOrderProduct(
    @newProductId int,
    @ID int
)
as begin
    update [Order]
    set ProductID = @newProductId
    where ID = @ID
end
go

create procedure DeleteOrder(
    @ID int
)
as begin
    delete from [Order]
    where ID = @ID
end
go

create procedure DeleteProduct(
    @ID int
)
as begin
    delete from Product
    where ID = @ID
end
go

create procedure DeleteCustomer(
    @ID int
)
as begin
    delete from Customer
    where ID = @ID
end
go