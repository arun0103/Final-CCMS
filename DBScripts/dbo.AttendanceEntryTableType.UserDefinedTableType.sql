/****** Object:  UserDefinedTableType [dbo].[AttendanceEntryTableType]    Script Date: 06/05/2014 17:16:09 ******/
CREATE TYPE [dbo].[AttendanceEntryTableType] AS TABLE(
	[RollNo] [int] NULL,
	[FacultyClassId] [int] NULL,
	[Attendance] [bit] NULL,
	[AttendanceDate] [datetime] NULL,
	[routineid] [int] NOT NULL
)
GO
