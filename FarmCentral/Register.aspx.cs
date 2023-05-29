using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace FarmCentral
{
    public partial class Register : System.Web.UI.Page
    {
        private AccountProcesses accountprocesses = new AccountProcesses();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event that executes when the user clicks the 'Register Employee Account' button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (var db = new FarmCentralDBEntities())
            {               
                //Selects the User record where the username entered equals a username in the User table
                var usernameCheck = db.Users.Where(a => a.Username.Equals(txtUsername.Text)).Count();

                //If the username entered already exists in the User table
                if(usernameCheck == 1)
                {
                    //Informs the user that the username is already in use and to select another
                    MessageBox.Show("This username is already in use. Please try another one.", "Username Taken",
                        MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
                }

                //If the passwords entered don't match
                if (!(txtPassword.Text.Equals(txtConfirmPassword.Text)))
                {
                    //Informs the user that the passwords don't match and to try again
                    MessageBox.Show("Passwords don't match. Please try again.", "Passwords Don't Match",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //Assigns the random recovery key generated
                string recoveryKey = accountprocesses.RandomRecoveryCode();
                //Assigns the user id
                var userID = "USR" + (db.Users.Count() + 1).ToString();
                //Assigns the employee id
                var employeeID = "EMP" + (db.Employees.Count() + 1).ToString();

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

                //Inserting the user object into the User table
                db.Users.Add(user);
                db.SaveChanges();

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
                {
                    con.Open();

                    //Inserts the employee details into the employee table
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Employee] VALUES ('" + employeeID + "', '" + userID + "')", con);
                    cmd.ExecuteScalar();
                }
                
                //Informs the user that their account has been registered
                MessageBox.Show("Thank you " + user.FirstName + ", your Employee account has been registered successfully.", 
                    "Employee Account Registered Successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                //Displays the users recovery key to them
                MessageBox.Show("Your recovery key to reset the password on your account, should you forget your password, is: " + recoveryKey + ". Please save this somewhere safe.",
                    "Employee Account Recovery Key", MessageBoxButton.OK, MessageBoxImage.Information);

                //Redirects user to the login page
                Response.Redirect("Login.aspx");
            }
        }
    }
}
//----------------------------------------END OF FILE------------------------------------------------------
