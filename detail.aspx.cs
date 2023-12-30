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
    public partial class detail : System.Web.UI.Page
    {

        public string Title = "";
        public string Address = "";
        public string Listed = "";
        //public string Title = "";
        //public string Title = "";
        //public string Title = "";
        //public string Title = "";
        //public string Title = "";
        //public string Title = "";
        //public string Title = "";
        //public string Title = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PropertyID = "";

                if (Request["id"] != null)
                {
                    PropertyID = Cryptography.DecryptMessage(Request["p"].ToString());


                    DataTable dtPropertyImages = DBHandler.GetData(string.Format("select * from propertieimages where propertyid = '{0}'", PropertyID));


                    DataTable dtPropertyDetail = DBHandler.GetData(string.Format("select * from propertieimages where propertyid = '{0}'", PropertyID));

                    if (dtPropertyImages != null)
                    {
                        if (dtPropertyImages.Rows.Count > 0)
                        {
                            rptPropertyImages.DataSource = dtPropertyImages;
                            rptPropertyImages.DataBind();
                        }
                    }

                    if (dtPropertyImages != null)
                    {


                    }
                }
            }
        }
         
    }
}