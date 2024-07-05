USE [master]
GO

IF DB_ID('SWD392_DB') IS NOT NULL
BEGIN
    --DROP DATABASE [SWD392_DB]
	ALTER DATABASE [SWD392_DB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE [SWD392_DB];
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
) ON [PRIMARY];
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
    [Gender] NVARCHAR(50) NOT NULL,
    [ImgURL] NVARCHAR(MAX) NOT NULL,
    Created_Date DATE NOT NULL,
    Modified_Date DATE NULL,
    [Rating_Count] INT NULL,
    [Status] BIT NOT NULL,
FOREIGN KEY (Role_Id) REFERENCES [Role]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Token] 
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Value] NVARCHAR(MAX) NOT NULL,
    [User_Id] INT NOT NULL,
    [Expiration] DATETIME NOT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Payment] 
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Date] NVARCHAR(MAX) NOT NULL,
    [Amount] FLOAT NOT NULL,
    [Method] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Status] BIT NOT NULL, 
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [BannedAccount] 
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NOT NULL,
    [Description] NVARCHAR(Max) NOT NULL,
    [Date] DATETIME NOT NULL,
    [Modified_Date] DATETIME NULL,
    [Status] BIT NOT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Appeal] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NOT NULL,
    [BannerAcount_Id] INT NOT NULL,
    [Description] NVARCHAR(Max) NOT NULL,
    [Date] DATETIME NOT NULL,
    [Modified_Date] DATETIME NULL,
    [Status] BIT NOT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User]([Id]),
FOREIGN KEY ([BannerAcount_Id]) REFERENCES [BannedAccount]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Category] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Status] BIT NOT NULL, 
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [SubCategory] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Category_Id] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Status] BIT NOT NULL, 
FOREIGN KEY ([Category_Id]) REFERENCES [Category]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Product] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NOT NULL,
    [Category_Id] INT NOT NULL,
    [SubCategory_Id] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Price] FLOAT NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Location] NVARCHAR(100) NULL,
    [Url_IMG] NVARCHAR(MAX) NULL, 
    [Status] BIT NOT NULL, 
    [IsForSell] BIT NOT NULL, 
FOREIGN KEY ([SubCategory_Id]) REFERENCES [SubCategory] ([Id]),
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Category_Id]) REFERENCES [Category]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Post] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NOT NULL,
    [Product_Id] INT NOT NULL,
    [Title] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Date] DATE NOT NULL,
    [ImageUrl] NVARCHAR(MAX) NULL,
    [PublicStatus] BIT NULL,
    [ExchangedStatus] BIT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Product_Id]) REFERENCES [Product]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [Comment] 
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [User_Id] INT NOT NULL,
    [Post_Id] INT NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [Date] DATETIME,
    [Status]  BIT NOT NULL,
    FOREIGN KEY ([User_Id]) REFERENCES [User]([Id]),
    FOREIGN KEY ([Post_Id]) REFERENCES [Post]([Id])
) ON [PRIMARY];
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
) ON [PRIMARY];
GO

CREATE TABLE [Order] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NOT NULL,
    [Payment_Id] INT NULL,
    [Total_Price] FLOAT NULL,
    [Date] DATE NOT NULL,
    [Status] BIT NOT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Payment_Id]) REFERENCES [Payment]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [OrderDetails] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Order_Id] INT NOT NULL,
    [Product_Id] INT NOT NULL,
    [Price] FLOAT NOT NULL,
    [Status] BIT NOT NULL,
