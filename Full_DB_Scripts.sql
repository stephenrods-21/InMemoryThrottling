CREATE DATABASE hoteldb
Go

USE hoteldb
go


IF OBJECT_ID ('dbo.Hotels') IS NOT NULL
	DROP TABLE dbo.Hotels
GO

CREATE TABLE [dbo].[Hotels] (
    [CITY]    NVARCHAR (50) NOT NULL,
    [HOTELID] INT           IDENTITY (1, 1) NOT NULL,
    [ROOM]    NVARCHAR (50) NOT NULL,
    [PRICE]   INT           NOT NULL,
    CONSTRAINT [PK_Hotels] PRIMARY KEY CLUSTERED ([HOTELID] ASC)
);
GO


INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Bangkok', N'Deluxe', 1000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Amsterdam', N'Superior', 2000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Sweet Suite', 1300)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Amsterdam', N'Deluxe', 2200)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Sweet Suite', 1200)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Bangkok', N'Superior', 2000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Deluxe', 1600)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Bangkok', N'Superior', 2400)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Amsterdam', N'Sweet Suite', 30000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Superior', 1100)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Bangkok', N'Deluxe', 60)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Deluxe', 1800)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Amsterdam', N'Superior', 1000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Bangkok', N'Sweet Suite', 25000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Bangkok', N'Deluxe', 900)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Superior', 800)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Deluxe', 2800)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Bangkok', N'Sweet Suite', 5300)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Superior', 1000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Superior', 4444)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Deluxe', 7000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Sweet Suite', 14000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Amsterdam', N'Deluxe', 5000)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Superior', 1400)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Ashburn', N'Deluxe', 1900)
INSERT INTO [dbo].[Hotels] ([CITY], [ROOM], [PRICE]) VALUES (N'Amsterdam', N'Deluxe', 2300)

GO

create table [dbo].[Logs](
	[LogId] [int] IDENTITY(1,1) not null,
	[Level] [varchar](max) not null,
	[CallSite] [varchar](max) not null,
	[Type] [varchar](max) not null,
	[Message] [varchar](max) not null,
	[StackTrace] [varchar](max) not null,
	[InnerException] [varchar](max) not null,
	[AdditionalInfo] [varchar](max) not null,
	[LoggedOnDate] [datetime] not null constraint [df_logs_loggedondate]  default (getutcdate()),

	constraint [pk_logs] primary key clustered 
	(
		[LogId]
	)
)

GO

/*
	Create InsertLog stored procedure
*/

CREATE PROCEDURE [dbo].[InsertLog] 
(
	@level varchar(max),
	@callSite varchar(max),
	@type varchar(max),
	@message varchar(max),
	@stackTrace varchar(max),
	@innerException varchar(max),
	@additionalInfo varchar(max)
)
as

insert into dbo.Logs
(
	[Level],
	CallSite,
	[Type],
	[Message],
	StackTrace,
	InnerException,
	AdditionalInfo
)
values
(
	@level,
	@callSite,
	@type,
	@message,
	@stackTrace,
	@innerException,
	@additionalInfo
)

go
