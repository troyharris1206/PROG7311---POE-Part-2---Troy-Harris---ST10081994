using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace FarmCentral
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        private AccountProcesses accountprocesses = new AccountProcesses();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event that occurs when the user clicks the Reset Password button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (var db = new FarmCentralDBEntities())
            {
                //Selects the record in the User table where the username and recovery key entered by the user find a match
                var result = db.Users.SingleOrDefault(a => a.Username.Equals(txtUsername.Text) && a.RecoveryKey.Equals(txtRecoveryKey.Text));

                //If the above query finds a match
                if (result != null)
                {
                    //If the passwords entered don't match each other
                    if (!(txtPassword.Text.Equals(txtConfirmPassword.Text)))
                    {
                        //Informs user that the passwords don't match
                        MessageBox.Show("Passwords don't match. Please try again.", "Passwords Don't Match",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    //Assigns the new encrypted password entered by the user
                    result.Password = accountprocesses.Encrypt(txtPassword.Text);
                    db.SaveChanges();

                    //Informs user that the password reset was a success
                    MessageBox.Show("Thank you, " + result.FirstName + " your password has been reset successfully.", "Password Reset Successful", 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    //Redirects user to login
                    Response.Redirect("Login.aspx");
                }
                //If the query didn't find any matches
                else
                {
                    //Informs user that the account wasn't found
                    MessageBox.Show("We could not find any accounts matching the information entered. Please try again.", "Account Not Found",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
//----------------------------------------END OF FILE------------------------------------------------------
