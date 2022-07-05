using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PrjLavalifeWebServer.util;
using System.Data;

namespace PrjLavalifeWebServer
{
    public partial class UserProfile : System.Web.UI.Page
    {
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count == 0)
            {
                Response.Redirect(Constants.PAGE_DASHBOARD);
            }
            
            username = Request.QueryString["username"].ToString();

            DataRow user= DbService.GetUserByUsername(username);

            if (user != null)
            {
                Initialize(user);
            }
        }

        private void Initialize(DataRow user)
        {
            //imgSearchUser.ImageUrl 

            litName.Text = user.Field<String>("firstname") + " " + user.Field<String>("lastname");
            litUsername.Text = user.Field<string>("username");
            litGender.Text = user.Field<string>("gender");
            litEmail.Text = user.Field<string>("email");
            litBirthdate.Text = user.Field<DateTime>("BirthDate").ToString("dddd, dd MMMM yyyy");

            litPostalCode.Text = user.Field<string>("postalcode");
            litHeight.Text = user.Field<string>("height");

            litBodyType.Text = user["RefBodyType"].ToString();
            litDrinking.Text = user["RefDrinking"].ToString();
            litEducation.Text = user["RefEducation"].ToString();
            litEthinicity.Text = user["RefEthinicity"].ToString();
            litLooking.Text = user["RefLookingFor"].ToString();
            litReligion.Text = user["RefReligion"].ToString();
            litSmoking.Text = user["RefSmoking"].ToString();
            litInterested.Text = user["RefInterested"].ToString();
        }
    }
}