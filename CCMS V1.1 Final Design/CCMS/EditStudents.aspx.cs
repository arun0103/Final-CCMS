using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCMS
{
    public partial class EditStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindClassDropDown(ddlFrom);
                BindClassDropDown(ddlTo);
            }
        }

        private void BindClassDropDown(DropDownList ddl)
        {
            ddl.DataSource = CCMSBusinessLayer.GetClass();
            ddl.DataValueField = "classid";
            ddl.DataTextField = "Class";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Class", "-1"));
        }

        protected void btnUpgrade_Click(object sender, EventArgs e)
        {
            int affectedRows = 0;
            affectedRows = CCMSBusinessLayer.UpgradeStudentClass(Convert.ToInt32(ddlFrom.SelectedValue), Convert.ToInt32(ddlTo.SelectedValue));
            lblsuccssMsg.Text = affectedRows + " Student records were upgraded Sussfully";
            lblsuccssMsg.Visible = true;
        }

        protected void ddlFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexFrom = ddlFrom.SelectedIndex;
            if (indexFrom < ddlFrom.Items.Count-2)
            {
                ddlTo.SelectedIndex = indexFrom + 2;
            }
        }
    }
}