USE master
IF EXISTS(select * from sys.databases where name='teamfu')
DROP DATABASE teamfu
CREATE DATABASE Teamfu
ON   
( NAME = Teamfu_dat,  
    FILENAME = '/var/opt/mssql/data/teamfu_data.mdf',  
    SIZE = 10,  
    MAXSIZE = 50,  
    FILEGROWTH = 5 )  
LOG ON  
( NAME = Teamfu_log,  
    FILENAME = '/var/opt/mssql/data/teamfu_log.ldf',  
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB ) ;  
GO

USE Teamfu
GO


CREATE TABLE dbo.task 
(  
    PurchaseOrderID int NOT NULL  
    ,LineNumber smallint NOT NULL  
    ,ProductID int NULL  
    ,UnitPrice money NULL  
    ,OrderQty smallint NULL  
    ,ReceivedQty float NULL  
    ,RejectedQty float NULL  
    ,DueDate datetime NULL  
);
GO