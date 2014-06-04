using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient; 
using System.Text;
using System.Configuration;
using System.Data;
using System.Drawing;


namespace CCMS
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString;
        SqlConnection conDatabase;
        SqlCommand cmd;
        int checkID;
               

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                checkID = Convert.ToInt32(Session["UserId"]);
                CreateConnection();
                cmd = new SqlCommand();
                cmd.CommandText = "Select FirstName + ' ' + LastName As FullName FROM Users where UserID=" + checkID;

                cmd.Connection = conDatabase;

                try
                {
                    OpenConnection();

                    LblWelcome.ForeColor = System.Drawing.Color.Black;
                    LblWelcome.Text = "<b>Welcome,</b> " + cmd.ExecuteScalar().ToString();

                    LblDate.Text = "<b>Today's Date:</b> " + DateTime.Now.ToString();
                    LblDate.ForeColor = System.Drawing.Color.Black;
                    if (checkID >= 1)
                    {
                        CheckIfUserCheckedIn(checkID);
                    }
                }
                finally
                {
                    CloseConnection();

                }
            }
                    
        }
        #endregion

        #region SQL Connection
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

        #endregion

        #region Check CheckIn of User

        public void CheckIfUserCheckedIn(int UserId)
        {

            CreateConnection();

            cmd = new SqlCommand();
            cmd.CommandText = "Select CheckInTime,CheckOutTime from TimeSheet  where UserID = " + UserId + "and InOutDate = '" + DateTime.Now.Date + "'";
            cmd.Connection = conDatabase;

            try
            {
                OpenConnection();

                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                DataTable resultTable = new DataTable();
                sqlAdapter.SelectCommand = cmd;
                sqlAdapter.Fill(resultTable);

                if (resultTable.Rows.Count > 0)
                {
                    DateTime userCheckIn = Convert.ToDateTime(resultTable.Rows[0][0].ToString());
                    DateTime userCheckOut = Convert.ToDateTime(resultTable.Rows[0][0].ToString());

                    if (userCheckIn != null)
                    {

                        foreach (DataRow row in resultTable.Rows)
                        {
                            object value = row["CheckOutTime"];
                            if (value == DBNull.Value)
                            {
                                btncheckin.Enabled = false;
                                btncheckin.BackColor = Color.Transparent;
                                btncheckin.ForeColor = Color.White;

                                btncheckout.Enabled = true;
                            }
                            else
                            {
                                btncheckin.Enabled = false;
                                btncheckin.BackColor = Color.Transparent;
                                btncheckin.ForeColor = Color.White;
                                btncheckout.Enabled = false;
                                btncheckout.ForeColor = Color.White ;
                                btncheckout.BackColor = Color.Transparent;

                            }

                        }

                    }

                    else
                    {

                        btncheckin.Enabled = true;
                        btncheckout.Enabled = false;
                    }
                }

            }
            catch
            {
                Console.WriteLine("some exception occured");

            }
            finally
            {
                CloseConnection();
            }

        }

        #endregion

        #region Check in CLick
        protected void checkIn_Click(object sender, EventArgs e)
        {
            CreateConnection();
            cmd = new SqlCommand();
            cmd.Connection = conDatabase;
            cmd.CommandText = "INSERT INTO timesheet(userID, CheckInTime,InOutDate) VALUES (@User,@CheckinTime,@InOutDate);";
            cmd.Parameters.AddWithValue("@User", Convert.ToInt32(Session["UserId"].ToString()));
            cmd.Parameters.AddWithValue("@CheckinTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@InOutDate", DateTime.Now.Date);

            try
            {
                OpenConnection();

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('Your Check-In time is successfully recorded.');", true);
                    btncheckin.Enabled = false;
                    btncheckout.Enabled = true;
                    btncheckin.BackColor = Color.Transparent;
                    btncheckin.ForeColor= Color.White;
  
                }

            }
            catch
            {
                Console.WriteLine("some exception occured");
            }
            finally
            {
                CloseConnection();
            }

        }
        #endregion

        #region Check out Click
        protected void checkOut_Click(object sender, EventArgs e)
        {

            CreateConnection();
            cmd = new SqlCommand();
            cmd.Connection = conDatabase;
            cmd.CommandText = "Update TimeSheet set CheckoutTime =  @CheckoutTime where UserID = @User ";
            cmd.Parameters.AddWithValue("@User", Convert.ToInt32(Session["UserId"].ToString()));
            cmd.Parameters.AddWithValue("@CheckoutTime", DateTime.Now);

            try
            {
                OpenConnection();
                DataTable resultTable = new DataTable();
                
                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('Your Check-0ut time is successfully recorded.');", true);
                    btncheckout.Enabled = false;
                    btncheckout.BackColor = Color.Transparent;
                    btncheckout.ForeColor = Color.White;
                }
            }
            catch
            {
                Console.WriteLine("some exception occured");
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        protected void TimerTime_Tick(object sender, EventArgs e)
        {

        }
    }
}
     