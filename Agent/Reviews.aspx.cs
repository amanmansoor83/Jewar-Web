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
    public partial class Reviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReviews();
            }
        }

        public void LoadReviews()
        {
            string AgentID = "0";

            if (Session["ID"] != null)
            {
                AgentID = Session["ID"].ToString();
            }


            DataTable dtBanner = DBHandler.GetData(string.Format(@"
select * from reviews where propertyid in (select id from properties  where agentid = '{0}')", AgentID));

            if (dtBanner.Rows.Count > 0)
            {


                rptReviews.DataSource = dtBanner;
                rptReviews.DataBind();

            }
        }
    }
}