using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCMS
{
    public class DataService
    {
        private static string _connectionString;
        private static SqlConnection Connection;

        static DataService()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
        }


        public static DataTable GetDataWithoutParameter(string Command)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            DataTable result = new DataTable();
            using (SqlConnection Connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand DataCommand = Connection.CreateCommand())
                {
                    Connection.Open();
                    DataCommand.CommandType = CommandType.Text;
                    DataCommand.CommandText = Command;
                    SqlDataAdapter adapter = new SqlDataAdapter(DataCommand);

                    adapter.Fill(result);

                }
            }

            return result;
        }



        public static int InsertIntoDatabase(string query, SqlParameter[] parameters)
        {
            int result = 0;
            using (SqlCommand command = new SqlCommand(query))
            {
                command.Connection = Connection;
                command.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                Connection.Open();
                result = command.ExecuteNonQuery();
                Connection.Close();
            }

            return result;
        }

            public static int WriteToDB(string query, SqlParameter[] parameters)
        {
            int result = 0;
            using (SqlCommand command = new SqlCommand(query))
            {
                command.Connection = Connection;
                command.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                Connection.Open();
                result = command.ExecuteNonQuery();
                Connection.Close();

            }

            return result;
        }

        public static DataTable GetDataStoreProcClassCount(string procedureName, SqlParameter[] parameters)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            DataTable result = new DataTable();


            using (SqlConnection Connection = new SqlConnection(_connectionString))
            {

                using (SqlCommand DataCommand = Connection.CreateCommand())
                {

                    DataCommand.CommandText = procedureName;
                    DataCommand.Parameters.AddWithValue("@Fid", parameters[0].SqlValue);
                    DataCommand.Parameters.AddWithValue("@FromDate", parameters[1].SqlValue);
                    DataCommand.Parameters.AddWithValue("@EndDate", parameters[2].SqlValue);
                    DataCommand.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = DataCommand;

                    adapter.Fill(result);

                }


            }

            return result;

        }

        public static DataTable GetDataStoreProcStudentClassMiss(string procedureName, SqlParameter[] parameters)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            DataTable result = new DataTable();


            using (SqlConnection Connection = new SqlConnection(_connectionString))
            {

                using (SqlCommand DataCommand = Connection.CreateCommand())
                {

                    DataCommand.CommandText = procedureName;
                    DataCommand.Parameters.AddWithValue("@Fid", parameters[0].SqlValue);
                    DataCommand.Parameters.AddWithValue("@StudentId", parameters[1].SqlValue);
                    DataCommand.Parameters.AddWithValue("@FromDate", parameters[2].SqlValue);
                    DataCommand.Parameters.AddWithValue("@EndDate", parameters[3].SqlValue);
                    DataCommand.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = DataCommand;

                    adapter.Fill(result);

                }


            }

            return result;

        }

        public DataTable GetDataWithParameters(SqlCommand command)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            DataTable result = new DataTable();
            using (SqlConnection Connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand DataCommand = command)
                {
                    Connection.Open();
                    DataCommand.CommandType = CommandType.Text;
                    DataCommand.Connection = Connection;
                    SqlDataAdapter adapter = new SqlDataAdapter(DataCommand);

                    adapter.Fill(result);

                }
            }

            return result;
        }
        public static string ReadDatabaseSingle(string query, SqlParameter[] parameter)
        {
            string result = String.Empty;

            using (SqlCommand command = new SqlCommand(query))
            {
                command.Connection = Connection;
                command.CommandType = CommandType.Text;

                if (parameter != null)
                {
                    command.Parameters.AddRange(parameter);
                }
                Connection.Open();
                result = command.ExecuteScalar().ToString();
                Connection.Close();

            }

            return result;
        }

        public static int ReadDatabaseSingleValue(string query, SqlParameter[] parameter)
        {
            //string result = String.Empty;
            int result = 0;
            SqlDataAdapter ad = new SqlDataAdapter();
            DataTable dt = new DataTable();

            using (SqlCommand command = new SqlCommand(query))
            {
                command.Connection = Connection;
                command.CommandType = CommandType.Text;


                if (parameter != null)
                {
                    command.Parameters.AddRange(parameter);
                }

                try
                {
                    ad.SelectCommand = command;
                    Connection.Open();
                    ad.Fill(dt);
                    result = dt.Rows.Count;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    Connection.Close();
                }
            }
            return result;
        }
        public void BindFaculty(DropDownList ControlId)
        {
            string command = "Select UserId, FirstName + ' ' + LastName as FacultyName from faculty";
            DataTable FacultyTable = new DataTable();

            FacultyTable = GetDataWithoutParameter(command);
            ControlId.DataValueField = "UserId";
            ControlId.DataTextField = "FacultyName";
            ControlId.DataSource = FacultyTable;
            ControlId.DataBind();

        }
        public void BindStudent(DropDownList ControlId)
        {
            string command = "select RollNo, FirstName + ' '+ LastName as StudentName from Students";
            DataTable StudentTable = new DataTable();

            StudentTable = GetDataWithoutParameter(command);
            ControlId.DataValueField = "RollNo";
            ControlId.DataTextField = "StudentName";
            ControlId.DataSource = StudentTable;
            ControlId.DataBind();
        }

        public void bindAttendees(int attendancePercent)
        {
            //string command = "select  + ' '+ LastName as StudentName from Students";
            //DataTable StudentTable = new DataTable();
            //StudentTable = GetDataWithoutParameter(command);
            //ControlId.DataValueField = "RollNo";
            //ControlId.DataTextField = "StudentName";
            //ControlId.DataSource = StudentTable;
            //ControlId.DataBind();

        }

        public static DataTable ReadDB(string query, SqlParameter[] parameter)
        {
            DataTable result = new DataTable();

            using (SqlCommand command = new SqlCommand(query))
            {
                command.Connection = Connection;
                command.CommandType = CommandType.Text;

                if (parameter != null)
                {
                    command.Parameters.AddRange(parameter);
                }
                Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(result);
                Connection.Close();

            }

            return result;
        }

        public static int InsertIntoDatabaseSP(string procName, SqlParameter[] parameters)
        {
            int result = 0;

            using (SqlCommand command = new SqlCommand(procName))
            {
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                Connection.Open();
                result = command.ExecuteNonQuery();

            }
            return result;
        }

        public static int InsertStudentsIntoDbWithSP(DataTable dt, int classid)
        {
            int result = 0;

            SqlCommand insertCommand = new SqlCommand("spInsertStudents", Connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@StudentList", dt);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "dbo.TableTypeStudent";
            insertCommand.Parameters.AddWithValue("@ClassId", classid);

            try
            {
                Connection.Open();
                result = insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (Connection != null)
                {
                    Connection.Close();
                }
            }

            return result;
        }
    }
}