CREATE TABLE [dbo].[Password]
(
	[PasswordID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Password] VARCHAR(MAX) NOT NULL
)

CREATE TABLE [dbo].[User]
(
	[UserID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [PasswordID] VARCHAR(50) NOT NULL,
	[FirstName] VARCHAR(MAX) NOT NULL,
	[Surname] VARCHAR(MAX) NOT NULL,
	[Username] VARCHAR(MAX) NOT NULL,
	[RecoveryKey] VARCHAR(MAX) NOT NULL,
	CONSTRAINT FK_User1 FOREIGN KEY (PasswordID) REFERENCES [dbo].[Password](PasswordID)
)

CREATE TABLE [dbo].[Employee]
(
	[EmployeeID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [UserID] VARCHAR(50) NOT NULL,
	CONSTRAINT FK_Employee1 FOREIGN KEY (UserID) REFERENCES [dbo].[User](UserID)
)

CREATE TABLE [dbo].[Farmer]
(
	[FarmerID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [UserID] VARCHAR(50) NOT NULL,
	CONSTRAINT FK_Farmer1 FOREIGN KEY (UserID) REFERENCES [dbo].[User](UserID)
)

CREATE TABLE [dbo].[Product]
(
	[ProductID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [FarmerID] VARCHAR(50) NOT NULL,
	[ProductName] VARCHAR(MAX) NOT NULL,
	[ProductType] VARCHAR(MAX) NOT NULL,
	[ProductPrice] MONEY NOT NULL,
	CONSTRAINT FK_Product1 FOREIGN KEY (FarmerID) REFERENCES [dbo].[Farmer](FarmerID)
)

CREATE TABLE [dbo].[Stock]
(
	[StockID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [ProductID] VARCHAR(50) NOT NULL,
	[StockAmount] INT NOT NULL,
	CONSTRAINT FK_Stock1 FOREIGN KEY (ProductID) REFERENCES [dbo].[Product](ProductID)
)

CREATE TABLE [dbo].[Stock_Change]
(
	[StockChangeID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [StockID] VARCHAR(50) NOT NULL,
	[EmployeeID] VARCHAR(50) NOT NULL,
	[StockChange] INT NOT NULL,
	[ChangeDate] DATE NOT NULL,
	CONSTRAINT FK_StockChange1 FOREIGN KEY (StockID) REFERENCES [dbo].[Stock](StockID),
	CONSTRAINT FK_StockChange2 FOREIGN KEY (EmployeeID) REFERENCES [dbo].[Employee](EmployeeID)
)