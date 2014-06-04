using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace CCMS
{
    public partial class Students : System.Web.UI.Page
    {
        static DataTable dt = new DataTable();
        static string FileName;
        static string Extension;
        static string FolderPath;
        static string FilePath;


        protected void Page_Load(object sender, EventArgs e)
        {
            successMsg.Visible = false;
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (FileUploadStudent.HasFile)
            {
                FileName = Path.GetFileName(FileUploadStudent.PostedFile.FileName);
                Extension = Path.GetExtension(FileUploadStudent.PostedFile.FileName);
                FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                FilePath = Server.MapPath(FolderPath + FileName);

                FileUploadStudent.SaveAs(FilePath);
                FileInfoPanel.Visible = true;
                lblFileNameValue.Text = Path.GetFileName(FilePath);

                String connectionString = getExcelConnectionString(Extension, RadioButtonListHDR.SelectedItem.Text);

                OleDbConnection conExcel = new OleDbConnection(connectionString);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();

                cmdExcel.Connection = conExcel;
                conExcel.Open();

                //Bind the Sheets to DropDownList
                ddlSheets.Items.Clear();
                ddlSheets.DataSource = conExcel
                         .GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                ddlSheets.DataTextField = "TABLE_NAME";
                ddlSheets.DataValueField = "TABLE_NAME";
                ddlSheets.DataBind();

                conExcel.Close();
            }
        }

        private void ImportToGrid(string FilePath, string Extension)
        {
            String connectionString = getExcelConnectionString(Extension, RadioButtonListHDR.SelectedItem.Text);
            OleDbConnection conExcel = new OleDbConnection(connectionString);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            cmdExcel.Connection = conExcel;

            String SheetName = ddlSheets.SelectedItem.Text;

            conExcel.Open();
            cmdExcel.CommandText = "Select * from [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            conExcel.Close();

            //Bind data to GridView

            GridViewStudents.Caption = Path.GetFileName(FilePath);
            GridViewStudents.DataSource = dt;
            tempGridView.DataSource = dt; //invisible temporary gridview to load all data in excel without paging
            GridViewStudents.DataBind();
            tempGridView.DataBind();
        }

        protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridViewStudents.DataSource = dt; 
            GridViewStudents.PageIndex = e.NewPageIndex;
            GridViewStudents.DataBind();  
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            ImportToGrid(FilePath, Extension);
            btnSaveStudents.Visible = true;
        }

        public String getExcelConnectionString(String Extensoin, String isHDR)
        {
            string connString = null;
            switch (Extension)
            {
                case ".xls": //Excel 97 - 03
                    connString = ConfigurationManager.ConnectionStrings["Excel03ConnectionString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 2003 or later
                    connString = ConfigurationManager.ConnectionStrings["Excel07ConnectionString"].ConnectionString;
                    break;
            }

            connString = String.Format(connString, FilePath, isHDR);
            return connString;
        }


        protected void SaveStudents_Click(object sender, EventArgs e)
        {

            DataTable tempStudentTable = new DataTable();
            tempStudentTable.Columns.Add("rollNo", typeof(int));
            tempStudentTable.Columns.Add("firstName", typeof(string));
            tempStudentTable.Columns.Add("lastName", typeof(string));
            tempStudentTable.Columns.Add("email", typeof(string));

            DataRow dr = null;

            foreach (GridViewRow gr in tempGridView.Rows)
            {
                dr = tempStudentTable.NewRow();

                dr["rollNo"] = Convert.ToInt32(gr.Cells[0].Text);
                dr["firstName"] = gr.Cells[1].Text;
                dr["lastName"] = gr.Cells[2].Text;
                dr["email"] = gr.Cells[3].Text;

                tempStudentTable.Rows.Add(dr);
            }

            DataTable StudentTable = tempStudentTable.GetChanges(DataRowState.Added);
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand insertCommand = new SqlCommand("spInsertStudents", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@StudentList", StudentTable);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "dbo.TableTypeStudent";
            try
            {
                connection.Open();
                int affectedRows = insertCommand.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    successMsg.Text = affectedRows + " Records were inserted Successfully";
                    successMsg.Visible = true;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
    }

}