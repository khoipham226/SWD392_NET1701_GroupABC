-- Script to create ExchangedProduct table
CREATE TABLE [dbo].[ExchangedProduct] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [ExchangeId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    FOREIGN KEY ([ExchangeId]) REFERENCES [dbo].[Exchanged]([Id]), 
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product]([Id])
) ON [PRIMARY];

-- Drop foreign key constraints referencing the Post table
IF OBJECT_ID('FK_Post_User', 'F') IS NOT NULL
BEGIN
    ALTER TABLE [SWD392_DB].[dbo].[Post] DROP CONSTRAINT FK_Post_User;
END
GO

IF OBJECT_ID('FK_Post_Product', 'F') IS NOT NULL
BEGIN
    ALTER TABLE [SWD392_DB].[dbo].[Post] DROP CONSTRAINT FK_Post_Product;
END
GO
IF OBJECT_ID('FK__Report__Post_Id__52593CB8', 'F') IS NOT NULL
BEGIN
    ALTER TABLE [SWD392_DB].[dbo].[Report] DROP CONSTRAINT FK__Report__Post_Id__52593CB8;
END
GO

IF OBJECT_ID('FK__Exchanged__Post___5DCAEF64', 'F') IS NOT NULL
BEGIN
    ALTER TABLE [SWD392_DB].[dbo].[Exchanged] DROP CONSTRAINT FK__Exchanged__Post___5DCAEF64;
END
GO

-- Drop the Post table
IF OBJECT_ID('[SWD392_DB].[dbo].[Post]', 'U') IS NOT NULL
BEGIN
    DROP TABLE [SWD392_DB].[dbo].[Post];
END
GO

-- Drop the Post table
IF OBJECT_ID('[SWD392_DB].[dbo].[Post]', 'U') IS NOT NULL
BEGIN
    DROP TABLE [SWD392_DB].[dbo].[Post];
END
GO

-- Script to create the new Post table
CREATE TABLE [Post] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[User_Id] INT NOT NULL,
	[Product_Id] INT NOT NULL,
    [Title] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Date] DATE NOT NULL,
	[Status] BIT NOT NULL, 
	[ImageUrl] NVARCHAR(MAX) NULL,
	[PublicStatus] BIT NULL,
	[ExchangedStatus] BIT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Product_Id]) REFERENCES [Product]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY];