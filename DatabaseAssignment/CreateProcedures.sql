use Archer

create procedure AddCustomer(
    @FirstName varchar(32),
    @LastName varchar(64),
    @Email varchar(64),
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

create procedure AddProduct(
    @Name varchar(32),
    @Category varchar(32),
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

select * from Product