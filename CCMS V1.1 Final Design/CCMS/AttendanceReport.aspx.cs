using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace CCMS
{
    public partial class AttendanceReport : System.Web.UI.Page
    {
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFacultyDropdown();
                BindStudentDropdown();                
            }
            
        }

        #region BindFacultyDropdown
        private void BindFacultyDropdown()
        {
            DataService objDataService = new DataService();
           
            objDataService.BindFaculty(facultyV);
            facultyV.Items.Insert(0,new ListItem("All Teachers","0"));           
        }
        #endregion

        #region BindStudentDropdown
        private void BindStudentDropdown()
        {
            DataService objDataService = new DataService();
            objDataService.BindStudent(studentV);
            studentV.Items.Insert(0,new ListItem("All Students","0"));
        }
        #endregion

        private void loadGrid()
        {
            CCMSBusinessLayer cb = new CCMSBusinessLayer();
            if (Convert.ToInt32(facultyV.Text) == 0 && Convert.ToInt32(studentV.Text) != 0  )
            {
                DataTable allTeacherSingleStudent = new DataTable();    
                allTeacherSingleStudent = cb.displayAllStudentsRecord(Convert.ToInt32(facultyV.Text),Convert.ToInt32(studentV.Text),startDate.Text,endDate.Text);
                reportGrid.Visible = true;
                reportGrid.DataSource = allTeacherSingleStudent;
                reportGrid.DataBind();
                checkDateFilledOrNot(Convert.ToInt32(facultyV.Text), Convert.ToInt32(studentV.Text));
            }

            else if (Convert.ToInt32(facultyV.Text) != 0 && Convert.ToInt32(studentV.Text) != 0)
            {
                DataTable studentClassMiss = new DataTable();
                studentClassMiss = cb.GetStudentClassMiss(Convert.ToInt32(facultyV.Text), Convert.ToInt32(studentV.Text), startDate.Text, endDate.Text);
                reportGrid.Visible = true;
                reportGrid.DataSource = studentClassMiss;
                reportGrid.DataBind();
                checkDateFilledOrNot(Convert.ToInt32(facultyV.Text), Convert.ToInt32(studentV.Text));
                
            }

            else if (Convert.ToInt32(facultyV.Text) != 0 && Convert.ToInt32(studentV.Text) == 0) 
            {
                
                DataTable singleTeacherAllStudent = new DataTable();
                singleTeacherAllStudent = cb.displayOneTeacherAllStudentsRecord(Convert.ToInt32(facultyV.Text), Convert.ToInt32(studentV.Text), startDate.Text, endDate.Text);
                reportGrid.Visible = true;
                reportGrid.DataSource = singleTeacherAllStudent;
                reportGrid.DataBind();
                checkDateFilledOrNot(Convert.ToInt32(facultyV.Text), Convert.ToInt32(studentV.Text));
            }

            else if (Convert.ToInt32(facultyV.Text) == 0 && Convert.ToInt32(studentV.Text) == 0)
            {

                DataTable allTeacherAllStudent = new DataTable();
                allTeacherAllStudent = cb.displayallTeacherAllStudentsRecord(Convert.ToInt32(facultyV.Text), Convert.ToInt32(studentV.Text), startDate.Text, endDate.Text);
                reportGrid.Visible = true;
                reportGrid.DataSource = allTeacherAllStudent;
                reportGrid.DataBind();
                checkDateFilledOrNot(Convert.ToInt32(facultyV.Text), Convert.ToInt32(studentV.Text));

            }
                

        }

        private void checkDateFilledOrNot(int fid,int stdid)
        {
            if (startDate.Text != "" && endDate.Text != "")
            {
                if (fid == 0 && stdid == 0)
                {
                    //bindAttendees();
                    //drpAttendees.Visible = true;

                }
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
            }
        }

        private void bindAttendees()
        {
            int attendancePercent = 90;
            DataService objDataService = new DataService();

            objDataService.bindAttendees(attendancePercent);
            facultyV.Items.Insert(0, new ListItem("All Teachers", "0"));
        }

        protected void showReport_Click(object sender, EventArgs e)
        {
            if (startDate.Text == "" || endDate.Text == "")
            {
                AR_startDate.Visible = true;
                AR_endDate.Visible = true;
                Page.Validate();
            }
            if (startDate.Text != "" && endDate.Text != "")
            {
                loadGrid();
            }
        }

        protected void makeControlValidationFalse()
        {
            AR_startDate.Visible = false;
            AR_endDate.Visible = false;
        }
        protected void clearBtn_Click(object sender, EventArgs e)
        {
            facultyV.SelectedIndex = 0;
            studentV.SelectedIndex = 0;
            startDate.Text = "";
            endDate.Text = "";
            lblText.Visible = false;
            reportGrid.Visible = false;
            btnExport.Visible = false;
            startDateCalendar.Visible = false;
            endDateCalendar.Visible = false;
            makeControlValidationFalse();
        }
        protected void startDateCalendar_SelectionChanged1(object sender, EventArgs e)
        {
            
            startDate.Text = startDateCalendar.SelectedDate.ToShortDateString();
            startDateCalendar.Visible = false;
        }

        protected void endDateCalendar_SelectionChanged2(object sender, EventArgs e)
        {
            endDate.Text = endDateCalendar.SelectedDate.ToShortDateString();            
            endDateCalendar.VisibleDate = DateTime.Now;
            endDateCalendar.Visible = false;
        }

        protected void startDateCalendar_Button_Click(object sender, ImageClickEventArgs e)
        {
            makeControlValidationFalse();
            if (endDateCalendar.Visible)
            {
                endDateCalendar.Visible = false;
            }
            if (startDateCalendar.Visible)
            {
                startDateCalendar.Visible = false;
            }
            else
            {
                startDateCalendar.Visible = true;
                startDateCalendar.VisibleDate = DateTime.Now;
            }
        }

        protected void endDateCalendar_Button_Click(object sender, ImageClickEventArgs e)
        {
            makeControlValidationFalse();
            if (startDateCalendar.Visible)
            {
                startDateCalendar.Visible = false;
            }
            if (endDateCalendar.Visible)
            {
                endDateCalendar.Visible = false;
            }
            else
            {
                endDateCalendar.Visible = true;
                endDateCalendar.VisibleDate = DateTime.Now;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();

            Response.AddHeader("content-disposition", "attachment;filename=Report.xls");

            Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a table to contain the grid
                    Table table = new Table();

                    //  include the gridline settings
                    table.GridLines = reportGrid.GridLines;

                    //  add the header row to the table
                    if (reportGrid.HeaderRow != null)
                    {
                        AttendanceReport.PrepareControlForExport(reportGrid.HeaderRow);
                        table.Rows.Add(reportGrid.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in reportGrid.Rows)
                    {
                        AttendanceReport.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (reportGrid.FooterRow != null)
                    {
                        AttendanceReport.PrepareControlForExport(reportGrid.FooterRow);
                        table.Rows.Add(reportGrid.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }

        }

        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    AttendanceReport.PrepareControlForExport(current);
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            reportGrid.PageIndex = e.NewPageIndex;
            loadGrid();
            
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        loadGrid();
        DataTable dataTable = reportGrid.DataSource as DataTable;

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

            reportGrid.DataSource = dataView;
            reportGrid.DataBind();


        }

        

    }
        
        
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

        protected void reportGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    
}

