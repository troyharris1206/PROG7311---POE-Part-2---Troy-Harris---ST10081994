using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Windows;
using System.Text;

namespace FarmCentral
{
    public partial class Login : System.Web.UI.Page
    {
        AccountProcesses accountProcesses = new AccountProcesses();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLoginError.Visible = false;
        }

        /// <summary>
        /// Event that executes when the user clicks the login button on Login.aspx 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
            {
                con.Open();

                //Selects the UserID in the User table where the username entered finds a match
                SqlCommand cmd = new SqlCommand("SELECT U.UserID FROM [dbo].[User] U WHERE U.Username = '" + txtUsername.Text + "'", con);
                var userID = cmd.ExecuteScalar();

                //Selects the Password in the User table where the username entered finds a match
                cmd = new SqlCommand("SELECT U.Password FROM [dbo].[User] U WHERE U.Username = '" + txtUsername.Text + "'", con);
                var encryptedPW = cmd.ExecuteScalar();

                var frmUser = 0;               

                //If the username entered is present in the User table
                if (userID != null)
                {
                    //If the password entered is incorrect
                    if (!(accountProcesses.Decrypt(encryptedPW.ToString()).Equals(txtPassword.Text)))
                    {
                        //Shows user the lbl which informs them their password is incorrect
                        lblLoginError.Text = "Incorrect password for " + userID + ".";
                        lblLoginError.Visible = true;
                        return;
                    }

                    using (var db = new FarmCentralDBEntities())
                    {
                        //Checks whether the UserID is present in the Farmer table
                        frmUser = db.Farmers.Where(a => a.UserID.Equals(userID.ToString())).Count();
                    }                       

                    //If the UserID finds a match in the Farmer table
                    if (frmUser == 1)
                    {
                        //Assigns the username to the session
                        Session["Username"] = txtUsername.Text;
                        //User is redirected to the Farmer site
                        Response.Redirect("ProductsFarm.aspx");
                    }
                    //If the UserID is present in the Employee table
                    else
                    {
                        //Assigns the username to the session
                        Session["Username"] = txtUsername.Text;
                        //User is redirected to the Employee site
                        Response.Redirect("ProductsEmp.aspx");
                    }                   
                }
                //If the user details entered are invalid
                else
                {
                    //Shows user the lbl which informs them the details entered is incorrect
                    lblLoginError.Text = "No users matching the details entered were found.";
                    lblLoginError.Visible = true;
                }
            }
        }
    }
}
//----------------------------------------END OF FILE------------------------------------------------------
