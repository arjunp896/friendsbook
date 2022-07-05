using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PrjLavalifeWebServer.util;

namespace PrjLavalifeWebServer
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static DataSet myset;
        string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.SESSION_USERID].ToString() == "-1")
            {
                Response.Redirect(Constants.PAGE_INDEX);
            }

            username = Session[Constants.SESSION_USERID].ToString();

            if (!IsPostBack)
            {
                myset = DbService.LoadDashboardSetUsers(username);

                DataRow user = DbService.GetUserByUserid(username);

                if (user != null)
                {


                    Initialize();

                }


            }
        }

        private void Initialize()
        {

            repeatUserSearchResult.DataSource = myset.Tables["Users"];
            repeatUserSearchResult.DataBind();

            cboLooking.DataSource = myset.Tables[Constants.TABLE_LOOKING_FOR];
            cboLooking.AppendDataBoundItems = true;
            cboLooking.DataTextField = "LookingType";
            cboLooking.DataValueField = "RefLookingFor";
            cboLooking.DataBind();

            cboAgeFrom.DataSource = myset.Tables[Constants.TABLE_AGES];
            cboAgeFrom.AppendDataBoundItems = true;
            cboAgeFrom.DataTextField = "Year";
            cboAgeFrom.DataValueField = "Year";
            cboAgeFrom.DataBind();


            cboAgeTo.DataSource = myset.Tables[Constants.TABLE_AGES];
            cboAgeTo.AppendDataBoundItems = true;
            cboAgeTo.DataTextField = "Year";
            cboAgeTo.DataValueField = "Year";
            cboAgeTo.DataBind();

            cboEthinicity.DataSource = myset.Tables[Constants.TABLE_ETHINICITIES];
            cboEthinicity.AppendDataBoundItems = true;
            cboEthinicity.DataTextField = "EthinicityName";
            cboEthinicity.DataValueField = "RefEthinicity";
            cboEthinicity.DataBind();

            cboReligion.DataSource = myset.Tables[Constants.TABLE_RELIGIONS];
            cboReligion.AppendDataBoundItems = true;
            cboReligion.DataTextField = "ReligionName";
            cboReligion.DataValueField = "RefReligion";
            cboReligion.DataBind();

        }

        protected void search_SelectedIndexChanged(object sender, EventArgs e)
        {
            var usersTable = myset.Tables[Constants.TABLE_USERS];

            string religion = cboReligion.SelectedValue;
            string ethinicity = cboEthinicity.SelectedValue;
            string looking = cboLooking.SelectedValue;
            string gender = radGender.SelectedValue;

            EnumerableRowCollection<DataRow> data = usersTable.AsEnumerable();

            if (!String.IsNullOrEmpty(religion))
            {
                data = from user in usersTable.AsEnumerable()
                       where (user.Field<string>("RefReligion") == religion)
                       select user;
            }

            if (!String.IsNullOrEmpty(ethinicity))
            {
                data = from user in data
                       where (user.Field<string>("RefEthinicity") == ethinicity)
                       select user;
            }

            if (!string.IsNullOrEmpty(cboAgeFrom.SelectedValue))
            {
                int fromAge = int.Parse(cboAgeFrom.SelectedValue);

                data = from user in data
                       where (DateTime.Today.Year - user.Field<DateTime>("BirthDate").Year >= fromAge)
                       select user;
            }

            if (!String.IsNullOrEmpty(cboAgeTo.SelectedValue))
            {
                int toAge = int.Parse(cboAgeTo.SelectedValue);

                data = from user in data
                       where (DateTime.Today.Year - user.Field<DateTime>("BirthDate").Year <= toAge)
                       select user;
            }

            if (!String.IsNullOrEmpty(looking))
            {
                data = from user in data
                       where (user.Field<string>("RefLookingFor") == looking)
                       select user;
            }

            if (!String.IsNullOrEmpty(gender))
            {
                data = from user in data
                       where (user.Field<string>("gender") == gender)
                       select user;
            }

            repeatUserSearchResult.DataSource = data.AsDataView().ToTable();
            repeatUserSearchResult.DataBind();
        }

        protected void radGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            //litUsername.Text = radGender.SelectedValue;
        }
    }
}
