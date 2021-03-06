/****** Object:  Table [dbo].[StudentClass]    Script Date: 06/05/2014 17:16:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[rollNo] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[class] ([ClassId])
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[class] ([ClassId])
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD FOREIGN KEY([rollNo])
REFERENCES [dbo].[students] ([RollNo])
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD FOREIGN KEY([rollNo])
REFERENCES [dbo].[students] ([RollNo])
GO
