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
    public partial class MyProperties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProperties();
            }
        }

        public void LoadProperties()
        {
            string AgentID = "0";

            if (Session["ID"] != null)
            {
                AgentID = Session["ID"].ToString();
            }


            DataTable dtBanner = DBHandler.GetData(string.Format(@"
select * from properties where agentid = '{0}'", AgentID));

            if (dtBanner.Rows.Count > 0)
            {


                rptProperties.DataSource = dtBanner;
                rptProperties.DataBind();

            }
        }
    }
}