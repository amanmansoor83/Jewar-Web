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
    public partial class listing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string SearchQuery = "";

                if (Request["p"] != null)
                {
                    SearchQuery = Cryptography.DecryptMessage(Request["p"].ToString());


                    DataTable dtSearchResult = DBHandler.GetData(string.Format("select * from properties where {0}", SearchQuery));
                    if (dtSearchResult != null)
                    {
                        if (dtSearchResult.Rows.Count > 0)
                        {
                            rptSearchList.DataSource = dtSearchResult;
                            rptSearchList.DataBind();
                        }
                    }
                }
            }
        }
         
    }
}