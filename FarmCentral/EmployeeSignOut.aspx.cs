﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FarmCentral
{
    public partial class EmployeeSignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Signs the employee out by taking them back to login
            Session["Username"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}
//----------------------------------------END OF FILE------------------------------------------------------
