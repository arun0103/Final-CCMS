/****** Object:  UserDefinedTableType [dbo].[TableTypeStudent]    Script Date: 06/05/2014 17:16:09 ******/
CREATE TYPE [dbo].[TableTypeStudent] AS TABLE(
	[rollNo] [int] NULL,
	[firstName] [varchar](30) NULL,
	[lastName] [varchar](50) NULL,
	[email] [varchar](50) NULL
)
GO
