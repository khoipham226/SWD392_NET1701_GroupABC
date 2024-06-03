USE [master]
GO

IF DB_ID('SWD392_DB') IS NOT NULL
BEGIN
    DROP DATABASE [SWD392_DB]
END
GO

CREATE DATABASE [SWD392_DB]
GO

USE [SWD392_DB]
GO

CREATE TABLE [Role] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
	[Status] BIT NOT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO


CREATE TABLE [User] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    UserName NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
	DOB DATE NOT NULL,
	[Address] NVARCHAR(100) NOT NULL,
	Phone_Number NVARCHAR(50) NOT NULL,
	[Role_Id] INT NOT NULL,
	Created_Date DATE NOT NULL,
	Modified_Date DATE NULL,
	[Rating_Count] INT NULL,
	[Status] BIT NOT NULL,
FOREIGN KEY (Role_Id) REFERENCES [Role]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Token] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Value] NVARCHAR(MAX) NOT NULL,
	[User_Id] INT NOT NULL,
	[Expiration] DATETIME NOT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Payment] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Date] NVARCHAR(MAX) NOT NULL,
	[Amount] FLOAT NOT NULL,
	[Method] NVARCHAR(50) NOT NULL, 
	[Description] NVARCHAR(MAX) NULL, 
	[Status] BIT NOT NULL, 
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO


CREATE TABLE [Banned_Account] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NOT NULL,
	[Description] NVARCHAR(Max) NOT NULL,
	[Date] DATETIME NOT NULL,
	[Status] BIT NOT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Category] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Status] BIT NOT NULL, 
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Transaction_Type] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Status] BIT NOT NULL, 
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Product] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[User_Id] INT NOT NULL,
	[Category_Id] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Url_IMG] NVARCHAR(MAX) NULL, 
	[Stock_Quantity] INT NOT NULL,
	[Status] BIT NOT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Category_Id]) REFERENCES [Category]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Post] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[User_Id] INT NOT NULL,
	[TransactionType_Id] INT NOT NULL,
	[Product_Id] INT NOT NULL,
    [Title] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[IMG] NVARCHAR(MAX) NULL, 
	[Price] FLOAT NOT NULL,
	[Date] DATE NOT NULL,
	[Status] BIT NOT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([TransactionType_Id]) REFERENCES [Transaction_Type]([Id]),
FOREIGN KEY ([Product_Id]) REFERENCES [Product]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Report] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[User_Id] INT NOT NULL,
	[Post_Id] INT NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Date] DATE NOT NULL,
	[Status] BIT NOT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Post_Id]) REFERENCES [Post]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Order] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[User_Id] INT NOT NULL,
	[Post_Id] INT NOT NULL,
	[Payment_Id] INT NULL,
	[Quantity] INT NOT NULL,
	[Total_Price] FLOAT NOT NULL,
	[Date] DATE NOT NULL,
	[Status] BIT NOT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Post_Id]) REFERENCES [Post]([Id]),
FOREIGN KEY ([Payment_Id]) REFERENCES [Payment]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Dispute] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[User_Id] INT NOT NULL,
	[Order_Id] INT NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[DATE] DATETIME NOT NULL,
	[Status] BIT NOT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Order_Id]) REFERENCES [Order]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Exchanged] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[User_Id] INT NOT NULL,
	[Post_Id] INT NOT NULL,
	[Order_Id] INT NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[DATE] DATETIME NOT NULL,
	[Status] BIT NOT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Post_Id]) REFERENCES [Post] ([Id]),
FOREIGN KEY ([Order_Id]) REFERENCES [Order]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];
GO

