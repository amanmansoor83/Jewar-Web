using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jewar.CodeLibrary;

namespace Jewar_API
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public string AllProperties = "0";
        public string TotalViews = "0";
        public string TotalVisitorReviews= "0";
        public string TotalFavourites= "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    FillBoxes();
                }
                catch (Exception ee)
                { }
            }
        }


        public void FillBoxes()
        {
            try
            {
                //fill datatable from db 

                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                string StartDate = month.ToString("yyyy-MM-dd");
                string EndDate = month.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59");

                string AgentID = "0";

                if (Session["ID"] != null)
                {
                    AgentID = Session["ID"].ToString();
                }


                DataTable dtTotalPendingEnquiries = new DataTable(); // DBHandler.GetData("select * from users where type = 'BDE' or type = 'Salesman' order by Name");                      
                DataTable dtTotalVerifiedmembers = new DataTable();
                DataTable dtTotalPendingmembers = new DataTable();
                DataTable dtTotalorders = new DataTable();
                DataTable dtTotalRevenue = new DataTable();


                #region TotalPendingmembers

                dtTotalPendingmembers = DBHandler.GetData(string.Format("SELECT  COUNT(m.ID) AS TotalCount FROM properties m WHERE agentid  = {0}", AgentID));



                if (dtTotalPendingmembers.Rows.Count > 0)
                {
                    AllProperties = dtTotalPendingmembers.Rows[0]["TotalCount"].ToString();
                }
                #endregion

                #region Totalorders

                dtTotalorders = DBHandler.GetData(string.Format("SELECT  COUNT(o.ID) AS TotalCount FROM views o   ", AgentID));


                if (dtTotalorders.Rows.Count > 0)
                {
                    TotalViews = dtTotalorders.Rows[0]["TotalCount"].ToString();
                }
                #endregion

                #region Confirmedorders

                dtTotalorders = DBHandler.GetData(string.Format("SELECT  COUNT(o.ID) AS TotalCount FROM review o  ",AgentID));


                if (dtTotalorders.Rows.Count > 0)
                {
                    TotalVisitorReviews = dtTotalorders.Rows[0]["TotalCount"].ToString();
                }
                #endregion


                #region Confirmedorders

                dtTotalorders = DBHandler.GetData(string.Format("SELECT  COUNT(o.ID) AS TotalCount FROM review o  ", AgentID));


                if (dtTotalorders.Rows.Count > 0)
                {
                    TotalFavourites = dtTotalorders.Rows[0]["TotalCount"].ToString();
                }
                #endregion

            }
            catch (Exception ex)
            {
                //dvError.Visible = true;
                //spnError.InnerText = "Error in FillBoxes()- " + ex.Message;
            }
        }

    }
}