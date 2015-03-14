:On Error exit

USE [master]
GO

CREATE DATABASE [ExpenseAPI]
GO

IF NOT EXISTS(SELECT * FROM sys.syslogins WHERE [loginname]='expense_api')
BEGIN
	CREATE LOGIN [expense_api] WITH PASSWORD=N'expenseapi', DEFAULT_DATABASE=[ExpenseAPI], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
END
GO

USE [ExpenseAPI]
GO

-- Database User --
CREATE USER [expense_api] FOR LOGIN [expense_api] WITH DEFAULT_SCHEMA=[dbo]
GO
sp_addrolemember 'db_datareader', 'expense_api'
GO
sp_addrolemember 'db_datawriter', 'expense_api'
GO

-- User Table --
CREATE TABLE [dbo].[User]
(
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId]),
 CONSTRAINT [UK_User] UNIQUE NONCLUSTERED ([Name])
)
GO

-- Category Table --
CREATE TABLE [dbo].[Category]
(
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[UserId] [int] NOT NULL,
	[Type] [varchar](10) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ChangeDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([CategoryId]),
 CONSTRAINT [UK_Category] UNIQUE NONCLUSTERED ([UserId], [Name]),
 CONSTRAINT [CK_Type] CHECK ([Type] IN ('Income', 'Expense'))
)
GO

ALTER TABLE [dbo].[Category] ADD CONSTRAINT [FK_Category_User] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([UserId])
GO
CREATE NONCLUSTERED INDEX [IX_Category_User] ON [dbo].[Category] ([UserId])
GO

-- Transaction Table --
CREATE TABLE [dbo].[Transaction]
(
	[TransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[USD] [money] NOT NULL,
	[Comment] [nvarchar](100) NULL,
	[Date] [date] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ChangeDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([TransactionId])
)
GO

ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT [FK_Transaction_Category] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Category] ([CategoryId])
GO
CREATE NONCLUSTERED INDEX [IX_Transaction_Category] ON [dbo].[Transaction] ([CategoryId])
GO
CREATE NONCLUSTERED INDEX [IX_Transaction_Id] ON [dbo].[Transaction] ([Id])
GO
CREATE NONCLUSTERED INDEX [IX_Transaction_Date] ON [dbo].[Transaction] ([Date])
GO