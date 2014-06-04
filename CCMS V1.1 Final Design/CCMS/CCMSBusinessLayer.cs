using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography;
namespace CCMS
{
    public class CCMSBusinessLayer
    {
        string connectionString;
        SqlConnection conDatabase;
        SqlCommand cmd;
        public void CreateConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            conDatabase = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            conDatabase.Open();
        }

        public void CloseConnection()
        {
            conDatabase.Close();
        }

        public SqlDataReader GetStudentListForAttendance(int facultyClassId, string sectionName, string subjectCode)
        {
            CreateConnection();
            cmd = new SqlCommand();
            cmd.CommandText = "Select S.RollNo, S.FirstName + ' ' + S.LastName As Name from students S inner join StudentClass SC on SC.rollNo = S.RollNo inner join class C on SC.ClassId = C.ClassId inner join routine R on R.EnrollYear = C.ClassName and C.Section = R.SectionName inner join faculty F on F.UserId = R.Fid inner join Syllabus Sy on Sy.Subject_Code = R.SubjectId where R.Fid = @fid and Sy.Subject_Name= @subjectCode and R.SectionName=@sectionName";
            cmd.Parameters.AddWithValue("@fid", facultyClassId);
            cmd.Parameters.AddWithValue("@sectionName", sectionName);
            cmd.Parameters.AddWithValue("@subjectCode", subjectCode);

            cmd.Connection = conDatabase;

            try
            {
                OpenConnection();

                SqlDataReader sdr = cmd.ExecuteReader();
                return sdr;
            }

            catch
            {
                throw new Exception();

            }

        }

        public SqlDataReader EditStudentListForAttendance(int facultyClassId, string sectionName, string subjectName, string changeDate)
        {

            CreateConnection();
            cmd = new SqlCommand();
            cmd.CommandText = "select A.RollNo,S.FirstName+' '+S.LastName as Name,A.Attendance from StudentAttendance A inner join students S on A.RollNo = S.RollNo inner join StudentClass SC on SC.rollNo = S.RollNo inner join class C on SC.ClassId = C.ClassId inner join routine R on R.EnrollYear = C.ClassName and C.Section = R.SectionName inner join faculty F on F.UserId = R.Fid inner join Syllabus Sy on Sy.Subject_Code = R.SubjectId where convert(varchar(10) , AttendanceDate, 120) = @changeDate and A.FacultyClassId = @fid and  Sy.Subject_Name= @subjectName and R.SectionName=@sectionName";
            cmd.Parameters.AddWithValue("@fid", facultyClassId);
            cmd.Parameters.AddWithValue("@sectionName", sectionName);
            cmd.Parameters.AddWithValue("@subjectName", subjectName);
            cmd.Parameters.AddWithValue("@changeDate", changeDate);

            cmd.Connection = conDatabase;
            try
            {
                OpenConnection();

                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    return sdr;
                }
                else
                {
                    return null;
                }
            }

