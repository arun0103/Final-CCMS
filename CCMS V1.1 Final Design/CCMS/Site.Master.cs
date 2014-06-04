using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCMS
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
    
            if (Session["UserId"] == null)
            {
                btnLogout.Visible = false;
                Label emptyLabel = new Label();
                emptyLabel.Text = string.Empty;
                menu.Visible = false;
                menuuser.Visible = false;
                
            } 
            

            if (Session["Role"] != null && String.Compare(Session["Role"].ToString(),"Admin",true)==0)
            {
                menuuser.Visible = false;
                menu.Visible = true;
                menubar.Visible = true;
                if (String.Compare(Session["isPasswordUpdated"].ToString(), "False", true) == 0)
                {
                    lnkAddUser.Visible = false;
                    lnkEditUser.Visible = false;
                    lnkTimeEntry.Visible = false;
                    lnkEditTimeSheet.Visible = false;
                    lnkAddBatch.Visible = false;
                    lnkAddFacultyDetail.Visible = false;
                    lnkAddRoutine.Visible = false;
                    lnkViewFaculty.Visible = false;
                    lnkViewRoutine.Visible = false;
                    lnkAttendanceReport.Visible = false;

                }

                else
                {
                    lnkAddUser.Visible = true;
                    lnkAddUser.Text = "Add User";
                    lnkAddUser.PostBackUrl = "~/AddUser.aspx";

                    lnkEditUser.Visible = true;
                    lnkEditUser.Text = "Edit User Details";
                    lnkEditUser.PostBackUrl = "~/EditUserDetails.aspx";

                    lnkTimeEntry.Text = "Time Entry";
                    lnkTimeEntry.Visible = true;
                    lnkTimeEntry.PostBackUrl = "~/TimeEntry.aspx";

                    lnkEditTimeSheet.Text = "Edit Time Entry";
                    lnkEditTimeSheet.Visible = true;
                    lnkEditTimeSheet.PostBackUrl = "~/EditTimeEntry.aspx";

                    lnkAddBatch.Visible = true;
                    lnkAddBatch.Text = "Add Class";
                    lnkAddBatch.PostBackUrl = "~/AddNewBatch.aspx";

                    lnkAddFacultyDetail.Visible = true;
                    lnkAddFacultyDetail.Text = "Add Faculty Detail";
                    lnkAddFacultyDetail.PostBackUrl = "~/AddFacultyDetail.aspx";

                    lnkAddRoutine.Visible = true;
                    lnkAddRoutine.Text = "Add/View Routine";
                    lnkAddRoutine.PostBackUrl = "~/AddRoutine.aspx";

                    lnkViewFaculty.Visible = true;
                    lnkViewFaculty.Text = "View Faculty";
                    lnkViewFaculty.PostBackUrl = "~/ViewFaculty.aspx";

                    //lnkViewRoutine.Visible = true;
                    //lnkViewRoutine.Text = "View Routine";
                    //lnkViewRoutine.PostBackUrl = "~/viewRoutine.aspx";

                    lnkAttendanceReport.Visible = true;
                    lnkAttendanceReport.Text = "Report";
                    lnkAttendanceReport.PostBackUrl = "~/AttendanceReport.aspx";

                    lnkAddStudent.Visible = true;
                    lnkAddStudent.Text = "Add Students";
                    lnkAddStudent.PostBackUrl = "~/Students.aspx";

                    lnkEditStudent.Visible = true;
                    lnkEditStudent.Text = "Edit Students";
                    lnkEditStudent.PostBackUrl = "~/EditStudents.aspx";

                }
            }
            else if (Session["Role"] != null  && String.Compare(Session["Role"].ToString() ,"Faculty",true)==0)
            {
                menu.Visible = false;
                if (String.Compare(Session["isPasswordUpdated"].ToString(), "True", true) == 0)
                {
                    menubaruser.Visible = true;
                    menuuser.Visible = true;
                    lnkTimeEntryUser.Visible = true;
                    lnkTimeEntryUser.Text = "Time Entry";
                    lnkTimeEntryUser.PostBackUrl = "~/TimeEntry.aspx";
                    lnkFacultyPage.Visible = true;
                    lnkFacultyPage.Text = "Faculty";
                    lnkFacultyPage.PostBackUrl = "~/FacultyPage.aspx";

                }

                else
                {
                    lnkTimeEntry.Visible = false;
                    lnkFacultyPage.Visible = false;
                } 
               

            }
            else if (Session["Role"] != null && String.Compare(Session["Role"].ToString(), "User", true) == 0)
            {
                menu.Visible = false;
                if (String.Compare(Session["isPasswordUpdated"].ToString(), "True", true) == 0)
                {
                    menubaruser.Visible = true;
                    menuuser.Visible = true;
                    lnkTimeEntryUser.Visible = true;
                    lnkTimeEntryUser.Text = "Time Entry";
                    lnkTimeEntryUser.PostBackUrl = "~/TimeEntry.aspx";


                }

                else
                {
                    lnkTimeEntry.Visible = false;

                }


            }
            
            
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}