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
    public partial class AddFacultyDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindEmailDropdown();
            }
        }
        public int findUserID(string email)
        {
            return CCMSBusinessLayer.FindUserIdfromEmail(email);
        }

        public void BindEmailDropdown()
        {
            string command = "Select UserID, UserEmail from Users where role = 'Faculty' order by UserEmail";
            DataTable EmailTable = new DataTable();


            EmailTable = DataService.GetDataWithoutParameter(command);
            EmailList.DataValueField = "UserID";
            EmailList.DataTextField = "UserEmail";
            EmailList.DataSource = EmailTable;

            EmailList.DataBind();
            EmailList.Items.Insert(0, new ListItem("--- Select Email ---", "0"));
        }

        public void EmailIndexChanged(Object sender, EventArgs e)
        {
            String email = EmailList.SelectedValue;
            String connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT FirstName, LastName, contact FROM  Users WHERE UserEmail= @email";
            cmd.Parameters.AddWithValue("@email", EmailList.SelectedItem.ToString());
           
            try
            {

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    firstName.Text = Convert.ToString(dr["FirstName"]);
                    lastName.Text = Convert.ToString(dr["LastName"]);
                    contact.Text = Convert.ToString(dr["contact"]);
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

        }


        #region Check in Click
        protected void addBtn_Click(object sender, EventArgs e)
        {

            NumberValidation.Enabled = true;
            Page.Validate();
            if (Page.IsValid)
            {
                

                if (firstName.Text != "" && lastName.Text != "" && EmailList.SelectedIndex != 0)
                {

                    Faculty faculty = new Faculty
                    {
                        Active = activeCB.Checked,
                        Contact = contact.Text,
                        //Email = txtEmail.Text,
                        Email = EmailList.SelectedItem.Text,
                        LastName = lastName.Text,
                        FirstName = firstName.Text,
                        UserId = findUserID(EmailList.SelectedValue)
                    };

                    int added = CCMSBusinessLayer.AddFaculty(faculty);

                    if (added > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('Faculty detail is successfully recorded.');", true);
                        //string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
                        //string sqlQuery = "select * from faculty";
                        //using (SqlConnection con = new SqlConnection(connectionString))
                        //{
                        //    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                        //    con.Open();
                        //    SqlDataReader rdr = cmd.ExecuteReader();
                        //    FacultyGridView.DataSource = rdr;
                        //    FacultyGridView.DataBind();
                        //}
                        //ContentPlaceHolder mcon = new ContentPlaceHolder();
                        //mcon = (ContentPlaceHolder)Master.FindControl("pageContent2");
                        //mcon.Visible = true;
                        Reset();
                    }
                }
            }
        }
        #endregion

        private void Reset()
        {
            makeControlValidationFalse();
            firstName.Text = String.Empty;
            lastName.Text = String.Empty;
            EmailList.SelectedIndex = 0;
            //txtEmail.Text = String.Empty;
            contact.Text = String.Empty;
            activeCB.Checked = false;
            
            updateMsg.Visible = false;

        }

        protected void clearBtn_Click1(object sender, EventArgs e)
        {
            NumberValidation.Enabled = false;
            NumberValidation.Visible = false;
            Reset();
        }

        protected void makeControlValidationFalse()
        {
            emailValidator.Enabled = false;
            
        }
    }
}

