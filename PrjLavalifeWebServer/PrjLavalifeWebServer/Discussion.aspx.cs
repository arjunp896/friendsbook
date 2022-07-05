using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PrjLavalifeWebServer.util;
using System.Linq;

namespace PrjLavalifeWebServer
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        static string currentUser ;
        static string receiverId;
        DataTable messages;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.SESSION_USERID].ToString() == "-1")
            {
                Response.Redirect(Constants.PAGE_INDEX);
            }

            currentUser = Session[Constants.SESSION_USERID].ToString();

            messages = DbService.GetMessages(currentUser);

            LoadMessages();


        }

        private void LoadMessages()
        {
            var chatList = from DataRow row in messages.Rows
                           where (row["RefReceiver"].ToString() != currentUser)
                          
                           select new
                           {
                               refuser = row["RefUser"],
                               username = row["username"],
                               name = row["lastname"] + " " + row["lastname"]
                           };

            chatList = chatList.GroupBy(g =>g.refuser).Select(c => c.FirstOrDefault());

            repeateChatList.DataSource = chatList;

            repeateChatList.DataBind();

        }

        protected void btnChatRow_Click(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;

            receiverId = (button.FindControl("hidRefUser") as HiddenField).Value;

            LoadDiscussion();
        }

        private void LoadDiscussion()
        {
            var data = from DataRow row in messages.Rows
                       where (row["RefReceiver"].ToString() == receiverId || row["RefSender"].ToString() == receiverId)
                       orderby row.Field<DateTime?>("CreateDate") ascending
                       select new
                       {

                           sender = (row["RefSender"].ToString() == currentUser),
                           refuser = row["RefUser"],
                           username = row["username"],
                           //name = row["lastname"] + " " + row["lastname"]
                           message = row["Message"],
                           image = row["ImageUrl"],
                           createdate = row.Field<DateTime?>("CreateDate").Value.ToString("hh:mm tt | MMMM dd")
                       };

            repeateMsg.DataSource = data;
            repeateMsg.DataBind();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtWriteMsg.Text;

            if (string.IsNullOrEmpty(message))
            {
                return;
            } 

            bool result = DbService.CreateMessage(message, currentUser,receiverId);

            if (result)
            {
                messages = DbService.GetMessages(currentUser);
                LoadDiscussion();
            }
        }
    }
}