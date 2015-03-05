:On Error exit

USE [ExpenseAPI]
GO

INSERT INTO [User] ([Name],[CreateDate]) VALUES ('chyuck',GETUTCDATE())
GO

INSERT INTO [Category] ([Name],[UserId],[Type],[CreateDate],[ChangeDate]) VALUES ('Salary',1,'Income',GETUTCDATE(),GETUTCDATE())
GO
INSERT INTO [Category] ([Name],[UserId],[Type],[CreateDate],[ChangeDate]) VALUES ('Food',1,'Expense',GETUTCDATE(),GETUTCDATE())
GO

INSERT INTO [Transaction] ([CategoryId],[USD],[Comment],[Time],[CreateDate],[ChangeDate]) VALUES (1,1200,'April Salary',GETUTCDATE(),GETUTCDATE(),GETUTCDATE())
GO
INSERT INTO [Transaction] ([CategoryId],[USD],[Comment],[Time],[CreateDate],[ChangeDate]) VALUES (2,21.45,'Market Basket',GETUTCDATE(),GETUTCDATE(),GETUTCDATE())
GO
