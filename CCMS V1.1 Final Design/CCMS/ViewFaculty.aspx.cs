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
    public partial class ViewFaculty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        }

        #region Bind GridView
        private void BindGridView()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(GetConnectionString());
            try
            {
                connection.Open();
                string sqlStatement = "SELECT * FROM faculty";
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);

                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetch Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Insert New or Update Record
        private void UpdateOrAddNewRecord(int FId, string FirstName, string LastName, string Email, long Contact, bool Active, bool isUpdate)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            string sqlStatement = string.Empty;

            if (!isUpdate)
            {
                sqlStatement = "INSERT INTO faculty" +
    "(FirstName,LastName,Email,Contact,Active)" +
    "VALUES (@FirstName,@LastName,@Email,@Contact,@Active)";
            }
            else
            {
                sqlStatement = "UPDATE faculty " +
                               "SET FirstName = @FirstName," +
                               "LastName = @LastName,Email = @Email,Contact = @Contact,Active = @Active" +
                               " WHERE FId = @FId";
            }
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@FId", FId);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Contact", Contact);
                cmd.Parameters.AddWithValue("@Active", Active);
                cmd.CommandType = CommandType.Text;
                int value = cmd.ExecuteNonQuery();
                if (value > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('Updated successfully.');", true);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert/Update Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1; //swicth back to default mode
            BindGridView(); // Rebind GridView to show the data in default mode
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
            BindGridView(); // Rebind GridView to show the data in edit mode
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Accessing Edited values from the GridView
            int FId = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[0].Text); //Faculty ID
            //int UserID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[1].Text); //User ID
            string FirstName = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text; //First Name
            string LastName = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text; //Last Name
            string Email = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text; //Email
            long Contact = Convert.ToInt64(((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text); //Contact Number
            bool Active = Convert.ToBoolean(((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text); // Active Status

            UpdateOrAddNewRecord(FId, FirstName, LastName, Email, Contact, Active, true); // call update method
            GridView1.EditIndex = -1;
            BindGridView(); // Rebind GridView to reflect changes made
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[0].Text); //get the id of the selected row
            DeleteRecord(id);//call delete method
            BindGridView();//rebind grid to reflect changes made
        }

        #region Delete Record
        private void DeleteRecord(int FId)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            string sqlStatement = "DELETE FROM faculty WHERE FId = @FId";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@FId", FId);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Deletion Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
    }
}