            catch
            {
                throw new Exception();
            }
        }


        internal string getMd5Hash(System.Security.Cryptography.MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static int FindUserIdfromEmail(string email)
        {

            int UserId = 0;
            string query = "select UserID from Users Where UserEmail = @email";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@email", email);

            UserId = Convert.ToInt32(DataService.ReadDatabaseSingle(query, param));

            return UserId;

        }

        public static int AddFaculty(Faculty faculty)
        {
            int inserted = 0;
            string sqlQuery = "insert into Faculty (UserId,FirstName,LastName,Email,Contact, Active) values ( @UserID, @FirstName, @LastName, @Email, @Contact, @active)";
            SqlParameter[] parameter = new SqlParameter[6];

            //dataCommand.Parameters.AddWithValue("@FId", f_Id.Text);
            parameter[0] = new SqlParameter("@UserId", faculty.UserId);
            parameter[1] = new SqlParameter("@FirstName", faculty.FirstName);
            parameter[2] = new SqlParameter("@LastName", faculty.LastName);
            parameter[3] = new SqlParameter("@Email", faculty.Email);
            parameter[4] = new SqlParameter("@Contact", faculty.Contact);
            parameter[5] = new SqlParameter("@Active", faculty.Active);

            inserted = DataService.WriteToDB(sqlQuery, parameter);

            return inserted;
        }

        public static int AddBatch(Class batch)
        {
            int inserted = 0;
            string sqlQuery = "insert into class (ClassName, Year, Semester, Section,StartDate, EndDate, Active, CreatedDate, LastModifiedDate) values (@BatchName, @Year, @Semester, @Section, @StartDate, @EndDate, @active, @CreatedDate, @LastModifiedDate)";
            SqlParameter[] parameter = new SqlParameter[9];

            parameter[0] = new SqlParameter("@BatchName", batch.ClassName);
            parameter[1] = new SqlParameter("@Year", batch.Year);
            parameter[2] = new SqlParameter("@Semester", batch.Semester);
            parameter[3] = new SqlParameter("@Section", batch.Section);
            parameter[4] = new SqlParameter("@StartDate", batch.StartDate);
            parameter[5] = new SqlParameter("@EndDate", batch.EndDate);
            parameter[6] = new SqlParameter("@active", batch.Active);
            parameter[7] = new SqlParameter("@CreatedDate", batch.CreatedDate);
            parameter[8] = new SqlParameter("@LastModifiedDate", batch.LastModifiedDate);

            inserted = DataService.WriteToDB(sqlQuery, parameter);

            return inserted;
        }

        public static int AddRoutine(Routine routine)
        {
            int inserted = 0;
            string sqlQuery = "INSERT INTO routine(Fid,classId,EnrollYear, Semester, SubjectID, SectionName) VALUES (@Fid,@BatchID,@EnrollYear,@Semester, @SubjectID, @SectionName)";
            SqlParameter[] parameter = new SqlParameter[6];

            parameter[0] = new SqlParameter("@Fid", routine.Fid);
            parameter[1] = new SqlParameter("@BatchID", routine.ClassId);
            parameter[2] = new SqlParameter("@EnrollYear", routine.EnrollYear);
            parameter[3] = new SqlParameter("@Semester", routine.Semester);
            parameter[4] = new SqlParameter("@SubjectID", routine.SubjectID);
            parameter[5] = new SqlParameter("@SectionName", routine.SectionName);

            inserted = DataService.WriteToDB(sqlQuery, parameter);

            return inserted;
        }

        public DataTable GetClassCount(int Fid, string FromDate, string EndDate)
        {
            DataTable result = new DataTable();
            string query = "AttendanceReport";
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@Fid", Fid);
            parameter[1] = new SqlParameter("@FromDate", FromDate);
            parameter[2] = new SqlParameter("@EndDate", EndDate);

            result = DataService.GetDataStoreProcClassCount(query, parameter);

            return result;
        }


        public DataTable GetStudentClassMiss(int Fid, int studentId, string FromDate, string EndDate)
        {
            DataTable result = new DataTable();
            string query = "AttendanceReport";
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@Fid", Fid);
            parameter[1] = new SqlParameter("@StudentId", studentId);
            parameter[2] = new SqlParameter("@FromDate", FromDate);
            parameter[3] = new SqlParameter("@EndDate", EndDate);

            result = DataService.GetDataStoreProcStudentClassMiss(query, parameter);

            return result;
        }

        public DataTable displayAllStudentsRecord(int Fid, int studentId, string FromDate, string EndDate)
        {
            DataTable result = new DataTable();
            string query = "AttendanceReport";
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@Fid", null);
            parameter[1] = new SqlParameter("@StudentId", studentId);
            parameter[2] = new SqlParameter("@FromDate", FromDate);
            parameter[3] = new SqlParameter("@EndDate", EndDate);

            result = DataService.GetDataStoreProcStudentClassMiss(query, parameter);

            return result;
        }

        public DataTable displayOneTeacherAllStudentsRecord(int Fid, int studentId, string FromDate, string EndDate)
        {
            DataTable result = new DataTable();
            string query = "AttendanceReport";
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@Fid", Fid);
            parameter[1] = new SqlParameter("@StudentId", null);
            parameter[2] = new SqlParameter("@FromDate", FromDate);
            parameter[3] = new SqlParameter("@EndDate", EndDate);

            result = DataService.GetDataStoreProcStudentClassMiss(query, parameter);

            return result;
        }

        public DataTable displayallTeacherAllStudentsRecord(int Fid, int studentId, string FromDate, string EndDate)
        {
            DataTable result = new DataTable();
            string query = "AttendanceReport";
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@Fid", null);
            parameter[1] = new SqlParameter("@StudentId", null);
            parameter[2] = new SqlParameter("@FromDate", FromDate);
            parameter[3] = new SqlParameter("@EndDate", EndDate);

            result = DataService.GetDataStoreProcStudentClassMiss(query, parameter);

            return result;
        }

        public static string GetRandomPassword(int length)
        {
            char[] chars = "#@!abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string password = string.Empty;
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int x = random.Next(1, chars.Length);
                //Don't Allow Repetition of Characters
                if (!password.Contains(chars.GetValue(x).ToString()))
                    password += chars.GetValue(x);
                else
                    i--;
            }
            return password;
        }

        public static int AddUser(User user)
        {

            int inserted = 0;
            string pass = GetRandomPassword(5);
            user.Password = hashPassword(pass);
            string sqlQuery = "INSERT INTO users(userEmail,Password,FirstName,LastName,active,role,Contact) VALUES (@userEmail,@password,@firstName,@lastName,@Active,@Role,@Contact)";
            SqlParameter[] parameter = new SqlParameter[7];

            parameter[0] = new SqlParameter("@Role", user.Role);
            parameter[1] = new SqlParameter("@firstName", user.FirstName);
            parameter[2] = new SqlParameter("@lastName", user.LastName);
            parameter[3] = new SqlParameter("@userEmail", user.Email);
            parameter[4] = new SqlParameter("@Contact", user.Contact);
            parameter[5] = new SqlParameter("@Active", user.Active);
            parameter[6] = new SqlParameter("@password", user.Password);
            inserted = DataService.WriteToDB(sqlQuery, parameter);


            if (inserted > 0)
            {
                SendGeneratedPassword(user.Email, pass, user.FirstName);
            }
            return inserted;
        }

        private static string hashPassword(string pass)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());

            CCMSBusinessLayer md = new CCMSBusinessLayer();
            string hash = "";
            string source = pass + result;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = md.getMd5Hash(md5Hash, source);
            }
            string combinedPassword = hash + ":" + result;

            return combinedPassword;
        }


        public static void SendGeneratedPassword(string email, string password, string fname)
        {
            string college = "DWIT";
            MailMessage message = new MailMessage();
            MailAddress Sender = new MailAddress(ConfigurationManager.AppSettings["smtpUser"]);
            MailAddress receiver = new MailAddress(email);
            message.Subject = "Password for CCMS";
            SmtpClient smtp = new SmtpClient();
            {
                smtp.Host = ConfigurationManager.AppSettings["smtpServer"];
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(
                  ConfigurationManager.AppSettings["smtpUser"],
                  ConfigurationManager.AppSettings["smtpPass"]);

            }
            message.From = Sender;
            message.To.Add(receiver);
            message.Body = "Dear " + fname + "," + Environment.NewLine + Environment.NewLine + "Welcome to CCMS. Your password for login is: " + password + Environment.NewLine + Environment.NewLine + "Thanks," + Environment.NewLine + college;

            smtp.Send(message);
        }

        public static DataTable GetUsers(string role)
        {
            string query = "select * from Users  where role = @role ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@role", role);

            DataTable Users = DataService.ReadDB(query, param);
            return Users;
        }

        public static bool isExistingUser(string email)
        {

            string sqlQuery = "select UserID from Users where UserEmail= @email";
            SqlParameter[] parameter = new SqlParameter[1];

            parameter[0] = new SqlParameter("@email", email);


            int checkCount = DataService.ReadDatabaseSingleValue(sqlQuery, parameter);

            if (checkCount > 0)
            {
                return true;
            }
            return false;
        }
    }
}