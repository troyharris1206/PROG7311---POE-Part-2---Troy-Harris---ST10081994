using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace FarmCentral
{
    public partial class ProductsEmp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If there is a logged in user
            if (Session["Username"] != null)
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
                {
                    con.Open();

                    //Selects the FarmerID where the username in the User table is equal to the username in the Session
                    SqlCommand cmd = new SqlCommand("SELECT F.FarmerID FROM([dbo].[User] U INNER JOIN [dbo].[Farmer] F ON  U.UserID = F.UserID) "
                        + "WHERE U.Username = '" + Session["Username"] + "'", con);
                    var farmerID = cmd.ExecuteScalar();

                    //If the user logged in is a farmer
                    if (farmerID != null)
                    {
                        //Informs user that they cannot access this page
                        MessageBox.Show("Your account is not authorised to make use of this page, please sign in as an employee.", "Incorrect Account Type",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        //Logs the user out
                        Response.Redirect("Login.aspx");
                        Session["Username"] = null;
                    }
                }
            }
            //If there is no logged in user
            else
            {
                //Logs the user out
                Response.Redirect("Login.aspx");
                return;
            }
        }

        /// <summary>
        /// Event that executes when the user clicks the View All Products button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
            {
                con.Open();

                //Selects an array of columns that are relevant to display all the products to the user
                string sqlQuery = "SELECT P.FarmerID, U.FirstName AS \"FarmerName\", U.Surname AS \"FarmerSurname\", P.ProductName, P.ProductPrice, P.DateAdded, PT.ProductType, S.StockAmount " +
                    "FROM(([dbo].[Product] P INNER JOIN[dbo].[Product_Type] PT ON P.ProductTypeID = PT.ProductTypeID) INNER JOIN[dbo].[Stock] S ON S.ProductID = P.ProductID " +
                    "INNER JOIN[dbo].[Farmer] F ON F.FarmerID = P.FarmerID INNER JOIN[dbo].[User] U ON U.UserID = F.UserID) ORDER BY U.UserID";
                var dataAdapter = new SqlDataAdapter(sqlQuery, con);

                //Fills the grid view with the product data selected above
                DataTable dtTable = new DataTable();
                dataAdapter.Fill(dtTable);
                ProductsView.DataSource = dtTable;
                ProductsView.DataBind();
            }
        }

        /// <summary>
        /// Event that executes when the user clicks the Filter By Farmer button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFarmerFilter_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
            {
                con.Open();

                //Selects an array of columns that are relevant to display the products added by the FarmerID entered by the user
                string sqlQuery = "SELECT P.FarmerID, U.FirstName AS \"FarmerName\", U.Surname AS \"FarmerSurname\", P.ProductName, P.ProductPrice, P.DateAdded, PT.ProductType, S.StockAmount " +
                    "FROM(([dbo].[Product] P INNER JOIN[dbo].[Product_Type] PT ON P.ProductTypeID = PT.ProductTypeID) " +
                    "INNER JOIN[dbo].[Stock] S ON S.ProductID = P.ProductID INNER JOIN[dbo].[Farmer] F ON F.FarmerID = P.FarmerID " +
                    "INNER JOIN[dbo].[User] U ON U.UserID = F.UserID) WHERE P.FarmerID = '" + txtFarmer.Text + "'";
                var dataAdapter = new SqlDataAdapter(sqlQuery, con);

                //Fills the grid view with the product data selected above
                DataTable dtTable = new DataTable();
                dataAdapter.Fill(dtTable);
                ProductsView.DataSource = dtTable;
                ProductsView.DataBind();
            }
        }

        /// <summary>
        /// Event that executes when the user clicks the Filter By Date button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDateFilter_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
            {
                con.Open();

                //Selects an array of columns that are relevant to display the products added by the FarmerID and between the two dates entered by the user
                string sqlQuery = "SELECT P.FarmerID, U.FirstName AS \"FarmerName\", U.Surname AS \"FarmerSurname\", P.ProductName, P.ProductPrice, P.DateAdded, PT.ProductType, S.StockAmount " +
                    "FROM(([dbo].[Product] P INNER JOIN[dbo].[Product_Type] PT ON P.ProductTypeID = PT.ProductTypeID) " +
                    "INNER JOIN[dbo].[Stock] S ON S.ProductID = P.ProductID INNER JOIN[dbo].[Farmer] F ON F.FarmerID = P.FarmerID " +
                    "INNER JOIN[dbo].[User] U ON U.UserID = F.UserID) WHERE P.DateAdded BETWEEN '" + txtFirstDate.Text + "' AND '" + txtSecondDate.Text + "'" + " AND F.FarmerID = '" +
                    txtFarmer.Text + "' ORDER BY P.DateAdded";
                var dataAdapter = new SqlDataAdapter(sqlQuery, con);

                //Fills the grid view with the product data selected above
                DataTable dtTable = new DataTable();
                dataAdapter.Fill(dtTable);
                ProductsView.DataSource = dtTable;
                ProductsView.DataBind();
            }
        }

        /// <summary>
        /// Event that executes when the user clicks the Filter By Product Type button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnProductTypeFilter_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
            {
                con.Open();

                //Selects an array of columns that are relevant to display the products added by the FarmerID and the product type entered by the user
                string sqlQuery = "SELECT P.FarmerID, U.FirstName AS \"FarmerName\", U.Surname AS \"FarmerSurname\", P.ProductName, P.ProductPrice, P.DateAdded, PT.ProductType, S.StockAmount " +
                    "FROM(([dbo].[Product] P INNER JOIN[dbo].[Product_Type] PT ON P.ProductTypeID = PT.ProductTypeID) " +
                    "INNER JOIN[dbo].[Stock] S ON S.ProductID = P.ProductID INNER JOIN[dbo].[Farmer] F ON F.FarmerID = P.FarmerID " +
                    "INNER JOIN[dbo].[User] U ON U.UserID = F.UserID) WHERE PT.ProductType = '" + txtProductType.Text + "' AND F.FarmerID = '" +
                    txtFarmer.Text + "' ORDER BY U.UserID";
                var dataAdapter = new SqlDataAdapter(sqlQuery, con);

                //Fills the grid view with the product data selected above
                DataTable dtTable = new DataTable();
                dataAdapter.Fill(dtTable);
                ProductsView.DataSource = dtTable;
                ProductsView.DataBind();
            }
        }
    }
}
//----------------------------------------END OF FILE------------------------------------------------------
