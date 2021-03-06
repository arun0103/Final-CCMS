/****** Object:  StoredProcedure [dbo].[Attendance]    Script Date: 06/05/2014 17:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[Attendance]
	-- Add the parameters for the stored procedure here
	@type varchar(10) = null,
	@changeDate DateTime = null,
	@AttendanceEntries AttendanceEntryTableType READONLY
	
AS
BEGIN
	if @type='insert'
	BEGIN	
    -- Insert statements for procedure here
		insert into dbo.StudentAttendance
			(RollNo,FacultyClassId,Attendance,AttendanceDate,routineid)
			SELECT * FROM @AttendanceEntries
	END

	else if @type = 'update'
	BEGIN
		UPDATE A SET A.Attendance = B.Attendance from [dbo].[StudentAttendance] A inner join  @AttendanceEntries  B  on A.[RollNo]= B.[RollNo] where convert(varchar(10) , A.AttendanceDate, 120)=@changeDate
	END
END
GO
