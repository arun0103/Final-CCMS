using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CCMS
{
    public partial class FacultyPage : System.Web.UI.Page
    {
        string connectionString;
        SqlConnection conDatabase;
        SqlCommand cmd;
        int checkID;
        List<Tuple<string, string, string, string>> displaySubList = new List<Tuple<string, string, string, string>>();
        protected void Page_Load(object sender, EventArgs e)
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
                LblWelcome.Text = "Logged in as : " + cmd.ExecuteScalar().ToString();

                LblDate.Text = "Today's Date : " + DateTime.Now.ToShortDateString();
                LblDate.ForeColor = System.Drawing.Color.Black;

                recordDateV.Text = DateTime.Now.ToShortDateString();

                if (checkID >= 1)
                {
                    displaySubjectLinkFaculty(checkID);

                }
            }
            finally
            {
                CloseConnection();
            }
        }


        private void displaySubjectLinkFaculty(int checkID)
        {
            string sectionName = "";
            string batchName = "";
            string subjectName = "";
            string subjectCode = "";
            string Fid = "";
            string routineId = "";
            //List<Tuple<string, string, string,string>> displaySubList = new List<Tuple<string, string, string,string>>();
            
            CreateConnection();
            cmd = new SqlCommand();
            cmd.CommandText = "Select R.routineId,R.SectionName,F.UserId,B.ClassName,Sy.Subject_Name,Subject_Code from faculty F inner join routine R on F.UserId=R.Fid inner join class B on R.ClassId = B.ClassId inner join Syllabus Sy on Sy.Subject_Code=R.SubjectId where F.UserId=" + checkID;
            cmd.Connection = conDatabase;

            try
            {
                OpenConnection();
                SqlDataReader sdr = cmd.ExecuteReader();
                
                while (sdr.Read())
                {
                    
                    sectionName = sdr["SectionName"].ToString();
                    subjectCode = sdr["Subject_Code"].ToString();
                    batchName = sdr["ClassName"].ToString();
                    subjectName = sdr["Subject_Name"].ToString();
                    routineId = sdr["routineId"].ToString();
                    Fid = sdr["UserId"].ToString();
                    Session["UserId"] = Fid;
                    Session["Subject_Code"] = subjectCode;
                    //Session["routineId"] = routineId;
                    displaySubList.Add(Tuple.Create(batchName, sectionName, subjectName,routineId));
                
                }

                
                    for(int i=0;i<displaySubList.Count;i++)
                    {
                       
                        LinkButton lb = new LinkButton();
                        //literal1.Text = string.Format(literal1.Text, routineId);
                        
                        //string s = ((HiddenField)literal1.FindControl("someid")).Value;
                        ValueHiddenField.Visible = false;
                        ValueHiddenField.Value = displaySubList[i].Item4;
                        string s = ValueHiddenField.Value;
                        lb.Text = displaySubList[i].Item1 + "-" + displaySubList[i].Item2 + "-" + displaySubList[i].Item3;// + literal1.Text;

                        lb.ID = Convert.ToString(i); // LinkButton ID’s
                        lb.CommandArgument = Convert.ToString(i); //LinkButton CommandArgument
                        lb.CommandName = Convert.ToString(i); //LinkButton CommanName
                        lb.Command += new CommandEventHandler(lb_Command);//Create Handler for it.
                        PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
                        PlaceHolder1.Controls.Add(new HtmlGenericControl("br"));
                        
                        
                    }
                {
                    
                }

            }
            finally
            {
                CloseConnection();
            }


        }

        void lb_Command(object sender, CommandEventArgs e)
        {
            int i, j = 0;
            for (i = 0; i < displaySubList.Count; i++) 
            {
                LinkButton lnk = sender as LinkButton;
                lnk.ID = e.CommandName;
                
                int k = i;
                if (Equals(lnk.ID, i.ToString()))
                {
                    string textualContent = ((LinkButton)PlaceHolder1.Controls[j]).Text;
                    string[] parts = textualContent.Split('-');
                    Session["Subject_Name"] = parts[2];
                    Session["SectionName"] = parts[1];
                    Session["routineId"] = displaySubList[i].Item4;
                    
                    sublink(sender, e);
                    
                }
                j+=2;
/*
                if (Equals(lnk.ID, "1"))
                {
                    string textualContent = ((LinkButton)PlaceHolder1.Controls[2]).Text;
                    string[] parts = textualContent.Split('-');
                    Session["Subject_Name"] = parts[2];
                    Session["SectionName"] = parts[1];
                    sublink(sender, e);

                }

                if (Equals(lnk.ID, i.ToString()))
                {
                    string textualContent = ((LinkButton)PlaceHolder1.Controls[4]).Text;
                    string[] parts = textualContent.Split('-');
                    Session["Subject_Name"] = parts[2];
                    Session["SectionName"] = parts[1];
                    sublink(sender, e);

                }

                if (Equals(lnk.ID, i.ToString()))
                {
                    string textualContent = ((LinkButton)PlaceHolder1.Controls[6]).Text;
                    string[] parts = textualContent.Split('-');
                    Session["Subject_Name"] = parts[2];
                    Session["SectionName"] = parts[1];
                    sublink(sender, e);

                }*/
            

            
            }
        }

       

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

        protected void Faculty_Calendar_Button_Click(object sender, ImageClickEventArgs e)
        {

            if (calendar.Visible)
            {
                calendar.Visible = false;
            }
            else
            {
                calendar.Visible = true;
            }
        }

        protected void Faculty_Calendar_SelectionChanged(object sender, EventArgs e)
        {
            recordDateV.Text = calendar.SelectedDate.ToShortDateString();
            calendar.Visible = false;
        }

        protected void sublink(object sender, EventArgs e)
        {
            Response.Redirect("AttendanceEntry.aspx");
        }

    }
}

