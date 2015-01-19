:On Error exit

USE [master]
GO

CREATE DATABASE [ExpenseAPI]
GO

CREATE LOGIN [expense_api] WITH PASSWORD=N'expenseapi', DEFAULT_DATABASE=[ExpenseAPI], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [ExpenseAPI]
GO

CREATE USER [expense_api] FOR LOGIN [expense_api] WITH DEFAULT_SCHEMA=[dbo]
GO
sp_addrolemember 'db_datareader', 'expense_api'
GO
sp_addrolemember 'db_datawriter', 'expense_api'
GO