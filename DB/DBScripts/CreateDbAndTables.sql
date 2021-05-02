create database JParts
on PRIMARY
( NAME='JParts_Primary',
    FILENAME=
       'D:\BSTU\2 Êóðñ\2 Ñåìåñòð\Êóðñîâàÿ ÎÎÏ\DB\JpartsDB.mdf',
    SIZE=4MB,
    MAXSIZE=100MB,
    FILEGROWTH=1MB)
go
use JParts;
go

create table [ADDRESS]
(
	ADDRESS_ID as (CITY + STREET + CONVERT(nvarchar(4), HOUSE_NUM) + CONVERT(nvarchar(4), FLAT_NUM)) persisted primary key,
	CITY nvarchar(50) not null,
	STREET nvarchar(100) not null,
	HOUSE_NUM int not null,
	FLAT_NUM int not null
)

create table CAR
(
	CAR_ID as (MANUFACTURER + MODEL + CONVERT(nvarchar(4), [YEAR])) persisted primary key,
	MANUFACTURER nvarchar(50) not null,
	MODEL nvarchar(70) not null,
	[YEAR] int not null
)

create table CLIENT
(
	CLIENT_ID as ([NAME] + PHONE_NUM) persisted primary key,
	[NAME] nvarchar(200) not null,
	PHONE_NUM nvarchar(20) not null,
	[ADDRESS] nvarchar(max) foreign key references [ADDRESS](ADDRESS_ID),
	EMAIL nvarchar(100) not null,
	[LOGIN] nvarchar(50) not null,
	[PASSWORD] nvarchar(30) not null
)

create table SHOP
(
	SHOP_ID as (PHONE_NUM) persisted primary key,
	[ADDRESS] nvarchar(max) foreign key references [ADDRESS](ADDRESS_ID),
	PHONE_NUM nvarchar(20),
)

create table PART
(
	PART_ID as (CAR_ID + [NAME]) persisted primary key,
	CAR_ID nvarchar(max) foreign key references CAR(CAR_ID),
	[NAME] nvarchar(150) not null,
	[TYPE] nvarchar(100) not null,
	PRICE decimal(7,2) not null,
	[AVAILABILITY] bit not null,
	IMG varbinary(max) not null
)
create table PARTS_ORDERED
(
	PARTS_ORDERED_ID nvarchar(max),			--ÍÓÆÅÍ ÑÏÈÑÎÊ ÂÑÅÕ ÒÎÂÀÐÎÂ Ê ÎÄÍÎÌÓ ÇÀÊÀÇÓ!!!
	PART_ID nvarchar(max) foreign key references PART(PART_ID),
)

create table [ORDER]
(
	ORDER_ID as (ÑLIENT_ID + CONVERT(nvarchar(3), RAND())),
	CLIENT_ID nvarchar(max) foreign key references CLIENT(CLIENT_ID),
	PARTS_ORDERED_ID nvarchar(max) foreign key references PARTS_ORDERED(PARTS_ORDERED_ID)
)

