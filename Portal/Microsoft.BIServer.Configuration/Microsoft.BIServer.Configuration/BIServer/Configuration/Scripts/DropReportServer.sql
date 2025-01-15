IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'ReportServerTempDB') DROP DATABASE [ReportServerTempDB] 
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'ReportServer') DROP DATABASE [ReportServer]
GO
