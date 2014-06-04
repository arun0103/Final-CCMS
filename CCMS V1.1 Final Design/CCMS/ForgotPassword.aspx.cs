using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCMS
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void checkEmail(object sender , EventArgs e) 
        {
        string uniqueCode = string.Empty;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            // get the records matching the supplied username or email id.         
            cmd = new SqlCommand("select * from Users where userEmail COLLATE Latin1_general_CS_AS=@emailId", con);

            //cmd.Parameters.AddWithValue("@username", Convert.ToString(txtUserName.Text.Trim()));
            cmd.Parameters.AddWithValue("@emailId", Convert.ToString(txtEmail.Text.Trim()));        
            dr = cmd.ExecuteReader();
            cmd.Dispose();
            if (dr.HasRows)
            {
                dr.Read();
                //generate unique code
                uniqueCode = Convert.ToString(System.Guid.NewGuid());
                //Updating an unique random code in then UniquCode field of the database table
                cmd = new SqlCommand("update Users set UniqueCode=@uniqueCode where userEmail=@emailid", con);
                cmd.Parameters.AddWithValue("@uniqueCode", uniqueCode);
                //cmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("@emailid", txtEmail.Text.Trim());
               
                StringBuilder strBody = new StringBuilder();
                //Passing emailid,username and generated unique code via querystring. For testing pass your localhost number and while making online pass your domain name instead of localhost path.
                strBody.Append("<a href=http://localhost:50574/ResetPassword.aspx?userEmail=" + txtEmail.Text + "&uCode=" + uniqueCode + ">Click here to change your password</a>");
               // sbody.Append("&uCode=" + uniqueCode + "&uName=" + txtUserName.Text + ">Click here to change your password</a>");

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("cccmsdwit@gmail.com", dr["userEmail"].ToString(), "Reset Your Password", strBody.ToString());
                //pasing the Gmail credentials to send the email

                System.Net.NetworkCredential mailAuthenticaion = new System.Net.NetworkCredential("ccmsdwit@gmail.com", "deerwalk");
              
                System.Net.Mail.SmtpClient mailclient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                mailclient.EnableSsl = true;
                mailclient.UseDefaultCredentials = false;
                mailclient.Credentials = mailAuthenticaion;            
                mail.IsBodyHtml = true;
                mailclient.Send(mail);
                dr.Close();
                dr.Dispose();
                cmd.ExecuteReader();
                cmd.Dispose();             
                con.Close();
                lblText.Text = "Reset password link has been sent to your email address";
                txtEmail.Text = string.Empty;
                //txtUserName.Text = string.Empty;           
            }
            else
            {
                lblText.Text = "Please enter valid email address or username";
                txtEmail.Text = string.Empty;
                //txtUserName.Text = string.Empty;
                con.Close();
                return;
            }
        }
        catch (Exception ex)
        {
            lblText.Text = "Error Occured: " + ex.Message.ToString();
        }
        finally
        {             
            cmd.Dispose();           
        }
    }
        
        }
    }
