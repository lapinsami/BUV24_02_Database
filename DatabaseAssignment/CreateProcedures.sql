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

select * from Customer