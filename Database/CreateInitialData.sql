:On Error exit

USE [ExpenseAPI]
GO

INSERT INTO [User] ([Name]) VALUES ('chyuck')
GO

INSERT INTO [Category] ([Name],[UserId],[Type],[CreateDate],[ChangeDate]) VALUES ('Salary',1,'Income',GETDATE(),GETDATE())
GO
INSERT INTO [Category] ([Name],[UserId],[Type],[CreateDate],[ChangeDate]) VALUES ('Food',1,'Expense',GETDATE(),GETDATE())
GO
