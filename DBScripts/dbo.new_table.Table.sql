/****** Object:  Table [dbo].[new_table]    Script Date: 06/05/2014 17:16:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[new_table](
	[RollNo] [int] NOT NULL,
	[FacultyClassId] [int] NOT NULL,
	[Attendance] [bit] NOT NULL,
	[AttendanceDate] [datetime] NOT NULL,
	[routineid] [int] NOT NULL
) ON [PRIMARY]
GO
