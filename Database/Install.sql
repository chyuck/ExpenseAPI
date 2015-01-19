SET NOCOUNT ON
GO

PRINT CAST(SYSDATETIME() AS NVARCHAR) + N' - Start creating database'

USE [master]
GO

IF DB_ID(N'ExpenseAPI') IS NOT NULL
BEGIN
	DROP DATABASE [ExpenseAPI]
	PRINT CAST(SYSDATETIME() AS NVARCHAR) + N' - Database was dropped'
END
GO

:On Error exit

:r CreateDatabase.sql
PRINT CAST(SYSDATETIME() AS NVARCHAR) + N' - Database schema was created'
GO

:r CreateInitialData.sql
PRINT CAST(SYSDATETIME() AS NVARCHAR) + N' - Database initial data was created'
GO

PRINT CAST(SYSDATETIME() AS NVARCHAR) + N' - Database creating was completed'
GO