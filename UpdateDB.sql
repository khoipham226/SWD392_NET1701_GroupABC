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