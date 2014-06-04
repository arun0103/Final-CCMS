using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;


namespace CCMS
{
    public partial class FirstLoginPasswordChange : System.Web.UI.Page
    {
        string connectionString;
        SqlCommand cmd;
        SqlConnection conDatabase;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
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
        



        public void updatePassword(Object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(originalPassword.Text) && !string.IsNullOrEmpty(newPassword.Text) && !string.IsNullOrEmpty(confirmedPassword.Text))
            {
                
                if (originalPassword.Text != Session["password"].ToString())
                {
                    
                    lblMessage.Visible = true;
                    lblMessage.Text = "Original Password entry incorrect.";
                }

                else if (!Regex.Equals(confirmedPassword.Text,newPassword.Text))
                {
                    
                    lblMessage.Visible = true;
                    lblMessage.Text = "Password doesnot match.";
                }
                
                else
                {
                    var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var random = new Random();
                    var result = new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());

                    CCMSBusinessLayer md = new CCMSBusinessLayer();
                    string hash = "";
                    string source = confirmedPassword.Text + result;
                    using (MD5 md5Hash = MD5.Create())
                    {
                        hash = md.getMd5Hash(md5Hash, source);
                    }

                    string combinedPassword = hash + ":" + result;

                    CreateConnection();
                    cmd = new SqlCommand();
                    cmd.Connection = conDatabase;
                    cmd.CommandText = "Update users set password =  @password,isPasswordUpdated = 'true' where UserID = @User ";
                    cmd.Parameters.AddWithValue("@password", combinedPassword);
                    cmd.Parameters.AddWithValue("@User", Convert.ToInt32(Session["UserId"].ToString()));

                    try
                    {
                        OpenConnection();
                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            ContinueAfterPasswordChange();
                            Session["password"] = confirmedPassword.Text;
                            Session["isPasswordUpdated"]= "true";

                        }

                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (conDatabase != null)
                        {
                            conDatabase.Close();
                        }
                    }
            
                }
            

            }
            

        }
        
        public void ContinueAfterPasswordChange()
        {
            originalPassword.Enabled = false;
            newPassword.Enabled = false;
            confirmedPassword.Enabled = false;

            OriginalPasswordValidator.Enabled = false;
            newPasswordValidator.Enabled = false;
            regExpressionValidator.Enabled = false;
           
            confirmPasswordValidator.Enabled = false;

            showPanel.Visible = true;

            
        }

        /* redirect user to respective page after password change*/ 
        public void passwordChange(Object sender, EventArgs e) 
        {
            //making default panel of Faculty of Site.Master visible
            System.Web.UI.WebControls.Panel p = (System.Web.UI.WebControls.Panel)Master.FindControl("menubar");
            p.Visible = true;

            //making default ContentPlaceholder of Site.Master  visible
            ContentPlaceHolder cpuser = (ContentPlaceHolder)Master.FindControl("menuuser");
            cpuser.Visible = true;

            //redirecting user to Timeentry page
            Response.Redirect("TimeEntry.aspx");   
        }
    }
}