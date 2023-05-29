using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace FarmCentral
{
    public partial class ProductsFarm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If there is a user logged in
            if (Session["Username"] != null)
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
                {
                    con.Open();

                    //Selects the FarmerID from the User table where the username in the User table equals the username in the Session
                    SqlCommand cmd = new SqlCommand("SELECT F.FarmerID FROM([dbo].[User] U INNER JOIN [dbo].[Farmer] F ON  U.UserID = F.UserID) "
                        + "WHERE U.Username = '" + Session["Username"] + "'", con);
                    var farmerID = cmd.ExecuteScalar();

                    //If the user logged in is an employee
                    if (farmerID == null)
                    {
                        //Informs user that they cannot access this page
                        MessageBox.Show("Your account is not authorised to make use of this page, please sign in as a farmer.", "Incorrect Account Type",
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
        /// Event that executes when the user clicks the Add Product button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var db = new FarmCentralDBEntities())
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCONSTRING"].ConnectionString))
                {
                    con.Open();

                    //Selects the ProductName from the Product table where the product name entered finds a match 
                    SqlCommand cmd = new SqlCommand("SELECT P.ProductName FROM [dbo].[Product] P WHERE P.ProductName = '" + txtProductName.Text + "'", con);
                    var existingProductCheck = cmd.ExecuteScalar();

                    //If the product name entered doesn't exist in the Product table
                    if (existingProductCheck == null)
                    {
                        //Assigns the ProductID 
                        var productID = "PRD" + (db.Products.Count() + 1).ToString();
                        //Assigns the StockID
                        var stockID = "STK" + (db.Stocks.Count() + 1).ToString();

                        //Selects the ProductType from the Product_Type table where the ProductType entered finds a match
                        cmd = new SqlCommand("SELECT P.ProductTypeID FROM [dbo].[Product_Type] P WHERE P.ProductType = '" + txtProductType.Text + "'", con);
                        var existingProductTypeID = cmd.ExecuteScalar();

                        //Selects the FarmerID of the user that adds the product
                        cmd = new SqlCommand("SELECT F.FarmerID FROM([dbo].[User] U INNER JOIN [dbo].[Farmer] F ON  U.UserID = F.UserID) "
                            + "WHERE U.Username = '" + Session["Username"] + "'", con);
                        var farmerID = cmd.ExecuteScalar();

                        string productTypeID = "";

                        //If the product type entered doesn't exist in the Product_Type table
                        if (existingProductTypeID == null)
                        {
                            //Assigns the ProductTypeID
                            productTypeID = "PRT" + (db.Product_Type.Count() + 1).ToString();

                            //Creates a new Product_Type object
                            var productType = new Product_Type
                            {
                                ProductTypeID = productTypeID,
                                ProductType = txtProductType.Text
                            };

                            //Inserts the productType object into the Prodect_Type table
                            db.Product_Type.Add(productType);
                            db.SaveChanges();
                        }
                        //If the product type entered already exists in the Product_Type table
                        else
                        {
                            productTypeID = existingProductTypeID.ToString();
                        }                       

                        //Inserts the product details entered into the Product table 
                        cmd = new SqlCommand("INSERT INTO [dbo].[Product] VALUES ('" + productID + "', '" + farmerID
                        + "', '" + txtProductName.Text + "', '" + decimal.Parse(txtProductPrice.Text) + "', '"
                        + DateTime.Now.Date + "', " + "'" + productTypeID + "')", con);
                        cmd.ExecuteScalar();

                        //Inserts the stock details entered into the Stock table
                        cmd = new SqlCommand("INSERT INTO [dbo].[Stock] VALUES ('" + stockID + "', '" + productID
                        + "', '" + int.Parse(txtStockAmount.Text) + "')", con);
                        cmd.ExecuteScalar();

                        //Informs user that the product has been added successfully
                        MessageBox.Show("Your product, " + txtProductName.Text + ", has been successfully logged.",
                            "Product Successfully Logged", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    //If the product name entered already exists under the user account
                    else
                    {
                        //Informs user that the product that they've entered already exists under their account
                        MessageBox.Show("The product, " + txtProductName.Text + ", already exists under your account.",
                            "Product Already Exists", MessageBoxButton.OK, MessageBoxImage.Information);
                    }                  
                }           
            }
        }
    }
}
//----------------------------------------END OF FILE------------------------------------------------------
