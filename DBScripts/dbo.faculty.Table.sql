/****** Object:  Table [dbo].[faculty]    Script Date: 06/05/2014 17:16:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[faculty](
	[Fid] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[FirstName] [varchar](30) NULL,
	[LastName] [varchar](30) NULL,
	[Email] [varchar](30) NULL,
	[Contact] [bigint] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK__faculty__C1D1314A29572725] PRIMARY KEY CLUSTERED 
(
	[Fid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[faculty]  WITH CHECK ADD  CONSTRAINT [FK__faculty__UserId__2B3F6F97] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[faculty] CHECK CONSTRAINT [FK__faculty__UserId__2B3F6F97]
GO
