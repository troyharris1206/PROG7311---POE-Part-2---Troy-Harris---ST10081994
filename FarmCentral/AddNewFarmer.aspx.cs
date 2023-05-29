using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace FarmCentral
{
    public partial class AddNewFarmer : System.Web.UI.Page
    {
        private AccountProcesses accountprocesses = new AccountProcesses();

        protected void Page_Load(object sender, EventArgs e)
        {
            //If there is a logged in user
            if (Session["Username"] != null)
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
                {
                    con.Open();

                    //Selects the FarmerID of the logged in user
                    SqlCommand cmd = new SqlCommand("SELECT F.FarmerID FROM([dbo].[User] U INNER JOIN [dbo].[Farmer] F ON  U.UserID = F.UserID) "
                        + "WHERE U.Username = '" + Session["Username"] + "'", con);
                    var farmerID = cmd.ExecuteScalar();

                    //If the logged in user is a farmer
                    if (farmerID != null)
                    {
                        //Informs user that they need to be an employee to access this page
                        MessageBox.Show("Your account is not authorised to make use of this page, please sign in as an employee.", "Incorrect Account Type",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        //Redirects user to login
                        Response.Redirect("Login.aspx");
                        Session["Username"] = null;
                    }
                }
            }
            //If there is no logged in user
            else
            {
                //Redirects user to login
                Response.Redirect("Login.aspx");
            }
        }

        /// <summary>
        /// Event that occurs when the user clicks the Add New Farmer button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (var db = new FarmCentralDBEntities())
            {
                //Counts the username occurences in the db of the username entered by the user
                var usernameCheck = db.Users.Where(a => a.Username.Equals(txtUsername.Text)).Count();

                //If the username entered by the user already exists in the db
                if (usernameCheck == 1)
                {
                    //Informs user to pick another username
                    MessageBox.Show("This username is already in use. Please try another one.", "Username Taken",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //If the passwords entered don't match
                if (!(txtPassword.Text.Equals(txtConfirmPassword.Text)))
                {
                    //Informs user that the passwords don't match
                    MessageBox.Show("Passwords don't match. Please try again.", "Passwords Don't Match",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //Sets the random recovery key 
                string recoveryKey = accountprocesses.RandomRecoveryCode();
                //Sets the UserID
                var userID = "USR" + (db.Users.Count() + 1).ToString();
                //Sets the FarmerID
                var farmerID = "FRM" + (db.Farmers.Count() + 1).ToString();

                //Creates a new user object
                var user = new User
                {
                    UserID = userID,
                    FirstName = txtFirstName.Text,
                    Surname = txtSurname.Text,
                    Username = txtUsername.Text,
                    Password = accountprocesses.Encrypt(txtPassword.Text),
                    RecoveryKey = recoveryKey
                };

                //Inserts the user details into the User table
                db.Users.Add(user);
                db.SaveChanges();

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
                {
                    con.Open();

                    //Inserts the farmer details into the Farmer table
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Farmer] VALUES ('" + farmerID + "', '" + userID + "')", con);
                    cmd.ExecuteScalar();
                }

                //Informs user that the account creaion was a success
                MessageBox.Show("Thank you for registering an accont for Farmer: " + user.FirstName + ". The account has been registered successfully.",
                    "Farmer Account Registered Successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                //Shows user their assigned account recovery key
                MessageBox.Show("The recovery key to reset the password on this Farmer account, should they forget their password, is: " + recoveryKey + ". Please save this " +
                    "somewhere safe.", "Farmer Account Recovery Key", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }       
    }
}
//----------------------------------------END OF FILE------------------------------------------------------
