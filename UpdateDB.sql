-- Step 0: create [ExchangedProduct]
CREATE TABLE [dbo].[ExchangedProduct] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [ExchangeId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    FOREIGN KEY ([ExchangeId]) REFERENCES [dbo].[Exchanged]([Id]), 
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product]([Id])
) ON [PRIMARY];

-- Step 1: Delete all rows from the table (Excute each row manually)
DELETE FROM [SWD392_DB].[dbo].[ExchangedProduct];
DELETE FROM [SWD392_DB].[dbo].[Post];
DELETE FROM [SWD392_DB].[dbo].[Product];

-- Step 2: Drop the columns [Stock_Quantity] and [Condition]
ALTER TABLE [SWD392_DB].[dbo].[Product]
DROP COLUMN [Stock_Quantity],
             [Condition];

-- Step 3: Add the column [IsForSell] as BIT
ALTER TABLE [SWD392_DB].[dbo].[Product]
ADD [IsForSell] BIT;

-- Create Comment
CREATE TABLE Comment (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    User_Id INT NOT NULL,
    Post_Id INT NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [Date] DATETIME,
    [Status]  BIT NOT NULL,
    FOREIGN KEY (User_Id) REFERENCES [SWD392_DB].[dbo].[User](Id),
    FOREIGN KEY (Post_Id) REFERENCES [SWD392_DB].[dbo].[Post](Id)
);