FOREIGN KEY ([Product_Id]) REFERENCES [Product] ([Id]),
FOREIGN KEY ([Order_Id]) REFERENCES [Order] ([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Exchanged] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NOT NULL,
    [Post_Id] INT NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [DATE] DATETIME NOT NULL,
    [Status] BIT NOT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Post_Id]) REFERENCES [Post] ([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [ExchangedProduct] 
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [ExchangeId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    FOREIGN KEY ([ExchangeId]) REFERENCES [Exchanged]([Id]), 
    FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
) ON [PRIMARY];
GO

CREATE TABLE [Rating] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NOT NULL,
    [Score] INT NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [DATE] DATETIME NOT NULL,
    [Status] BIT NOT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Group] 
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [PostId] INT NOT NULL,
    [UserExchangeId] INT NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    [ModifiedDate] DATETIME NULL,
FOREIGN KEY ([PostId]) REFERENCES [Post]([Id]),
FOREIGN KEY ([UserExchangeId]) REFERENCES [User]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Message] 
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [SenderId] INT NOT NULL,
    [GroupId] INT NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    [ModifiedDate] DATETIME NULL,
FOREIGN KEY ([SenderId]) REFERENCES [User]([Id]),
FOREIGN KEY ([GroupId]) REFERENCES [Group]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

USE [SWD392_DB]
GO

-- Insert data into Role table
INSERT INTO [Role] ([Name], [Status]) VALUES
('Admin', 1),
('User', 1),
('Moderator', 1);

-- Insert data into User table
INSERT INTO [User] 
(UserName, [Password], Email, DOB, [Address], Phone_Number, [Role_Id], [Gender], [ImgURL], Created_Date, Modified_Date, [Rating_Count], [Status]) VALUES
('admin', 'adminpassword', 'admin@example.com', '1980-01-01', '123 Admin St.', '123-456-7890', 1, 'Male', 'http://example.com/admin.jpg', GETDATE(), NULL, NULL, 1),
('john_doe', 'password123', 'john@example.com', '1990-05-15', '456 Main St.', '123-456-7891', 2, 'Male', 'http://example.com/john.jpg', GETDATE(), NULL, NULL, 1),
('jane_doe', 'password456', 'jane@example.com', '1992-08-25', '789 Maple St.', '123-456-7892', 2, 'Female', 'http://example.com/jane.jpg', GETDATE(), NULL, NULL, 1);

-- Insert data into Token table
INSERT INTO [Token] 
([Value], [User_Id], [Expiration]) VALUES
('tokenvalue1', 1, DATEADD(day, 1, GETDATE())),
('tokenvalue2', 2, DATEADD(day, 1, GETDATE())),
('tokenvalue3', 3, DATEADD(day, 1, GETDATE()));

-- Insert data into Payment table
INSERT INTO [Payment] 
([Date], [Amount], [Method], [Description], [Status]) VALUES
('2024-01-01', 100.0, 'Credit Card', 'Payment for services', 1),
('2024-02-01', 150.0, 'PayPal', 'Payment for products', 1),
('2024-03-01', 200.0, 'Bank Transfer', 'Payment for subscription', 1);

-- Insert data into BannedAccount table
INSERT INTO [BannedAccount] 
([User_Id], [Description], [Date], [Modified_Date], [Status]) VALUES
(2, 'Violation of terms', GETDATE(), NULL, 1),
(3, 'Spam activity', GETDATE(), NULL, 1);

-- Insert data into Appeal table
INSERT INTO [Appeal] 
([User_Id], [BannerAcount_Id], [Description], [Date], [Modified_Date], [Status]) VALUES
(2, 1, 'Appeal for ban', GETDATE(), NULL, 0),
(3, 2, 'Appeal for ban', GETDATE(), NULL, 0);

-- Insert data into Category table
INSERT INTO [Category] 
([Name], [Description], [Status]) VALUES
('Electronics', 'Electronic devices and gadgets', 1),
('Books', 'All kinds of books', 1),
('Clothing', 'Apparel and accessories', 1);

-- Insert data into SubCategory table
INSERT INTO [SubCategory] 
([Category_Id], [Name], [Description], [Status]) VALUES
(1, 'Mobile Phones', 'Smartphones and mobile phones', 1),
(1, 'Laptops', 'Laptops and notebooks', 1),
(2, 'Fiction', 'Fictional books and novels', 1),
(2, 'Non-Fiction', 'Non-fictional books', 1),
(3, 'Men', 'Men s clothing', 1),
(3, 'Women', 'Women s clothing', 1);

