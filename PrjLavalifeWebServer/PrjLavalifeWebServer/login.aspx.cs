using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PrjLavalifeWebServer.util;

namespace PrjLavalifeWebServer
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                int userid = DbService.ValidateUser(email, password);
                if (userid > 0)
                {
                    Session[Constants.SESSION_USERID] = userid;

                    Response.Redirect(Constants.PAGE_DASHBOARD);
                }
                else
                {
                    lblError.Text = "username or password is incorrect!";
                }
            }
        }
    }
}