using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PrjLavalifeWebServer.util;
using System.Data;

namespace PrjLavalifeWebServer.css
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStep2();

                cboMonth.DataSource = Constants.MONTHS;
                cboMonth.AppendDataBoundItems = true;
                cboMonth.DataTextField = "Value";
                cboMonth.DataValueField = "Key";
                cboMonth.DataBind();

                cboDay.DataSource = Constants.DAYS_OF_MONTHS;
                cboDay.AppendDataBoundItems=true;
                cboDay.DataValueField = "Key";
                cboDay.DataTextField = "Value";
                cboDay.DataBind();

                cboYear.DataSource = Constants.YEARS;
                cboYear.AppendDataBoundItems = true;
                cboYear.DataValueField = "Key";
                cboYear.DataTextField = "Value";
                cboYear.DataBind();

                cboIma.DataSource = DbService.GetInterstedFields();
                cboIma.AppendDataBoundItems = true ;
                cboIma.DataTextField = "Value";
                cboIma.DataValueField = "Key";
                cboIma.DataBind();
            }
        }

        protected void btnLink_Click(object sender, EventArgs e)
        {
            Response.Redirect(util.Constants.PAGE_LOGIN);
        }

        protected void btnJoinNow_Click(object sender, EventArgs e)
        {

            string fname = txtFname.Text;
            string lname = txtLname.Text;
            string interested = cboIma.Text;
            string month = cboMonth.SelectedItem.Value;
            string day = cboDay.Text;
            string year = cboYear.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string gender = radGender.SelectedItem.Value;
            string username = txtUsername.Text;

            if(!string.IsNullOrEmpty(fname) &&
              !string.IsNullOrEmpty(lname) &&
              !string.IsNullOrEmpty(username) &&
              !string.IsNullOrEmpty(gender) &&
              !string.IsNullOrEmpty(interested) &&
              !string.IsNullOrEmpty(month) &&
              !string.IsNullOrEmpty(day) &&
              !string.IsNullOrEmpty(year) &&
              !string.IsNullOrEmpty(email) &&
              !string.IsNullOrEmpty(password))
            {
                bool isNew = DbService.CheckUsername(username);

                if (!isNew)
                {
                    lblError.Text = "Username already exist";
                    return;
                }

                DateTime dob = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                
                int userid = DbService.CreateUser(fname, lname, username, gender, interested, dob, email, password);

                hidUserId.Value = userid.ToString();

                LoadStep2();

                step1.CssClass = step1.CssClass + " hidden";
                step2.CssClass = step2.CssClass.Replace("hidden", "");

            }
        }

        private void LoadStep2()
        {
            DataSet set = DbService.LoadDropDownForStep2();

            cboBodyType.DataSource = set.Tables[Constants.TABLE_BODY_TYPES];
            cboBodyType.AppendDataBoundItems = true;
            cboBodyType.DataTextField = "BodyTypeName";
            cboBodyType.DataValueField = "RefBodyType";
            cboBodyType.DataBind();

            cboEthnicity.DataSource = set.Tables[Constants.TABLE_ETHINICITIES];
            cboEthnicity.AppendDataBoundItems = true;
            cboEthnicity.DataTextField = "EthinicityName";
            cboEthnicity.DataValueField = "RefEthinicity";
            cboEthnicity.DataBind();

            cboReligion.DataSource = set.Tables[Constants.TABLE_RELIGIONS];
            cboReligion.AppendDataBoundItems = true;
            cboReligion.DataTextField = "ReligionName";
            cboReligion.DataValueField = "RefReligion";
            cboReligion.DataBind();

            cboEducation.DataSource = set.Tables[Constants.TABLE_EDUCATION];
            cboEducation.AppendDataBoundItems = true;
            cboEducation.DataTextField = "EducationType";
            cboEducation.DataValueField = "RefEducation";
            cboEducation.DataBind();

            cboLooking.DataSource = set.Tables[Constants.TABLE_LOOKING_FOR];
            cboLooking.AppendDataBoundItems = true;
            cboLooking.DataTextField = "LookingType";
            cboLooking.DataValueField = "RefLookingFor";
            cboLooking.DataBind();

            cboSmoking.DataSource = set.Tables[Constants.TABLE_SMOKING];
            cboSmoking.AppendDataBoundItems = true;
            cboSmoking.DataTextField = "SmokingType";
            cboSmoking.DataValueField = "RefSmoking";
            cboSmoking.DataBind();

            cboDrinking.DataSource = set.Tables[Constants.TABLE_DRINKING];
            cboDrinking.AppendDataBoundItems = true;
            cboDrinking.DataTextField = "DrinkingType";
            cboDrinking.DataValueField = "RefDrinking";
            cboDrinking.DataBind();

            cboHeight.DataSource = DbService.GetHeights();
            cboHeight.AppendDataBoundItems = true;
            cboHeight.DataTextField = "height";
            cboHeight.DataValueField = "value";
            cboHeight.DataBind();

            cboInches.DataSource = DbService.GetInches();
            cboInches.AppendDataBoundItems = true;
            cboInches.DataTextField = "inche";
            cboInches.DataValueField = "value";
            cboInches.DataBind();


        }

        protected void btnSubmitStep2_Click(object sender, EventArgs e)
        {
            cboBodyType.SelectedIndex = 1;
        }
    }
}