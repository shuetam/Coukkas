




CREATE DATABASE CoukkasDatabase

DROP DATABASE CoukkasDatabase

use master

USE CoukkasDatabase



CREATE TABLE Users
(
ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
Name NVARCHAR(20) NOT NULL,
Email NVARCHAR(20) NOT NULL,
Password NVARCHAR(20) NOT NULL,
Role NVARCHAR(20) NOT NULL,
LocationID  INT  NULL,
CreatedAt DATE NOT NULL,
);


CREATE TABLE Locations
(
ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Latitude FLOAT  NULL,
Longitude FLOAT  NULL,
)

ALTER TABLE Users ADD CONSTRAINT 
FK_LocationID FOREIGN KEY (LocationID) 
REFERENCES Locations(ID)




CREATE TABLE Fences
(
ID UNIQUEIDENTIFIER PRIMARY KEY,
Name NVARCHAR (20) NOT NULL,
Description  NVARCHAR(max),
OwnerID UNIQUEIDENTIFIER NOT NULL,
LocationID  INT  NULL,
Radius FLOAT NOT NULL,
CreatedAt DATE NOT NULL,
EndDate DATE NULL,
)

ALTER TABLE Fences ADD CONSTRAINT 
FK_LocationID FOREIGN KEY (LocationID) 
REFERENCES Locations(ID)

ALTER TABLE Fences ADD CONSTRAINT 
FK_OwnerID FOREIGN KEY (OwnerID) 
REFERENCES Users(ID)


CREATE TABLE Coupons
(
ID UNIQUEIDENTIFIER PRIMARY KEY,
FenceID UNIQUEIDENTIFIER NOT NULL,
Discount FLOAT NOT NULL,
EndOfValidity  DATE NULL,
UserID UNIQUEIDENTIFIER NULL,
LocationID  INT  NULL,
)

ALTER TABLE Coupons ADD CONSTRAINT 
FK_Location FOREIGN KEY (LocationID) 
REFERENCES Locations(ID)

ALTER TABLE Coupons ADD CONSTRAINT 
FK_User FOREIGN KEY (UserID) 
REFERENCES Users(ID)


ALTER TABLE Coupons ADD CONSTRAINT 
FK_Fence FOREIGN KEY (FenceID) 
REFERENCES Fences(ID)





----------------------------------------------------------------

SELECT * FROM Fences

SELECT * FROM Users

select * from locations

SELECT * FROM Coupons

-----------------------------------------------------------------------


TWO CLASS: FENCESQL AND COUPONSQL


--  create list of object fences with ID, Radius, Long, Lat
select Fences.ID, Radius, Latitude, Longitude from Fences Join Locations 
on Fences.LocationID = Locations.ID

-- create list of object coupons with fenceID Latitude and longitute
select Coupons.ID, FenceID, Latitude, Longitude from Coupons
join Locations on Coupons.LocationID = Locations.ID

-- AFTER TAT I WILL UPDATE TABLE CUPONS WHERE ID = COUPONSQL.ID



-----------------------------------------------------------------------