CREATE TABLE [Rating] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[User_Id] INT NOT NULL,
	[Exchange_Id] INT NOT NULL,
	[Score] INT NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[DATE] DATETIME NOT NULL,
	[Status] BIT NOT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Exchange_Id]) REFERENCES [Exchanged] ([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];



/*Insert Role*/
INSERT INTO [Role]([Name],[Status])VALUES ('admin',1)
INSERT INTO [Role]([Name],[Status])VALUES ('user',1)
INSERT INTO [Role]([Name],[Status])VALUES ('staff',1)

/*Insert Category*/
INSERT INTO [Category]([Name],[Description],[Status])VALUES ('may tinh sach tay',null,1)
INSERT INTO [Category]([Name],[Description],[Status])VALUES ('chuot',null,1)
INSERT INTO [Category]([Name],[Description],[Status])VALUES ('ban phim',null,1)
INSERT INTO [Category]([Name],[Description],[Status])VALUES ('sach',null,1)
INSERT INTO [Category]([Name],[Description],[Status])VALUES ('tai nghe',null,1)

/*Insert Transaction_Type*/
INSERT INTO [Transaction_Type]([Name],[Description],[Status])VALUES ('trao doi',null,1)
INSERT INTO [Transaction_Type]([Name],[Description],[Status])VALUES ('mua',null,1)
INSERT INTO [Transaction_Type]([Name],[Description],[Status])VALUES ('ban',null,1)

/*Insert User*/
INSERT INTO [User](UserName,[Password],[Email],DOB,[Address],[Phone_Number],Role_Id,Created_Date,[Status])
VALUES ('admin','12345','admin@gmail.com','2002/05/31','186 le van viet','0889339769',1,'2024-05-31',1)
INSERT INTO [User](UserName,[Password],[Email],DOB,[Address],[Phone_Number],Role_Id,Created_Date,[Status])
VALUES ('user','12345','user@gmail.com','2002/05/31','186 le van viet','0889339769',2,'2024-05-31',1)
INSERT INTO [User](UserName,[Password],[Email],DOB,[Address],[Phone_Number],Role_Id,Created_Date,[Status])
VALUES ('user1','12345','admin@gmail.com','2002/05/31','186 le van viet','0889339769',2,'2024-05-31',1)

/*Insert Product*/
INSERT INTO [Product]([User_Id],[Category_Id],[Name],[Description],[Url_IMG],Stock_Quantity,[Status])
VALUES (3,1,'may tinh asus',null,null,2,1)
INSERT INTO [Product]([User_Id],[Category_Id],[Name],[Description],[Url_IMG],Stock_Quantity,[Status])
VALUES (3,1,'may tinh acer',null,null,2,1)
INSERT INTO [Product]([User_Id],[Category_Id],[Name],[Description],[Url_IMG],Stock_Quantity,[Status])
VALUES (3,1,'may tinh dell',null,null,2,1)

/*Insert Post*/
INSERT INTO [Post]([User_Id],[TransactionType_Id],[Product_Id],Title,[Description],[IMG],Price,[Date],[Status])
VALUES (2,1,1,'ban',null,null,100000,'2024-05-31',1)
INSERT INTO [Post]([User_Id],[TransactionType_Id],[Product_Id],Title,[Description],[IMG],Price,[Date],[Status])
VALUES (2,3,2,'ban',null,null,200000,'2024-05-31',1)
INSERT INTO [Post]([User_Id],[TransactionType_Id],[Product_Id],Title,[Description],[IMG],Price,[Date],[Status])
VALUES (2,3,3,'ban',null,null,300000,'2024-05-31',1)

/*Insert Order*/
INSERT INTO [Order]([User_Id],[Post_Id],[Payment_Id],Quantity,[Total_Price],[Date],[Status])
VALUES (3,2,NULL,1,100000,'2024-05-31',1)
INSERT INTO [Order]([User_Id],[Post_Id],[Payment_Id],Quantity,[Total_Price],[Date],[Status])
VALUES (3,3,NULL,1,200000,'2024-05-31',1)
INSERT INTO [Order]([User_Id],[Post_Id],[Payment_Id],Quantity,[Total_Price],[Date],[Status])
VALUES (3,1,NULL,1,300000,'2024-05-31',1)







