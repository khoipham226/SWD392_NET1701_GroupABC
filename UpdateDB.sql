-- Script to create ExchangedProduct table
CREATE TABLE [dbo].[ExchangedProduct] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [ExchangeId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    FOREIGN KEY ([ExchangeId]) REFERENCES [dbo].[Exchanged]([Id]), 
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product]([Id])
) ON [PRIMARY];

-- Script to drop the existing Post table if it exists
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