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

    public partial class AddRoutine : System.Web.UI.Page
    {
        string connectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {
                //ContentPlaceHolder mcon = new ContentPlaceHolder();
                //mcon = (ContentPlaceHolder)Master.FindControl("pageContent2");
                //mcon.Visible = true;
                
                populateData();
                BindFacultyDropdown();
                BindClassDropdown();
                BindSemesterDropDown();
                
            }
        }

        public void showClasses()
        {
            ContentPlaceHolder mcon = new ContentPlaceHolder();
            mcon = (ContentPlaceHolder)Master.FindControl("pageContent2");
            mcon.Visible = true;

            RFV_FacultyList.Enabled = false;
            RFV_ClassList.Enabled = false;
            RFV_YearList.Enabled = false;
            RFV_Semester_drp.Enabled = false;
            //RFV_section_drp.Enabled = false;
            RFV_subjectlist_drp.Enabled = false;
            Page.Validate();

            connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            string sqlQuery = "Select routine.routineId, Users.FirstName + ' ' +Users.LastName as FacultyName, class.ClassName, routine.SectionName, routine.Semester from routine " +
                                    "inner join Users on routine.Fid = Users.UserID " +
                                    "join class on routine.ClassId = class.ClassId;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                RoutineGridView.DataSource = rdr;
                RoutineGridView.DataBind();
            }
            RFV_FacultyList.Enabled = true;
            RFV_ClassList.Enabled = true;
            RFV_YearList.Enabled = true;
            RFV_Semester_drp.Enabled = true;
            //RFV_section_drp.Enabled = true;
            RFV_subjectlist_drp.Enabled = true;
        }
        public void ClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string BatchName = ClassList.SelectedValue;

            String connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT Year, Semester, Section  FROM  class WHERE GETDATE() <=EndDate And className = @BatchName order by Semester DESC";
            cmd.Parameters.AddWithValue("@BatchName", ClassList.SelectedItem.ToString());
            section_drp.Enabled = true;
            section_drp.SelectedIndex = 0;
            try
            {

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int count = 0;
                    while (dr.Read())
                    {
                        YearList.SelectedIndex = Convert.ToInt32(dr["Year"]);
                        //YearList.Enabled = false;

                        Semester_drp.SelectedValue = Convert.ToString(dr["Semester"]);
                        //Semester_drp.Enabled = false;

                        count++;
                    }
                    if (count <2 ) // this shows there is only one section, so that section is selected and made not modifiable
                    {
                        section_drp.SelectedIndex = 3;
                        //section_drp.Enabled = false;
                    }
                    dr.Close();
                }
            }
            catch
            {
                Console.WriteLine("Some exception Occured");
            }
            finally
            {
                con.Close();
            }
            Semester_drp_SelectedIndexChanged(sender, e);
        }

        public void BindClassDropdown()
        {
            string command = "Select ClassId, ClassName from class order by ClassName";
            DataTable ClassTable = new DataTable();
            

            ClassTable = DataService.GetDataWithoutParameter(command);
            ClassList.DataValueField = "ClassId";
            ClassList.DataTextField = "ClassName";
            ClassList.DataSource = ClassTable;

            ClassList.DataBind();
            ClassList.Items.Insert(0, new ListItem("--- Select Class ---", "0"));


        }
        public void BindFacultyDropdown()
        {
            string command = "Select UserID, FirstName + ' ' + LastName as FacultyName from Users where role = 'Faculty'";
            DataTable FacultyTable = new DataTable();
            

            FacultyTable = DataService.GetDataWithoutParameter(command);

            FacultyList.DataValueField = "UserID";
            FacultyList.DataTextField = "FacultyName";
            FacultyList.DataSource = FacultyTable;
            FacultyList.DataBind();
            FacultyList.Items.Insert(0, new ListItem("--- Select Faculty ---", "0"));


        }
        public void BindSemesterDropDown()
        {
            string command = "Select distinct Semester from Syllabus";

            DataTable SemesterTable = new DataTable();
            

            SemesterTable = DataService.GetDataWithoutParameter(command);
            Semester_drp.DataValueField = "Semester";
            Semester_drp.DataTextField = "Semester";
            Semester_drp.DataSource = SemesterTable;
            Semester_drp.DataBind();
            Semester_drp.Items.Insert(0, new ListItem("--- Select Semester ---", "0"));
            

        }

        public void deleteRow(object sender, GridViewDeleteEventArgs e)
        {
            int id = e.RowIndex;
            SqlConnection con = new SqlConnection(connectionString);

            GridViewRow row = RoutineGridView.Rows[id] as GridViewRow;
            int routineId = Convert.ToInt32(row.Cells[0].Text);
            String query = "Delete from routine where routineId = @routineId";
            
            SqlParameter[] parameter = new SqlParameter[1];

            Routine routine = new Routine
            {
                Fid = routineId
            };
            parameter[0] = new SqlParameter("@routineId", routine.Fid);
            
            int result = DataService.WriteToDB(query,parameter);
            if (result == 1)
            {

                connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
                string sqlQuery = "Select routine.routineId, Users.FirstName + ' ' +Users.LastName as FacultyName, class.ClassName, routine.SectionName, Syllabus.Subject_Name from routine "+
                                   "inner join Users on routine.Fid = Users.UserID "+
                                   "join class on routine.ClassId = class.ClassId "+
                                   "join Syllabus on routine.SubjectId=Syllabus.Subject_Code order by Subject_Name;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    RoutineGridView.DataSource = rdr;
                    RoutineGridView.DataBind();
                    ContentPlaceHolder mcon = new ContentPlaceHolder();
                    mcon = (ContentPlaceHolder)Master.FindControl("pageContent2");
                    mcon.Visible = true;
                
                }
            }
          
        }
        protected void populateData()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            string sqlQuery = "Select routine.routineId, Users.FirstName + ' ' +Users.LastName as FacultyName, class.ClassName, routine.SectionName, Syllabus.Subject_Name from routine " +
                                   "inner join Users on routine.Fid = Users.UserID " +
                                   "join class on routine.ClassId = class.ClassId " +
                                   "join Syllabus on routine.SubjectId=Syllabus.Subject_Code order by Subject_Name;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                RoutineGridView.DataSource = rdr;
                RoutineGridView.DataBind();

            }
            ContentPlaceHolder mcon = new ContentPlaceHolder();
            mcon = (ContentPlaceHolder)Master.FindControl("pageContent2");
            mcon.Visible = true;
        }
        protected void RoutineAddBtn_Click(object sender, EventArgs e)
        {
            RFV_ClassList.Enabled = true;
            RFV_FacultyList.Enabled = true;
            //RFV_section_drp.Enabled = true;
            RFV_Semester_drp.Enabled = true;
            RFV_subjectlist_drp.Enabled = true;
            RFV_YearList.Enabled = true;
            Page.Validate();
          

            if (ClassList.SelectedIndex != 0 && FacultyList.SelectedIndex != 0 && Semester_drp.SelectedIndex != 0 && subjectlist_drp.SelectedIndex != 0 && YearList.SelectedIndex != 0)
            {

                Routine routine = new Routine
                {
                    Fid = Convert.ToInt16(FacultyList.SelectedValue),
                    ClassId = Convert.ToInt16(ClassList.SelectedValue),
                    EnrollYear = YearList.SelectedItem.Text,
                    Semester = Semester_drp.SelectedItem.Text,
                    SubjectID = subjectlist_drp.SelectedItem.Value,
                    SectionName = section_drp.SelectedItem.Text
                };

                int added = CCMSBusinessLayer.AddRoutine(routine);

                if (added > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Success !! ", "alert('Routine details has been added.');", true);
                }

                connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
                string sqlQuery = "Select routine.routineId, Users.FirstName + ' ' +Users.LastName as FacultyName, class.ClassName, routine.SectionName, Syllabus.Subject_Name from routine " +
                                   "inner join Users on routine.Fid = Users.UserID " +
                                   "join class on routine.ClassId = class.ClassId " +
                                   "join Syllabus on routine.SubjectId=Syllabus.Subject_Code order by Subject_Name;";
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    RoutineGridView.DataSource = rdr;
                    RoutineGridView.DataBind();
                }
                
                
            }
            clear();
        }
        public void clear()
        {
            FacultyList.SelectedIndex = 0;
            ClassList.SelectedIndex = 0;
            YearList.SelectedIndex = 0;
            section_drp.SelectedIndex = 0;
            Semester_drp.SelectedIndex = 0;
            subjectlist_drp.SelectedIndex = 0;

            RFV_ClassList.Enabled = false;
            RFV_FacultyList.Enabled = false;
           // RFV_section_drp.Enabled = false;
            RFV_Semester_drp.Enabled = false;
            RFV_subjectlist_drp.Enabled = false;
            RFV_YearList.Enabled = false;

            section_drp.Enabled = true;
            Semester_drp.Enabled = true;
            YearList.Enabled = true;
        }

        protected void clearBtn_Routine(object sender, EventArgs e)
        {
            clear();
        }

        protected void Semester_drp_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "select Subject_Code, Subject_Name from Syllabus where Semester = @semester",
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("@semester", Semester_drp.SelectedItem.Text);
            DataService objDataService = new DataService();
            DataTable result = objDataService.GetDataWithParameters(cmd);
            subjectlist_drp.DataSource = result;
            subjectlist_drp.DataValueField = "Subject_Code";
            subjectlist_drp.DataTextField = "Subject_Name";
            subjectlist_drp.DataBind();
            subjectlist_drp.Items.Insert(0, new ListItem("--- Select Subject ---", "0"));

        }

        protected void RoutineGrid_Edit(object sender, GridViewEditEventArgs e)
        {
            RoutineGridView.EditIndex = e.NewEditIndex;
            RoutineGridView.DataBind();
        }
        //protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    RoutineGridView.PageIndex = e.NewPageIndex;
        //    RoutineGridView.DataSource = rdr;
        //    RoutineGridView.DataBind();

        //}


    }
}

