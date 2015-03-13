:On Error exit

USE [ExpenseAPI]
GO

INSERT INTO [User] ([Name],[CreateDate]) VALUES ('chyuck',GETUTCDATE())
GO

INSERT INTO [Category] ([Name],[UserId],[Type],[CreateDate],[ChangeDate]) VALUES ('Salary',1,'Income',GETUTCDATE(),GETUTCDATE())
GO
INSERT INTO [Category] ([Name],[UserId],[Type],[CreateDate],[ChangeDate]) VALUES ('Food',1,'Expense',GETUTCDATE(),GETUTCDATE())
GO

INSERT INTO [Transaction] ([CategoryId],[Id],[USD],[Comment],[Time],[CreateDate],[ChangeDate]) VALUES (1,'D676F6C7-08EB-4D8F-9580-169FF4D9E8D7',1200,'April Salary',GETUTCDATE(),GETUTCDATE(),GETUTCDATE())
GO
INSERT INTO [Transaction] ([CategoryId],[Id],[USD],[Comment],[Time],[CreateDate],[ChangeDate]) VALUES (2,'33B71215-9BDB-4898-8AC7-8676F93D9620',21.45,'Market Basket',GETUTCDATE(),GETUTCDATE(),GETUTCDATE())
GO
