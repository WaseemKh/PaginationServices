CREATE DATABASE Inventory;
USE Inventory;

--############ Firstly you must Create DataBase then Create Table on It
CREATE TABLE items (
    ID INT PRIMARY KEY,
    Name VARCHAR(255),
    Barcode VARCHAR(255)
);
BEGIN TRANSACTION;
DECLARE @i INT = 1;
WHILE @i <= 1000
BEGIN
    INSERT INTO items (ID, Name, Barcode) VALUES (@i, CONCAT('Item ', @i), CONCAT('BCODE', REPLICATE('0', 5-LEN(@i)), @i));
    SET @i = @i + 1;
END;
COMMIT TRANSACTION;