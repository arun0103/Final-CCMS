/****** Object:  StoredProcedure [dbo].[spInsertStudents]    Script Date: 06/05/2014 17:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertStudents]

	@StudentList TableTypeStudent READONLY	,
	@ClassId int	
AS
BEGIN
	insert into students (rollNo,firstName,lastName,email)
	SELECT * from (select * from @StudentList except select * from students) as uniqueStudents
	
	
	Insert into studentClass (rollNo,classId,active,lastModifiedDate)
	Select rollNo,@ClassId ,1,getdate() from @StudentList except Select rollNo,@ClassId ,1,getdate() from studentClass
	
	
	
END
GO
