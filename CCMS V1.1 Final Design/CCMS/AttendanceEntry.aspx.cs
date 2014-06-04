using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCMS
{
    public partial class AttendanceEntry : System.Web.UI.Page
    {
        string connectionString;
        SqlConnection conDatabase;
        SqlCommand cmd;
        static int facultyClassId = 0;
        static string sectionName = "";
        static string subjectCode = "";
        static string subName = "";
        static int routineId = 0;
        String userChoosedDate = DateTime.Now.ToShortDateString();
        CCMSBusinessLayer objBL = new CCMSBusinessLayer();


        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                facultyClassId = Convert.ToInt32(Session["UserId"]);
                sectionName = Session["SectionName"].ToString();
                subjectCode = Session["Subject_Code"].ToString();
                subName = Session["Subject_Name"].ToString();
                routineId = Convert.ToInt32(Session["routineId"].ToString());
                if (!IsPostBack)
                {
                    recordDateV.Text = DateTime.Now.ToShortDateString();
                    LblSubject.Text = subName;
                    String format = "yyyy-MM-dd";
                    DateTime dt = Convert.ToDateTime(userChoosedDate);
                    string convertedDate = dt.ToString(format);
                    userChoosedDate = convertedDate;
                    CreateConnection();
                    cmd = new SqlCommand();

                    cmd.CommandText = "select count(AttendanceDate) as counts from studentAttendance where convert(varchar(10) , AttendanceDate, 120) = @changeDate and FacultyClassId =@facultyClassId and routineId = @routineId";
                    cmd.Parameters.AddWithValue("@changeDate", userChoosedDate);
                    cmd.Parameters.AddWithValue("@facultyClassId", facultyClassId);
                    cmd.Parameters.AddWithValue("@routineId", routineId);

                    cmd.Connection = conDatabase;
                    try
                    {
                        OpenConnection();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            string count = sdr["counts"].ToString();//it will assign O if no values  previously in database otherwise assign the exact count

                            int checkCount = Convert.ToInt32(count);

                            if (checkCount > 0)
                            {

                                GridView2.DataSource = objBL.EditStudentListForAttendance(facultyClassId, sectionName, subName, userChoosedDate);
                                GridView2.DataBind();
                            }
                            else
                            {
                                GridView1.DataSource = objBL.GetStudentListForAttendance(facultyClassId, sectionName, subName);
                                GridView1.DataBind();

                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("some exception occured");
                    }
                }
            }
            catch
            {
                Console.WriteLine("some exception occured");
            }
        }

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

        protected void Calendar_Button_Click(object sender, ImageClickEventArgs e)
        {
            if (calendar.Visible)
            {
                calendar.Visible = false;
            }
            else
            {
                calendar.Visible = true;
                calendar.VisibleDate = DateTime.Now;
            }
        }

        protected void Calendar1_SelectionChanged1(object sender, EventArgs e)
        {
            recordDateV.Text = calendar.SelectedDate.ToShortDateString();
            calendar.Visible = false;
            String format = "yyyy-MM-dd";
            DateTime dt = Convert.ToDateTime(recordDateV.Text);

            string convertedDate = dt.ToString(format); //date converted to format yyyy-MM-dd
            DateTime dateToday = DateTime.Now;

            if (dt > dateToday)//comparison of selected date either it is from previous day or coming day
            {
                System.Windows.Forms.MessageBox.Show(" Let's not take TOMORROW's attendance TODAY.");
                recordDateV.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                userChoosedDate = convertedDate;
                editAttendance(convertedDate);
            }
        }

        protected bool GetStatus(string str)//checking the status of attendance either true or false from database table
        {
            if (str == "1")
                return true;
            else
                return false;
        }

        private void editAttendance(string convertedDate)
        {
            GridView1.Visible = false;//changing previous Gridview1 state false from true

            GridView2.DataSource = objBL.EditStudentListForAttendance(facultyClassId, sectionName, subName, convertedDate);//

            if (GridView2.DataSource == null)
            {
                System.Windows.Forms.MessageBox.Show("Attendace doesnot exist of this particular date");
                recordDateV.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                GridView2.DataBind();//binding the selected student in the Gridview2
            }
        }

        protected void saveAttendance(Object sender, EventArgs e)
        {
            //firstly changing the date format to yyyy-MM-dd
            recordDateV.Text = calendar.SelectedDate.ToShortDateString();
            string todayD = DateTime.Now.ToShortDateString();
            string convertedDate = null;
            String format = "yyyy-MM-dd";

            if (todayD == userChoosedDate && recordDateV.Text == "1/1/0001")
            {
                DateTime dt = Convert.ToDateTime(userChoosedDate);
                convertedDate = dt.ToString(format); //date converted to format yyyy-MM-dd
                recordDateV.Text = todayD;
            }
            else
            {
                DateTime dt = Convert.ToDateTime(recordDateV.Text);

                convertedDate = dt.ToString(format); //date converted to format yyyy-MM-dd
            }

            CreateConnection();
            cmd = new SqlCommand();
            cmd.CommandText = "select count(AttendanceDate) as counts from studentAttendance where convert(varchar(10) , AttendanceDate, 120) = @changeDate and FacultyClassId =@fid and routineId = @routineId";
            cmd.Parameters.AddWithValue("@changeDate", convertedDate);
            cmd.Parameters.AddWithValue("@fid", facultyClassId);
            cmd.Parameters.AddWithValue("@routineId", routineId);
            cmd.Connection = conDatabase;

            try
            {
                OpenConnection();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    string count = sdr["counts"].ToString();//it will assign O if no values  previously in database otherwise assign the exact count

                    int checkCount = Convert.ToInt32(count);

                    if (checkCount > 0) //if count greater than 0 then update otherwise insert to table of database
                    {
                        DateTime scheduledTime = DateTime.Now;
                        DataTable editable = new DataTable();
                        editable.Columns.Add("RollNo", typeof(int));
                        editable.Columns.Add("FacultyClassId", typeof(int));
                        editable.Columns.Add("Attendance", typeof(Boolean));
                        editable.Columns.Add("AttendanceDate", typeof(DateTime));
                        editable.Columns.Add("routineid", typeof(int));

                        DataRow dr = null;

                        foreach (GridViewRow gr in GridView2.Rows)
                        {
                            dr = editable.NewRow();
                            dr["RollNo"] = gr.Cells[0].Text;
                            dr["FacultyClassId"] = facultyClassId;

                            CheckBox cb = (CheckBox)(gr.Cells[2].FindControl("chkbox"));

                            if (cb.Checked)
                            {
                                dr["Attendance"] = true;
                            }
                            else
                            {
                                dr["Attendance"] = false;
                            }

                            dr["AttendanceDate"] = scheduledTime;
                            dr["routineid"] = routineId;

                            editable.Rows.Add(dr);
                        }

                        DataTable AttendanceTable = editable.GetChanges(DataRowState.Added);
                        string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

                        SqlConnection connection = new SqlConnection(connectionString);
                        SqlCommand insertCommand = new SqlCommand("Attendance", connection);
                        insertCommand.Parameters.AddWithValue("@type", "update");
                        insertCommand.CommandType = CommandType.StoredProcedure;
                        insertCommand.Parameters.AddWithValue("@changeDate", convertedDate);

                        SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@AttendanceEntries", AttendanceTable);
                        tvpParam.SqlDbType = SqlDbType.Structured;

                        tvpParam.TypeName = "dbo.AttendanceEntryTableType";
                        try
                        {
                            connection.Open();
                            int affectedRows = insertCommand.ExecuteNonQuery();
                            if (affectedRows > 0)
                            {
                                System.Windows.Forms.MessageBox.Show(affectedRows + " entries updated");


                            }
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (connection != null)
                            {
                                connection.Close();
                            }
                        }
                    }
                    else
                    {
                        DateTime scheduledTime = DateTime.Now;
                        CCMSBusinessLayer objBL = new CCMSBusinessLayer();
                        DataTable editable = new DataTable();
                        editable.Columns.Add("RollNo", typeof(int));
                        editable.Columns.Add("FacultyClassId", typeof(int));
                        editable.Columns.Add("Attendance", typeof(Boolean));
                        editable.Columns.Add("AttendanceDate", typeof(DateTime));
                        editable.Columns.Add("routineid", typeof(int));

                        DataRow dr = null;

                        foreach (GridViewRow gr in GridView1.Rows)
                        {
                            dr = editable.NewRow();
                            dr["RollNo"] = gr.Cells[0].Text;
                            dr["FacultyClassId"] = facultyClassId;

                            CheckBox cb = (CheckBox)(gr.Cells[2].FindControl("CheckBoxAttendance"));

                            if (cb.Checked)
                            {
                                dr["Attendance"] = true;
                            }
                            else
                            {
                                dr["Attendance"] = false;
                            }

                            dr["AttendanceDate"] = scheduledTime;
                            dr["routineid"] = routineId;

                            editable.Rows.Add(dr);
                        }

                        DataTable AttendanceTable = editable.GetChanges(DataRowState.Added);
                        string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

                        SqlConnection connection = new SqlConnection(connectionString);
                        SqlCommand insertCommand = new SqlCommand("Attendance", connection);
                        insertCommand.Parameters.AddWithValue("@type", "insert");
                        insertCommand.CommandType = CommandType.StoredProcedure;

                        SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@AttendanceEntries", AttendanceTable);

                        tvpParam.SqlDbType = SqlDbType.Structured;

                        tvpParam.TypeName = "dbo.AttendanceEntryTableType";
                        try
                        {
                            connection.Open();
                            int affectedRows = insertCommand.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                System.Windows.Forms.MessageBox.Show(affectedRows + " entries stored");
                            }
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (connection != null)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
            catch
            {
                throw new Exception();
            }
        }

    }
}