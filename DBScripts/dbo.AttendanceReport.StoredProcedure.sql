/****** Object:  StoredProcedure [dbo].[AttendanceReport]    Script Date: 06/05/2014 17:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author:		<Author,,Name>

-- Create date: <Create Date,,>

-- Description:	<Description,,>

-- =============================================
CREATE PROCEDURE [dbo].[AttendanceReport]

	(

	@Fid int = null,
	@Studentid int = null,
	@FromDate DateTime,
	@EndDate DateTime

	)

AS

BEGIN	

	if(@Fid is not null and @Studentid is null)

	BEGIN
	 
	 Select count(distinct(A.AttendanceDate)) as TotalClass,COUNT(NULLIF( A.Attendance, 0 )) as present, COUNT(NULLIF( A.Attendance, 1 )) as Missedclass,
	 round((cast(COUNT(NULLIF( Attendance, 0 ))as float) /count(distinct(A.AttendanceDate))*100),2 )as AttendancePercent,S.FirstName + ' ' + S.LastName As StudentName,U.FirstName + ' ' + U.LastName As FacultyName,Sy.Subject_Name from students S inner join StudentClass SC 
	 on SC.rollNo = S.RollNo inner join routine R on SC.ClassId = R.ClassId inner join Syllabus Sy 
	 on Sy.Subject_Code = R.SubjectId and R.Semester = Sy.Semester inner join StudentAttendance A on A.RollNo = S.RollNo and A.routineid = R.routineId inner join Users U on R.Fid = U.UserID where R.Fid = @Fid and  A.AttendanceDate between @FromDate and @EndDate  group by Sy.Subject_Name,S.FirstName+' '+S.LastName,U.FirstName+' '+U.LastName,A.RollNo;

	 
	END

	else if(@Fid is not null and @Studentid is not null)
	
	BEGIN
		Select count(distinct(A.AttendanceDate)) as TotalClass,COUNT(NULLIF( A.Attendance, 0 )) as present, COUNT(NULLIF( A.Attendance, 1 )) as Missedclass,
		round((cast(COUNT(NULLIF( Attendance, 0 ))as float) /count(distinct(A.AttendanceDate))*100),2 )as AttendancePercent,S.RollNo,S.FirstName + ' ' + S.LastName As StudentName,U.FirstName + ' ' + U.LastName As FacultyName,Sy.Subject_Name,A.FacultyClassId from students S inner join StudentClass SC 
		on SC.rollNo = S.RollNo inner join routine R on SC.ClassId = R.ClassId inner join Syllabus Sy 
		on Sy.Subject_Code = R.SubjectId inner join StudentAttendance A on A.RollNo = S.RollNo and A.routineid = R.routineId inner join faculty F 
		on F.UserId = R.Fid inner join Users U on R.Fid = U.UserID  where R.Fid = @Fid and S.RollNo =@Studentid and  A.AttendanceDate between @FromDate and @EndDate group by Sy.Subject_Name,S.FirstName+' '+S.LastName,U.FirstName+' '+U.LastName,S.RollNo,A.FacultyClassId;
	END

	else if(@Fid is null and @Studentid is not null)
	BEGIN
		Select count(distinct(A.AttendanceDate)) as TotalClass,COUNT(NULLIF( A.Attendance, 0 )) as present, COUNT(NULLIF( A.Attendance, 1 )) as Missedclass,
		round((cast(COUNT(NULLIF( Attendance, 0 ))as float) /count(distinct(A.AttendanceDate))*100),2 )as AttendancePercent,S.RollNo,S.FirstName + ' ' + S.LastName As StudentName,U.FirstName + ' ' + U.LastName As FacultyName,Sy.Subject_Name from students S inner join StudentClass SC 
		on SC.rollNo = S.RollNo inner join routine R on SC.ClassId = R.ClassId inner join Syllabus Sy 
		on Sy.Subject_Code = R.SubjectId inner join StudentAttendance A on A.RollNo = S.RollNo and A.routineid = R.routineId inner join faculty F 
		on F.UserId = R.Fid inner join Users U on R.Fid = U.UserID  where  S.RollNo = @Studentid and  A.AttendanceDate between @FromDate and @EndDate group by Sy.Subject_Name,S.FirstName+' '+S.LastName,U.FirstName+' '+U.LastName,S.RollNo;
	END

	else if(@Fid is null and @Studentid is null)
	BEGIN
		Select count(distinct(A.AttendanceDate)) as TotalClass,COUNT(NULLIF( A.Attendance, 0 )) as present, COUNT(NULLIF( A.Attendance, 1 )) as Missedclass,
		round((cast(COUNT(NULLIF( Attendance, 0 ))as float) /count(distinct(A.AttendanceDate))*100),2 )as AttendancePercent,S.RollNo,S.FirstName + ' ' + S.LastName As StudentName,U.FirstName + ' ' + U.LastName As FacultyName,Sy.Subject_Name,A.FacultyClassId from students S inner join StudentClass SC 
		on SC.rollNo = S.RollNo inner join routine R on SC.ClassId = R.ClassId inner join Syllabus Sy 
		on Sy.Subject_Code = R.SubjectId inner join StudentAttendance A on A.RollNo = S.RollNo and R.routineId=A.routineid inner join faculty F 
		on F.UserId = R.Fid inner join Users U on A.FacultyClassId = U.UserID and  A.AttendanceDate between @FromDate and @EndDate group by Sy.Subject_Name,S.FirstName+' '+S.LastName,U.FirstName+' '+U.LastName,S.RollNo,A.FacultyClassId;


	END


END
GO
