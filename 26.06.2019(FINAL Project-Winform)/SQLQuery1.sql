--create database Fitness

use Fitness

--create table Times(
-- id int primary key identity,
-- Name nvarchar(20) not null,
-- Hours nvarchar(100) not null
-- )

--create table Services(
--id int  primary key identity,
--Name nvarchar(50) not null unique,
--"Price (per hour)" decimal(18,2) not null,
--TimeId int references Times(id)
--)

--create table Packages(
--id int primary key identity,
--Name nvarchar(100) not null,
--TimeId int references Times(id),
--"Price (per month)" decimal(18,2) not null
--)

--create table ServicesToPackages(
--id int primary key identity,
--ServiceId int references Services(id),
--PackageId int references Packages(id)
--)

--create table Cards(
--id int primary key identity
--)

--create table Customers(
--id int primary key identity,
--Name nvarchar(100) not null,
--Surname nvarchar(100),
--CardId int references Cards(id)
--)

--create table Positions(
--id int primary key identity,
--Name nvarchar(100) not null unique
--)

--create table Employees(
--id int primary key identity,
--Name nvarchar(100) not null,
--Surname nvarchar(100),
--Username nvarchar(50) not null unique,
--Password nvarchar(50) not null,
--PositionId int references Positions(id),
--HasVerified bit not null
--)

--create table Orders(
--id int primary key identity,
--EmployeeId int references Employees(id),
--CustomerId int references Customers(id),
--Date datetime not null,
--Price decimal(18,2) not null
--)

--create table OrderDetails(
--id int primary key identity,
--ServiceId int references Services(id),
--PackageId int references Packages(id),
--OrderId int references Orders(id)
--)

--create table Incomes(
--id int primary key identity,
--Date datetime not null,
--Income decimal(18,2) not null
--)

--create table Outcomes(
--id int primary key identity,
--Date datetime not null,
--Outcome decimal(18,2) not null
--)
