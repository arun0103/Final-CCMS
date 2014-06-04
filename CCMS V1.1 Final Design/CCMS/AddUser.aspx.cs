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
using System.Net.Mail;
using System.Net;
using System.Web.Services.Description;
using CCMS;
using System.Security.Cryptography;


namespace MyCCMS
{
    public partial class addUser : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txtEmail.Text = String.Empty;
            txtFirstname.Text = String.Empty;
            txtLastname.Text = String.Empty;
            DropDownRole.SelectedIndex = 0;
            chkActive.Checked = false;
            txtContact.Text = String.Empty;
            AU_email.Enabled = false;
            AU_firstname.Enabled = false;
            AU_lastname.Enabled = false;
            AU_role.Enabled = false;
            AU_contact.Enabled = false;
            updateMsg.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AU_email.Enabled = true;
            AU_firstname.Enabled = true;
            AU_lastname.Enabled = true;
            AU_role.Enabled = true;
            AU_contact.Enabled = true;
            Page.Validate();

            if (Page.IsValid)
            {

                if (CCMSBusinessLayer.isExistingUser(txtEmail.Text))
                {
                    updateMsg.Visible = true;
                }

                else
                {
                    Save();
                }
                Reset();

            }

        }

        public int findUserID(string email)
        {

            return CCMSBusinessLayer.FindUserIdfromEmail(email);
        }


        private void Save()
        {
            User newUser = new User
            {
                Active = chkActive.Checked,
                Contact = txtContact.Text,
                Email = txtEmail.Text,
                FirstName = txtFirstname.Text,
                LastName = txtLastname.Text,
                Role = DropDownRole.SelectedItem.Text
            };

            int affectedRows = CCMSBusinessLayer.AddUser(newUser);
            if (affectedRows > 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('User Successfully Saved');", true);
                Reset();
            }
        }
    }
}







