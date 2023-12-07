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
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPropertiesByCities();
            }
        }


        public void FillPropertiesByCities()
        {
            DataTable dtProerties = DBHandler.GetData(string.Format(@"
select City, count(ID) as TotalProperties from properties group by city"));

            if (dtProerties.Rows.Count > 0)
            {


                rptPropertiesbyCities.DataSource = dtProerties;
                rptPropertiesbyCities.DataBind();

            }
        }

    }
}