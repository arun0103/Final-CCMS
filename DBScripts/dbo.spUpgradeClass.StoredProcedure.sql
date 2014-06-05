/****** Object:  StoredProcedure [dbo].[spUpgradeClass]    Script Date: 06/05/2014 17:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spUpgradeClass]
	@ClassIdFrom int,
	@ClassIdTo int
AS
BEGIN
	update studentClass set active = 0 where classId = @ClassIdFrom

	insert into studentClass (rollNo,classId,active,lastModifiedDate)
	Select rollNo,@ClassIdTo,1,GETDATE() from studentClass where classId = @ClassIdFrom
END
GO
