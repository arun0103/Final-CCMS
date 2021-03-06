/****** Object:  Table [dbo].[routine]    Script Date: 06/05/2014 17:16:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[routine](
	[routineId] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NULL,
	[SubjectId] [varchar](10) NULL,
	[SectionName] [varchar](2) NULL,
	[EnrollYear] [int] NULL,
	[Semester] [varchar](4) NULL,
	[Fid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[routineId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[routine]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[class] ([ClassId])
GO
ALTER TABLE [dbo].[routine]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[class] ([ClassId])
GO
ALTER TABLE [dbo].[routine]  WITH CHECK ADD FOREIGN KEY([Fid])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[routine]  WITH CHECK ADD FOREIGN KEY([Fid])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[routine]  WITH CHECK ADD FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Syllabus] ([Subject_Code])
GO
ALTER TABLE [dbo].[routine]  WITH CHECK ADD FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Syllabus] ([Subject_Code])
GO
