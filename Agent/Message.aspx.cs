using Jewar.CodeLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jewar_API
{
    public partial class Message : System.Web.UI.Page
    {
        public DataTable dtMessages = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadChats();
            }
        }

        public void LoadChats()
        {
            string AgentID = "0";

            if (Session["AgentID"] != null)
            {
                AgentID = Session["AgentID"].ToString();
            }


            DataTable dtBanner = DBHandler.GetData(string.Format(@"
select * from chat c inner join agent a on c.agenttoid = a.id where AgentFromID = '{0}'", AgentID));

            if (dtBanner.Rows.Count > 0)
            {


                rptMessages.DataSource = dtBanner;
                rptMessages.DataBind();

            }
        }

        protected void rptMessages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "update")
            {

                string ChatID = e.CommandArgument.ToString();
 
                



                    dtMessages = DBHandler.GetData(string.Format("SELECT * FROM chatmessages WHERE chatid = '{0}' order by Created", ChatID));

                  
                    if (dtMessages.Rows.Count > 0)
                    {
                        rtpMessages.DataSource = dtMessages;
                        rtpMessages.DataBind();
                    }
               


            }
        }
    }
}