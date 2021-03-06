/****** Object:  Table [dbo].[Syllabus]    Script Date: 06/05/2014 17:16:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Syllabus](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[Subject_Code] [varchar](10) NOT NULL,
	[Subject_Name] [varchar](100) NULL,
	[Semester] [varchar](10) NULL,
	[Year] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Subject_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
