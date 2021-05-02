create database JParts
on PRIMARY
( NAME='JParts_Primary',
    FILENAME=
       'D:\BSTU\2 Курс\2 Семестр\Курсовая ООП\DB\JpartsDB.mdf',
    SIZE=4MB,
    MAXSIZE=100MB,
    FILEGROWTH=1MB)
go
use JParts;
go

create table [ADDRESS]
(
	ADDRESS_ID as CONCAT(CITY, STREET, HOUSE_NUM, FLAT_NUM) primary key,
	CITY nvarchar(50) not null,
	STREET nvarchar(100) not null,
	HOUSE_NUM int not null,
	FLAT_NUM int not null
)

create table CAR
(
	CAR_ID as CONCAT(MANUFACTURER, MODEL, [YEAR]) primary key,
	MANUFACTURER nvarchar(50) not null,
	MODEL nvarchar(70) not null,
	[YEAR] int not null
)

create table CLIENT
(
	CLIENT_ID as CONCAT([NAME], PHONE_NUM) primary key,
	[NAME] nvarchar(200) not null,
	PHONE_NUM nvarchar(20) not null,
	[ADDRESS] nvarchar(max) foreign key references [ADDRESS](ADDRESS_ID),
	EMAIL nvarchar(100) not null,
	[LOGIN] nvarchar(50) not null,
	[PASSWORD] nvarchar(30) not null
)

create table SHOP
(
	SHOP_ID as CONCAT([ADDRESS], PHONE_NUM) primary key,
	[ADDRESS] nvarchar(max) foreign key references [ADDRESS](ADDRESS_ID),
	PHONE_NUM nvarchar(20),
)

create table PART
(
	PART_ID as CONCAT(CAR_ID, [NAME], PRICE),
	CAR_ID nvarchar(max) foreign key references CAR(CAR_ID),
	[NAME] nvarchar(150) not null,
	[TYPE] nvarchar(100) not null,
	PRICE decimal(7,2) not null,
	[AVAILABILITY] bit not null,
	IMG varbinary(max) not null
)