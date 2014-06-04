using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace DWIT_HRM_System
{
    public partial class EditTimeEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                recordDateV.Text = DateTime.Today.ToShortDateString();

                for (int i = 1; i < 13; i++)
                {
                    ddlcinhour.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddlcouthour.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                for (int j = 0; j < 60; j++)
                {
                    ddlcinminute.Items.Add(new ListItem(j.ToString("00"), j.ToString("00")));
                    ddlcoutminute.Items.Add(new ListItem(j.ToString("00"), j.ToString("00")));
                }
            }
            notFoundMsg.Visible = false;
            updateMsg.Visible = false;
        }

        public void Search()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT userID,userEmail,password,FirstName,LastName,active,role
                FROM users WHERE userEmail = @searchbox";

            cmd.Parameters.AddWithValue("@searchbox", searchBox.Text);

            try
            {
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                int check = 0;

                if (dr.Read())
                {
                    check = 1;
                    useridV.Text = dr["userID"].ToString();
                }

                dr.Close();

                if (check == 0)
                {
                    notFoundMsg.Visible = true;
                    panelWrapper.Visible = false;
                }
                else
                {
                    panelWrapper.Visible = true;

                    SqlCommand cmd_timesheet = new SqlCommand();
                    cmd_timesheet.Connection = con;
                    cmd_timesheet.CommandText = @"SELECT checkInTime,checkOutTime
                FROM timesheet WHERE UserID = @userid AND InOutDate = @recorddate";

                    cmd_timesheet.Parameters.AddWithValue("@userid", useridV.Text);
                    cmd_timesheet.Parameters.AddWithValue("@recorddate", recordDateV.Text);

                    SqlDataReader dr_timesheet = cmd_timesheet.ExecuteReader();

                    if (dr_timesheet.Read())
                    {
                        string textin = dr_timesheet["checkInTime"].ToString();
                        string textout = dr_timesheet["checkOutTime"].ToString();
                        char[] delimeterChars = { ' ', ':' };

                        string[] wordsin = textin.Split(delimeterChars);
                        string[] wordsout = textout.Split(delimeterChars);

                        ddlcinhour.SelectedValue = wordsin[1];
                        ddlcinminute.SelectedValue = wordsin[2];
                        ddlcinperiod.SelectedValue = wordsin[4];

                        ddlcouthour.SelectedValue = wordsout[1];
                        ddlcoutminute.SelectedValue = wordsout[2];
                        ddlcoutperiod.SelectedValue = wordsout[4];

                        checkInStatus.Visible = false;
                        checkOutStatus.Visible = false;
                    }
                    else
                    {

                        ddlcinhour.SelectedValue = "-1";
                        ddlcinminute.SelectedValue = "-1";
                        ddlcinperiod.SelectedValue = "-1";

                        ddlcouthour.SelectedValue = "-1";
                        ddlcoutminute.SelectedValue = "-1";
                        ddlcoutperiod.SelectedValue = "-1";

                        checkInStatus.Visible = true;
                        checkOutStatus.Visible = true;
                    }
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
        }

        protected void searchBox_TextChanged(object sender, EventArgs e)
        {
            recordDateV.Text = DateTime.Today.ToShortDateString();
            Search();
        }

        protected void searchButton_Click(object sender, ImageClickEventArgs e)
        {
            recordDateV.Text = DateTime.Today.ToShortDateString();
            Search();
        }

        protected void Calendar_Button_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }
        }

        protected void Calendar1_SelectionChanged1(object sender, EventArgs e)
        {
            recordDateV.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;

            String connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);


            SqlCommand cmd_timesheet = new SqlCommand();
            cmd_timesheet.Connection = con;
            cmd_timesheet.CommandText = @"SELECT checkInTime,checkOutTime
                FROM timesheet WHERE UserID = @userid AND InOutDate = @recorddate";

            cmd_timesheet.Parameters.AddWithValue("@userid", useridV.Text);
            cmd_timesheet.Parameters.AddWithValue("@recorddate", recordDateV.Text);

            try
            {
                con.Open();

                SqlDataReader dr_timesheet = cmd_timesheet.ExecuteReader();

                if (dr_timesheet.Read())
                {
                    string textin = dr_timesheet["checkInTime"].ToString();
                    string textout = dr_timesheet["checkOutTime"].ToString();
                    char[] delimeterChars = { ' ', ':' };

                    string[] wordsin = textin.Split(delimeterChars);
                    string[] wordsout = textout.Split(delimeterChars);

                    ddlcinhour.SelectedValue = wordsin[1];
                    ddlcinminute.SelectedValue = wordsin[2];
                    ddlcinperiod.SelectedValue = wordsin[4];

                    ddlcouthour.SelectedValue = wordsout[1];
                    ddlcoutminute.SelectedValue = wordsout[2];
                    ddlcoutperiod.SelectedValue = wordsout[4];

                    checkInStatus.Visible = false;
                    checkOutStatus.Visible = false;
                }
                else
                {

                    ddlcinhour.SelectedValue = "-1";
                    ddlcinminute.SelectedValue = "-1";
                    ddlcinperiod.SelectedValue = "-1";

                    ddlcouthour.SelectedValue = "-1";
                    ddlcoutminute.SelectedValue = "-1";
                    ddlcoutperiod.SelectedValue = "-1";

                    checkInStatus.Visible = true;
                    checkOutStatus.Visible = true;
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

        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            if (ddlcinhour.SelectedValue != "-1" || ddlcinminute.SelectedValue != "-1" || ddlcinperiod.SelectedValue != "-1")
            {
                if (ddlcinhour.SelectedValue == "-1" || ddlcinminute.SelectedValue == "-1" || ddlcinperiod.SelectedValue == "-1")
                {
                checkInStatus.Text = "Please Select Valid Time";
                checkInStatus.Visible = true;
                return;
                }
            }

            if (ddlcouthour.SelectedValue != "-1" || ddlcoutminute.SelectedValue != "-1" || ddlcoutperiod.SelectedValue != "-1")
            
            {
            if (ddlcouthour.SelectedValue == "-1" || ddlcoutminute.SelectedValue == "-1" || ddlcoutperiod.SelectedValue == "-1")
                {
                    checkOutStatus.Text = "Please Select Valid Time";
                    checkOutStatus.Visible = true;
                    return;
                }
            }
            
            String connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            tempcin.Text = (recordDateV.Text + " " + ddlcinhour.SelectedValue + ":" + ddlcinminute.SelectedValue + ":00 " + ddlcinperiod.SelectedValue);
            tempcout.Text = (recordDateV.Text + " " + ddlcouthour.SelectedValue + ":" + ddlcoutminute.SelectedValue + ":00 " + ddlcoutperiod.SelectedValue);

            SqlCommand cmd_TS_update = new SqlCommand();
            cmd_TS_update.Connection = con;
            cmd_TS_update.CommandText = @"UPDATE timesheet SET CheckInTime = @tempcin, CheckOutTime = @tempcout WHERE UserID = @userid AND InOutDate = @recorddate ";

            cmd_TS_update.Parameters.AddWithValue("@tempcin", tempcin.Text);
            cmd_TS_update.Parameters.AddWithValue("@tempcout", tempcout.Text);
            cmd_TS_update.Parameters.AddWithValue("@userid", useridV.Text);
            cmd_TS_update.Parameters.AddWithValue("@recorddate", recordDateV.Text);

            SqlCommand cmd_TS_insert = new SqlCommand();
            cmd_TS_insert.Connection = con;
            cmd_TS_insert.CommandText = @"INSERT INTO timesheet (UserID,CheckInTime,CheckOutTime,InOutDate) VALUES (@userid, @tempcin, @tempcout, @recorddate)  ";

            if (ddlcinhour.SelectedValue == "-1" || ddlcinminute.SelectedValue == "-1" || ddlcinperiod.SelectedValue == "-1")
            {
                cmd_TS_insert.Parameters.AddWithValue("@tempcin", System.DBNull.Value);
            }

            else
            {
                cmd_TS_insert.Parameters.AddWithValue("@tempcin", tempcin.Text);
            }

            if (ddlcouthour.SelectedValue == "-1" || ddlcoutminute.SelectedValue == "-1" || ddlcoutperiod.SelectedValue == "-1")
            {
                cmd_TS_insert.Parameters.AddWithValue("@tempcout", System.DBNull.Value);
            }

            else
            {
               cmd_TS_insert.Parameters.AddWithValue("@tempcout", tempcout.Text);
             }
            cmd_TS_insert.Parameters.AddWithValue("@userid", useridV.Text);
            
           
            cmd_TS_insert.Parameters.AddWithValue("@recorddate", recordDateV.Text);

            try
            {
                con.Open();

                if (checkInStatus.Visible && checkOutStatus.Visible)
                {
                    cmd_TS_insert.ExecuteNonQuery();
                    updateMsg.Text = "New Time Record is Inserted Successfully";
                }
                else
                {
                    cmd_TS_update.ExecuteNonQuery();
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

            checkInStatus.Visible = false;
            checkOutStatus.Visible = false;
            updateMsg.Visible = true;

        }
    }
}