/****** Object:  Table [dbo].[class]    Script Date: 06/05/2014 17:16:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[class](
	[ClassName] [varchar](20) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Active] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[ClassId] [int] IDENTITY(1,1) NOT NULL,
	[Semester] [varchar](10) NULL,
	[Section] [varchar](10) NULL,
	[Year] [int] NULL,
 CONSTRAINT [PK__batch__5D55CE58239E4DCF] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
