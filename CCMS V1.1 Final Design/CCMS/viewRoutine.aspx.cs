using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCMS
{
    public partial class viewRoutine : System.Web.UI.Page
    {
        string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
                string sqlQuery = "select routine.routineId, faculty.FirstName + ' ' + faculty.LastName as FacultyName from routine inner join faculty on routine.Fid = faculty.FId";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //DataTable fullTable = new DataTable();
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                  
                    //fullTable.Columns.Add(dcol1);
                    //Persist the table in the Session object.
                   
                    
                    RoutineGridView.DataSource = rdr;
                    RoutineGridView.DataBind();
                    
                }

            }
        }
        public void RoutineGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RoutineGridView.EditIndex = e.NewEditIndex;
        }

        public object taskTable { get; set; }

        protected void RoutineGrid_Edit(object sender, GridViewEditEventArgs e)
        {
            RoutineGridView.EditIndex = e.NewEditIndex;
            RoutineGridView.DataBind();
        }
    }
}