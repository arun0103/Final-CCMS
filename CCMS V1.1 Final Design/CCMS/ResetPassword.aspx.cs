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
    public partial class ResetPassword : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlDataReader dr;
                try
                {
                    //Here we will check from the passed querystring that if the email id/username and generated unique code is same then the panel for resetting password will be visible otherwise not
                    cmd = new SqlCommand("select userEmail,UniqueCode from users where UniqueCode=@uCode and userEmail = @userEmail", con);
                    cmd.Parameters.AddWithValue("@uCode", Convert.ToString(Request.QueryString["uCode"]));
                    cmd.Parameters.AddWithValue("@userEmail", Convert.ToString(Request.QueryString["userEmail"]));

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        ResetPwdPanel.Visible = true;
                    }
                    else
                    {
                        ResetPwdPanel.Visible = false;
                        lblExpired.Text = "Reset password link has expired.It was for one time use only";
                        return;
                    }
                    dr.Close();
                    dr.Dispose();
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error Occured: " + ex.Message.ToString();
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                }
            }
        }
        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            try
            {   // Here we will update the new password and also set the unique code to null so that it can be used only for once.
                cmd = new SqlCommand("update Users set UniqueCode='',Password=@pwd,isPasswordUpdated=0 where UniqueCode=@uniqueCode and userEmail=@emailid", con);
                cmd.Parameters.AddWithValue("@uniqueCode", Convert.ToString(Request.QueryString["uCode"]));
                cmd.Parameters.AddWithValue("@emailid", Convert.ToString(Request.QueryString["userEmail"]));
                cmd.Parameters.AddWithValue("@pwd", txtNewPwd.Text.Trim());
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int value = cmd.ExecuteNonQuery();
                if (value > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('Password successfully updated');", true);
                    txtNewPwd.Text = string.Empty;
                    txtConfirmPwd.Text = string.Empty;
                    
                }
                Response.Redirect("Default.aspx");
                //lblStatus.Text = "Your password has been updated successfully.";
                
                
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error Occured : " + ex.Message.ToString();
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

    }
}