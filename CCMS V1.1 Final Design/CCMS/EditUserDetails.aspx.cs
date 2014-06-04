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
    public partial class EditUserDetails : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    userEmailV.Text = dr["userEmail"].ToString();
                    firstNameV.Text = dr["FirstName"].ToString();
                    lastNameV.Text = dr["LastName"].ToString();
                    statusV.SelectedValue = dr["active"].ToString();
                    roleV.SelectedValue = dr["role"].ToString();
                    
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
                }

            }
            finally
            {
                con.Close();
            }
            
        }

        protected void searchBox_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        protected void searchButton_Click(object sender, ImageClickEventArgs e)
        {
            Search();
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE users SET FirstName = @firstnameV, LastName = @lastnameV, active = @statusV, role = @roleV WHERE userEmail = @searchbox";
            cmd.Parameters.AddWithValue("@firstnameV", firstNameV.Text);
            cmd.Parameters.AddWithValue("@lastnameV", lastNameV.Text);
            cmd.Parameters.AddWithValue("@statusV", statusV.SelectedValue);
            cmd.Parameters.AddWithValue("@roleV", roleV.SelectedValue);
            cmd.Parameters.AddWithValue("@searchbox", userEmailV.Text);

            try
            {
                con.Open();

                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Some exception occured");
            }
            finally
            {
                con.Close();
            }

            updateMsg.Visible = true;
        }
    }
}