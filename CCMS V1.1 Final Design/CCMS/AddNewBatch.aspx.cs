using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Drawing;

namespace CCMS
{
    public partial class NewClass : System.Web.UI.Page
    {
        string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public void DropDownSection_TextChanged(object sender, EventArgs e)
        {
            // code to be displayed in the same page.
        }
        protected void makeControlValidationFalse()
        {   
            ANB_batch.Enabled = false;
            ANB_semester.Enabled = false;
            //ANB_section.Enabled = false;
            ANB_startDate.Enabled = false;
            ANB_endDate.Enabled = false;
        }
        
        protected void clearBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
        #region Reset
        private void Reset()
        {
            batchName.Text = "";
            ddl_semester.SelectedIndex = 0;
            ddl_section.SelectedIndex = 0;
            startDate.Text = "";
            endDate.Text = "";
            activeCB.Checked = false;
            startDateCalendar.Visible = false;
            endDateCalendar.Visible = false;     
            makeControlValidationFalse();
        }
        #endregion

        #region Check in CLick
        protected void addBtn_Click(object sender, EventArgs e)
        {   //Newly Added to Validate the Text Box;
            if (batchName.Text == "" || ddl_semester.SelectedIndex == 0 || startDate.Text == "" || endDate.Text != "")
            {
                ANB_batch.Enabled = true;
                ANB_semester.Enabled = true;
                //ANB_section.Enabled = true;
                ANB_startDate.Enabled = true;
                ANB_endDate.Enabled = true;
                Page.Validate();
            }
            //&& ddl_section.SelectedIndex != 0
            int added = 0;
            if (batchName.Text != "" && ddl_semester.SelectedIndex != 0  && startDate.Text != "" && endDate.Text != "")
            {
                connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
                
                string sqlQuery = "insert into class (ClassName, StartDate, EndDate, Active, CreatedDate) values (@BatchName, @StartDate, @EndDate, @active, @CreatedDate)";
                using (SqlConnection dataConnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand dataCommand = dataConnection.CreateCommand())
                    {
                        dataConnection.Open();
                        dataCommand.CommandType = CommandType.Text;
                        dataCommand.CommandText = sqlQuery;

                        dataCommand.Parameters.AddWithValue("@BatchName", batchName.Text);
                        dataCommand.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(startDate.Text));
                        dataCommand.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(endDate.Text));
                        dataCommand.Parameters.AddWithValue("@active", activeCB.Checked);
                        //dataCommand.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);
                        dataCommand.Parameters.AddWithValue("@CreatedDate", DateTime.Today);

                        added = dataCommand.ExecuteNonQuery(); //newly added code to generate add success pop up
                        dataConnection.Close();
                    }
                }
            }
            if (added > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('Class detail is successfully recorded.');", true);
                Reset();
            }
            
            
        }
        #endregion

        protected void startDateCalendar_SelectionChanged1(object sender, EventArgs e)
        {
            startDate.Text = startDateCalendar.SelectedDate.ToShortDateString();
            startDateCalendar.Visible = false;
            endDate.Text = startDateCalendar.SelectedDate.AddMonths(6).ToShortDateString();
        }

        protected void endDateCalendar_SelectionChanged2(object sender, EventArgs e)
        {
            endDate.Text = endDateCalendar.SelectedDate.ToShortDateString();
            endDateCalendar.Visible = false;
        }

        protected void startDateCalendar_Button_Click(object sender, ImageClickEventArgs e)
        {
            makeControlValidationFalse();
            if (endDateCalendar.Visible)
            {
                endDateCalendar.Visible = false;
            }
            if (startDateCalendar.Visible)
            {
                startDateCalendar.Visible = false;
            }
            else
            {
                startDateCalendar.Visible = true;
                startDateCalendar.VisibleDate = DateTime.Now;
            }
        }

        protected void endDateCalendar_Button_Click(object sender, ImageClickEventArgs e)
        {
            makeControlValidationFalse();
            if (startDateCalendar.Visible)
            {
                startDateCalendar.Visible = false;
            }
            if (endDateCalendar.Visible)
            {
                endDateCalendar.Visible = false;
            }
            else
            {
                endDateCalendar.Visible = true;
                endDateCalendar.VisibleDate = DateTime.Now;               
            }
        }
    }

}