-- Insert data into Product table
INSERT INTO [Product] 
([User_Id], [Category_Id], [SubCategory_Id], [Name], [Price], [Description], [Location], [Url_IMG], [Status], [IsForSell]) VALUES
(2, 1, 1, 'iPhone 12', 799.99, 'Latest model of iPhone', 'New York', 'http://example.com/iphone12.jpg', 1, 1),
(2, 2, 3, 'Harry Potter', 29.99, 'Complete series of Harry Potter', 'Los Angeles', 'http://example.com/harrypotter.jpg', 1, 1),
(3, 3, 6, 'Womens Jacket', 49.99, 'Stylish womens jacket', 'San Francisco', 'http://example.com/womensjacket.jpg', 1, 1);

-- Insert data into Post table
INSERT INTO [Post] 
([User_Id], [Product_Id], [Title], [Description], [Date], [ImageUrl], [PublicStatus], [ExchangedStatus]) VALUES
(2, 1, 'Selling iPhone 12', 'Brand new iPhone 12 for sale', GETDATE(), 'http://example.com/iphone12.jpg', 1, 0),
(2, 2, 'Selling Harry Potter Series', 'Complete Harry Potter series in good condition', GETDATE(), 'http://example.com/harrypotter.jpg', 1, 0),
(3, 3, 'Selling Women s Jacket', 'Stylish women s jacket for sale', GETDATE(), 'http://example.com/womensjacket.jpg', 1, 0);

-- Insert data into Comment table
INSERT INTO [Comment] 
([User_Id], [Post_Id], [Content], [Date], [Status]) VALUES
(2, 1, 'Is the iPhone still available?', GETDATE(), 1),
(3, 2, 'I am interested in the Harry Potter series.', GETDATE(), 1);

-- Insert data into Report table
INSERT INTO [Report] 
([User_Id], [Post_Id], [Description], [Date], [Status]) VALUES
(2, 1, 'This post is inappropriate.', GETDATE(), 1),
(3, 2, 'Spam content.', GETDATE(), 1);

-- Insert data into Order table
INSERT INTO [Order] 
([User_Id], [Payment_Id], [Total_Price], [Date], [Status]) VALUES
(2, 1, 100.0, GETDATE(), 1),
(3, 2, 150.0, GETDATE(), 1);

-- Insert data into OrderDetails table
INSERT INTO [OrderDetails] 
([Order_Id], [Product_Id], [Price], [Status]) VALUES
(1, 1, 799.99, 1),
(2, 2, 29.99, 1);

-- Insert data into Exchanged table
INSERT INTO [Exchanged] 
([User_Id], [Post_Id], [Description], [DATE], [Status]) VALUES
(2, 1, 'Exchanging iPhone for a laptop', GETDATE(), 1),
(3, 2, 'Exchanging Harry Potter series for a camera', GETDATE(), 1);

-- Insert data into ExchangedProduct table
INSERT INTO [ExchangedProduct] 
([ExchangeId], [ProductId]) VALUES
(1, 1),
(2, 2);

-- Insert data into Rating table
INSERT INTO [Rating] 
([User_Id], [Score], [Description], [DATE], [Status]) VALUES
(2, 5, 'Great product!', GETDATE(), 1),
(3, 4, 'Good seller.', GETDATE(), 1);

-- Insert data into Group table
INSERT INTO [Group] 
([PostId], [UserExchangeId], [CreatedDate], [ModifiedDate]) VALUES
(1, 2, GETDATE(), NULL),
(2, 3, GETDATE(), NULL);

-- Insert data into Message table
INSERT INTO [Message] 
([SenderId], [GroupId], [Content], [CreatedDate], [ModifiedDate]) VALUES
(2, 1, 'Is the iPhone still available?', GETDATE(), NULL),
(3, 2, 'I am interested in the Harry Potter series.', GETDATE(), NULL);
GO










