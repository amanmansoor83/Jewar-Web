using Jewar.CodeLibrary;
using Jewar.Handler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Broadway_New.Handler
{
    public partial class ExtraInfo : System.Web.UI.Page
    {
        public string ItemOptions = "";

        public string CartButton = "";
        public string CartButtonMobile = "";
        public string DeliveryTime = "";
        public string OutletID = "5437";
        public string OperationalHour = "12:00 AM to 12:00 PM";
        public string Areas = "";
        public string City = ""; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["ItemID"] != null && Request["SizeID"] != null)
            {
                //LoadOption(Request["ItemID"].ToString(), Request["SizeID"].ToString());
                LoadOptionNew(Request["ItemID"].ToString(), Request["SizeID"].ToString());
            }

            if (Request["city"] != null)
            {
                LoadOutlet();
            }

             DataTable dtCart = (DataTable)Session["Cart"];
             if (dtCart == null || dtCart.Rows.Count == 0)
             {
                 CartButton = string.Format(@"<div class='cart-button'>
                                            <button data-target='slide-cart'  class='sidenav-trigger custom-button custom-button--secondary   custom-button--size-medium cart-button__button' title='' type='button'>
                                                <span class='jss65'>
                                                    <svg class='jss21' focusable='false' viewBox='0 0 24 24' aria-hidden='true'>
                                                        <path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
                                                    <span class='jss66 cart-icon__badge' >0</span></span>
                                                <span class='cart-button__button-text' >Cart: Rs 0.00</span></button>
                                        </div>");


                 CartButtonMobile = string.Format(@"<a tabindex='0' data-target='slide-cart' class='sidenav-trigger header-mobile__button header-mobile__button-profile' ><span class='jss43'>

          <svg class='jss21 jss44' focusable='false' viewBox='0 0 24 24' aria-hidden='true'><path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
        </span><span class='jss47 cartQty'>0</span></a>");
             }
             else
             {
                 string NetTotal = dtCart.Compute("Sum(Total)", "").ToString();
                 int TotalItems = 0;
                 for (int a = 0; a < dtCart.Rows.Count; a++)
                 {
                     TotalItems += Convert.ToInt32(dtCart.Rows[a]["Qty"]);
                 }

                 CartButton = string.Format(@"<div class='cart-button'>
                                            <button data-target='slide-cart'   class='sidenav-trigger custom-button custom-button--secondary custom-button--size-medium cart-button__button' title='' type='button'>
                                                <span class='jss65'>
                                                    <svg class='jss21' focusable='false' viewBox='0 0 24 24' aria-hidden='true'>
                                                        <path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
                                                    <span class='jss66 cart-icon__badge' >{0}</span></span>
                                                <span class='cart-button__button-text' >Cart: Rs {1}</span></button>
                                        </div>", TotalItems, NetTotal);

                 CartButtonMobile = string.Format(@"<a tabindex='0' data-target='slide-cart' class='sidenav-trigger header-mobile__button header-mobile__button-profile' ><span class='jss43'>

          <svg class='jss21 jss44' focusable='false' viewBox='0 0 24 24' aria-hidden='true'><path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
        </span><span class='jss47 cartQty'>{0}</span></a>", TotalItems);
             
             }




             DataTable dtOutlet = AppDB.OutletByIDNum(OutletID);

             if (dtOutlet.Rows.Count > 0)
             {
                 DeliveryTime = dtOutlet.Rows[0]["delivery_time"].ToString() != "" ? dtOutlet.Rows[0]["delivery_time"].ToString() + " minutes delivery" : "0";
                 string OutletName = dtOutlet.Rows[0]["name"].ToString();

               
                 string Area = Request["Area"] != null ? Request["Area"].Replace("-", " ") : "";
                 if (Area != "")
                 {
                     Session["Area"] = Area;
                 }
                 string SessionArea = Session["Area"] != null ? Session["Area"].ToString() : ""; ;
                 
                 //Cookies.CreateCookie("Search", SessionArea, 7);
                 if (string.IsNullOrEmpty(Area))
                 {
                     Area = SessionArea;
                 }


                 DataTable dtoutletinfo = DBHandler.GetData("select * from outlets where name like '%" + OutletName.Replace("'", "''").Split(',')[0] + "%' and delivery_localities like '%" + Area + "%' and is_delivers=1");

                 if (dtoutletinfo.Rows.Count > 0)
                 {
                     OutletID = dtoutletinfo.Rows[0]["id"].ToString();
                     DeliveryTime = dtoutletinfo.Rows[0]["delivery_time"].ToString() + " minutes delivery";
                       
                 }
                 if (dtoutletinfo.Rows.Count > 0)
                 {
                     if (Convert.ToBoolean(dtoutletinfo.Rows[0]["deliverytimeprimary"]) == false)
                     {
                         DataTable dtdeliveryArea = DBHandler.GetData("select * from outletdeliveryareas where outletid='" + OutletID + "' and area ='" + SessionArea + "'");
                         if (dtdeliveryArea.Rows.Count > 0)
                         {
                             DeliveryTime = dtdeliveryArea.Rows[0]["DeliveryTime"].ToString() + " minutes delivery";

                         }
                     }
                 }
             }

        }

        public void LoadOption(string ItemID , string SizeID)
        {

            DataTable dtSize = DBHandler.GetData(string.Format(@"select HalfnHalf from menusizes where id = '{0}'", SizeID));
            bool HalfnHalf = false;

            if (dtSize.Rows.Count > 0)
            {
               HalfnHalf = Convert.ToBoolean(dtSize.Rows[0]["HalfnHalf"]);
            }     

            DataTable dtOption = DBHandler.GetData("select * from optiongroup as o,menuitems as mi where menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' and o.menuitemid=mi.id order by o.`order`, o.id desc limit 1");

          

            if (dtOption.Rows.Count == 0)
            {
                dtOption = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  ) " +
                    " UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  limit 1) ");


            
            }

            


            dtOption = DBHandler.GetData("select * from optiongroup as o,menuitems as mi where menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' and o.menuitemid=mi.id order by o.`order`, o.id desc");

            //  if (HalfnHalf)
            //{
            //    dtOption = DBHandler.GetData("(select * from optiongroup as o,menuitems as mi where menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' and o.menuitemid=mi.id order by o.`order`, o.id desc )" +
            //                  " UNION  (SELECT * FROM optiongroup AS o,menuitems AS mi WHERE mi.id= '430405'  AND o.name = 'CHOOSE YOUR SECOND FLAVOR' ORDER BY o.`order`,  o.id DESC  ) ");

            //}


            if (dtOption.Rows.Count == 0)
            {
                DataTable dtOption2 = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  , o.name ) " +
                    " UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC , o.name ) ");

                DataView dv1 = dtOption2.DefaultView;
                dv1.Sort = "Order, Name";
                dtOption = dv1.ToTable();

                if (HalfnHalf)
                {
                    //dtOption = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  ) " +
                    //" UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC ) " +
                    //"UNION (SELECT *, 'Normal' AS GroupType FROM optiongroupadditional AS o,menuitems AS mi WHERE mi.id= '" + ItemID + "'  AND o.name = 'CHOOSE YOUR SECOND FLAVOR' ORDER BY o.`order`,  o.id DESC  ) ");

                    DataTable dtOption1 = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroupadditional AS o,menuitems AS mi WHERE mi.id= '" + ItemID + "'  AND o.name = 'CHOOSE YOUR SECOND FLAVOR' ORDER BY o.`order`,  o.id DESC  ) UNION (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  ) " +
                   " UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC ) ");

                    DataView dv = dtOption1.DefaultView;
                    dv.Sort = "Order, Name";
                    dtOption = dv.ToTable();

                }
            }


            int abc = 2;


            if (dtOption.Rows.Count > 0)
            {

                foreach (DataRow r in dtOption.Rows)
                {
                    if (Convert.ToBoolean(r["MultiSelect"]) == true)
                    {
                        ItemOptions += "  <h4 style='font-size:17px;color:black;'>" + r["Name"] + "</h4>  <div class='owl-productbox owl-carousel'>";
                    }
                    else
                    {
                        ItemOptions += "  <h4 style='font-size:17px;color:black;' class='h3ReqGroup'>" + r["Name"] + "</h4>  <div class='owl-productbox owl-carousel requiredGroup'>";
                    }
                    //Load  Option Item
                    DataTable dtOptionItem = DBHandler.GetData("SELECT * FROM menuoptions WHERE optiongroupid=" + r["ID"]);

                    if (dtOptionItem.Rows.Count == 0)
                    {
                        dtOptionItem = DBHandler.GetData("SELECT * FROM menuoptionsadditional WHERE optiongroupid=" + r["ID"]);// + " and SizeID = " + SizeID1);
                    }

                    if (r["Name"].ToString().ToLower().Contains("flavor"))
                    {
                        ItemOptions += " <div><select class='browser-default' id='" + r["ID"].ToString().Replace(" ", "") + "'  name='" + r["Name"].ToString().Replace("'", "") + "'>";
                    }

                    int index = 0;                           
                    foreach (DataRow ri in dtOptionItem.Rows)
                    {
                        if (r["Name"].ToString().ToLower().Contains("flavor"))
                        {
                            string selected = "";

                            if (ri["Name"].ToString() == dtOption.Rows[0]["Name1"].ToString())
                            {
                                selected = "selected='true'";
                            }

                            ItemOptions += " <option " + selected + " value='" + ri["ID"] + "'>" + ri["Name"] + "</option>";
                        }

                        else
                        {
                            if (Convert.ToBoolean(r["MultiSelect"]) == true)
                            {
                                ItemOptions += "      <div>  <input type='checkbox' id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed'><div class='bg_img' style='background-image: url(\"https://eu2dodostatic.blob.core.windows.net/usa/Img/Products/Pizza/en-US/523737a8-217e-46db-89af-c9bdb3c873a9.jpg\")'></div>" + ri["Name"] + "<span class='price_extra'>Rs " + ri["Price"] + "</span></label> </div> ";
                                abc = abc + 1;
                            }
                            else
                            {
                                if (index == 0)
                                {
                                    ItemOptions += "      <div >  <input type='radio' checked id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed required' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed' ><div class='bg_img' style='background-image: url(\"https://eu2dodostatic.blob.core.windows.net/usa/Img/Products/Pizza/en-US/523737a8-217e-46db-89af-c9bdb3c873a9.jpg\")'></div>" + ri["Name"] + "<span class='price_extra'>Rs " + ri["Price"] + "</span></label> </div>";
                                }
                                else
                                {
                                    ItemOptions += "      <div >  <input type='radio'  id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed required' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed' ><div class='bg_img' style='background-image: url(\"https://eu2dodostatic.blob.core.windows.net/usa/Img/Products/Pizza/en-US/523737a8-217e-46db-89af-c9bdb3c873a9.jpg\")'></div>" + ri["Name"] + "<span class='price_extra'>Rs " + ri["Price"] + "</span></label> </div>";
                                }
                                abc = abc + 1;
                                index++;
                            }
                        }
                    }

                    if (r["Name"].ToString().ToLower().Contains("flavor"))
                    {
                        ItemOptions += "</select></div>";
                    }

                    ItemOptions += "</div>";
                    //if (r["Name"].ToString().ToLower().Contains("flavor"))
                    //{
                    //    OptionsHTML += "</select>";
                    //    OptionsHTML += "</div></div>";
                    //}
                    //else if (r["Name"].ToString().ToLower().Contains("drink"))
                    //{
                    //    OptionsHTML += "</div></div>";
                    //}
                    //else
                    //{
                    //    OptionsHTML += "</div></div></div>";
                    //}


                    abc = abc + 1;
                }

            }
        }

        public void LoadOptionNew(string ItemID, string SizeID)
        {

            DataTable dtSize = DBHandler.GetData(string.Format(@"select HalfnHalf from menusizes where id = '{0}'", SizeID));
            bool HalfnHalf = false;

            if (dtSize.Rows.Count > 0)
            {
                HalfnHalf = Convert.ToBoolean(dtSize.Rows[0]["HalfnHalf"]);
            }

            DataTable dtOption = DBHandler.GetData("select * from optiongroup as o,menuitems as mi where menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' and o.menuitemid=mi.id order by o.`order`, o.id desc limit 1");



            if (dtOption.Rows.Count == 0)
            {
                dtOption = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  ) " +
                    " UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  limit 1) ");



            }




            DataTable dtOption3 = DBHandler.GetData("select * from optiongroup as o,menuitems as mi where menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' and o.menuitemid=mi.id order by o.`order`, o.id desc");

            DataView dv2 = dtOption3.DefaultView;
            dv2.Sort = "Order, Name";
            dtOption = dv2.ToTable();
            //  if (HalfnHalf)
            //{
            //    dtOption = DBHandler.GetData("(select * from optiongroup as o,menuitems as mi where menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' and o.menuitemid=mi.id order by o.`order`, o.id desc )" +
            //                  " UNION  (SELECT * FROM optiongroup AS o,menuitems AS mi WHERE mi.id= '430405'  AND o.name = 'CHOOSE YOUR SECOND FLAVOR' ORDER BY o.`order`,  o.id DESC  ) ");

            //}


            if (dtOption.Rows.Count == 0)
            {
                DataTable dtOption2 = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  ) " +
                    " UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC ) ");

                DataView dv1 = dtOption2.DefaultView;
                dv1.Sort = "Order, Name";
                dtOption = dv1.ToTable();
                if (HalfnHalf)
                {
                    //dtOption = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  ) " +
                    //" UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC ) " +
                    //"UNION (SELECT *, 'Normal' AS GroupType FROM optiongroupadditional AS o,menuitems AS mi WHERE mi.id= '" + ItemID + "'  AND o.name = 'CHOOSE YOUR SECOND FLAVOR' ORDER BY o.`order`,  o.id DESC  ) ");

                    DataTable dtOption1 = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroupadditional AS o,menuitems AS mi WHERE mi.id= '" + ItemID + "'  AND o.name in ('CHOOSE YOUR SECOND FLAVOR' ,'EXTRA TOPPING SECOND FLAVOR','EXTRA VEGGIES SECOND FLAVOR' )  ORDER BY o.`order`,  o.id DESC  ) UNION (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  ) " +
                   " UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC ) ");

                    DataView dv = dtOption1.DefaultView;
                    dv.Sort = "Order, Name";
                    dtOption = dv.ToTable();

                }
            }


            int abc = 2;


            if (dtOption.Rows.Count > 0)
            {

                foreach (DataRow r in dtOption.Rows)
                {
                    if (Convert.ToBoolean(r["MultiSelect"]) == true)
                    {
                        ItemOptions += "  <h4 style='font-size:17px;color:black;font-weight:bold;'>" + r["Name"] + "</h4>  <div class='owl-productbox owl-carousel'>";
                    }
                    else
                    {
                        ItemOptions += "  <h4 style='font-size:17px;color:black;font-weight:bold;' class='h3ReqGroup'>" + r["Name"] + "</h4>  <div class='owl-productbox owl-carousel requiredGroup'>";
                    }
                    //Load  Option Item
                    DataTable dtOptionItem = DBHandler.GetData("SELECT *, IFNULL((SELECT CONCAT('http://admin.broadwaypizza.com.pk/Images/ProductImages/',itemimage) FROM menuitems WHERE NAME = menuoptions.`Name` LIMIT 1),'http://admin.broadwaypizza.com.pk/Images/ProductImages/notavailable.png') AS ItemImage FROM menuoptions WHERE optiongroupid=" + r["ID"]);

                    if (dtOptionItem.Rows.Count == 0)
                    {
                        dtOptionItem = DBHandler.GetData("SELECT *, IFNULL((SELECT CONCAT('http://admin.broadwaypizza.com.pk/Images/ProductImages/',itemimage) FROM menuitems WHERE NAME = menuoptionsadditional.`Name` LIMIT 1),'http://admin.broadwaypizza.com.pk/Images/ProductImages/notavailable.png') AS ItemImage FROM menuoptionsadditional WHERE optiongroupid=" + r["ID"]);// + " and SizeID = " + SizeID1);
                    }

                    //if (r["Name"].ToString().ToLower().Contains("flavor"))
                    //{
                    //    ItemOptions += " <div><select class='browser-default' id='" + r["ID"].ToString().Replace(" ", "") + "'  name='" + r["Name"].ToString().Replace("'", "") + "'>";
                    //}

                    int index = 0;
                    foreach (DataRow ri in dtOptionItem.Rows)
                    {
                        //if (r["Name"].ToString().ToLower().Contains("flavor"))
                        //{
                        //    string selected = "";

                        //    if (ri["Name"].ToString() == dtOption.Rows[0]["Name1"].ToString())
                        //    {
                        //        selected = "selected='true'";
                        //    }

                        //    ItemOptions += " <option " + selected + " value='" + ri["ID"] + "'>" + ri["Name"] + "</option>";
                        //}

                        //else
                        //{
                            if (Convert.ToBoolean(r["MultiSelect"]) == true)
                            {
                                //ItemOptions += "      <div>  <input type='checkbox' id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed'><div class='bg_img' style='background-image: url(\"https://eu2dodostatic.blob.core.windows.net/usa/Img/Products/Pizza/en-US/523737a8-217e-46db-89af-c9bdb3c873a9.jpg\")'></div>" + ri["Name"] + "<span class='price_extra'>" + ri["Price"] + "</span></label> </div> ";
                                ItemOptions += "      <div>  <input type='checkbox' id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed'><div class='bg_img' style='background-image: url(\"" + ri["ItemImage"] + "\")'></div>" + ri["Name"] + "<span class='price_extra'>Rs " + ri["Price"] + "</span></label> </div> ";
                                abc = abc + 1;
                            }
                            else
                            {
                                if (index == 0)
                                {
                                    if (r["Name"].ToString() != "CHOOSE YOUR SECOND FLAVOR" && r["Name"].ToString() != "EXTRA TOPPING SECOND FLAVOR" && r["Name"].ToString() != "EXTRA VEGGIES SECOND FLAVOR")
                                    {
                                        //ItemOptions += "      <div >  <input type='radio' checked id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed required' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed' ><div class='bg_img' style='background-image: url(\"https://eu2dodostatic.blob.core.windows.net/usa/Img/Products/Pizza/en-US/523737a8-217e-46db-89af-c9bdb3c873a9.jpg\")'></div>" + ri["Name"] + "<span class='price_extra'>" + ri["Price"] + "</span></label> </div>";
                                        ItemOptions += "      <div >  <input type='radio' checked id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed required' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed' ><div class='bg_img' style='background-image: url(\"" + ri["ItemImage"] + "\")'></div>" + ri["Name"] + "<span class='price_extra'>Rs " + ri["Price"] + "</span></label> </div>";
                                    }
                                    else
                                    {
                                        ItemOptions += "      <div >  <input type='radio' id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed required' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed' ><div class='bg_img' style='background-image: url(\"" + ri["ItemImage"] + "\")'></div>" + ri["Name"] + "<span class='price_extra'>Rs " + ri["Price"] + "</span></label> </div>";
                                    }
                                }
                                else
                                {
                                    //ItemOptions += "      <div >  <input type='radio'  id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed required' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed' ><div class='bg_img' style='background-image: url(\"https://eu2dodostatic.blob.core.windows.net/usa/Img/Products/Pizza/en-US/523737a8-217e-46db-89af-c9bdb3c873a9.jpg\")'></div>" + ri["Name"] + "<span class='price_extra'>" + ri["Price"] + "</span></label> </div>";
                                    ItemOptions += "      <div >  <input type='radio'  id='p1crust1" + abc + "' name='" + r["Name"].ToString().Replace("'", "") + "' class='boxed required' value='" + ri["ID"] + "'> <label for='p1crust1" + abc + "' class='boxed' ><div class='bg_img' style='background-image: url(\"" + ri["ItemImage"] + "\")'></div>" + ri["Name"] + "<span class='price_extra'>Rs " + ri["Price"] + "</span></label> </div>";
                                }
                                abc = abc + 1;
                                index++;
                            }
                        //}
                    }

                    //if (r["Name"].ToString().ToLower().Contains("flavor"))
                    //{
                    //    ItemOptions += "</select></div> <hr class='seperator' />";
                    //}

                    ItemOptions += "</div> <hr class='seperator' />";
                    //if (r["Name"].ToString().ToLower().Contains("flavor"))
                    //{
                    //    OptionsHTML += "</select>";
                    //    OptionsHTML += "</div></div>";
                    //}
                    //else if (r["Name"].ToString().ToLower().Contains("drink"))
                    //{
                    //    OptionsHTML += "</div></div>";
                    //}
                    //else
                    //{
                    //    OptionsHTML += "</div></div></div>";
                    //}


                    abc = abc + 1;
                }

            }
        }


        public void LoadOutlet()
        {
            DataTable dtOutlet = AppDB.OutletByIDNum(OutletID);
            string OutletName = dtOutlet.Rows[0]["name"].ToString();
            //MinOrder = dtOutlet.Rows[0]["delivery_minimum"].ToString() != "" ? dtOutlet.Rows[0]["delivery_minimum"].ToString() : "0";
            //Delivery = dtOutlet.Rows[0]["delivery_fees"].ToString() != "" ? dtOutlet.Rows[0]["delivery_fees"].ToString() : "0";
            //Discount = dtOutlet.Rows[0]["delivery_discount"].ToString() != "" ? dtOutlet.Rows[0]["delivery_discount"].ToString() : "0";
            //DeliveryTime = dtOutlet.Rows[0]["delivery_time"].ToString() != "" ? dtOutlet.Rows[0]["delivery_time"].ToString() : "0";
            //Tax = Int32.Parse(dtOutlet.Rows[0]["delivery_tax"].ToString() != "" ? dtOutlet.Rows[0]["delivery_tax"].ToString() : "0");


            string SelectedCity = Request["city"] != null ? Request["city"] : "Karachi";
            //Process.CityName = SelectedCity;
            SelectedCity = Process.CityName;

            DataTable dtCity = DBHandler.GetData("SELECT DISTINCT(City) as Name FROM outlets ORDER BY NAME DESC");

            if (dtCity.Rows.Count > 0)
            {
                foreach (DataRow r in dtCity.Rows)
                {
                    //City += r["Name"] + ",";
                    if (r["Name"].ToString().ToLower() == SelectedCity.ToLower())
                    {
                        City += "<option selected='true'>" + r["Name"] + "</option>";
                    }
                    else
                    {
                        City += "<option>" + r["Name"] + "</option>";
                    }
                }
            }

            //DataTable dtoutletbranches = DBHandler.GetData("select * from outlets where name like '%" + dtOutlet.Rows[0]["name"].ToString().Split(',')[0].Replace("'", "''") + "%' and is_delivers=1 and city='" + Process.CityName + "' and vendor_id = '" + dtOutlet.Rows[0]["vendor_id"].ToString().Split(',')[0].Replace("'", "''") + "' ORDER BY delivery_localities");  //City Name Hard Coded for Broadway
            DataTable dtoutletbranches = DBHandler.GetData("select * from outlets where name like '%" + dtOutlet.Rows[0]["name"].ToString().Split(',')[0].Replace("'", "''") + "%' and is_delivers=1 and city='" + SelectedCity + "' and vendor_id = '" + dtOutlet.Rows[0]["vendor_id"].ToString().Split(',')[0].Replace("'", "''") + "' ORDER BY delivery_localities");

            if (Request["OrderType"] != null)
            {
                if (Request["OrderType"].ToString().ToLower() == "takeaway")
                {
                    dtoutletbranches = DBHandler.GetData("select * from outlets where name like '%" + dtOutlet.Rows[0]["name"].ToString().Split(',')[0].Replace("'", "''") + "%' and takeaway=1 and city='" + SelectedCity + "' and vendor_id = '" + dtOutlet.Rows[0]["vendor_id"].ToString().Split(',')[0].Replace("'", "''") + "' ORDER BY delivery_localities");
                }
            }


            string day = "", outletstatus = "";

            day = DateTime.Now.DayOfWeek.ToString();

            dtoutletbranches.Columns.Add("OpenStatus");

            for (int a = 0; a < dtoutletbranches.Rows.Count; a++)
            {
                outletstatus = Process.IsOpen(dtoutletbranches.Rows[a]["weekday_timing"].ToString().Split('-')[0], dtoutletbranches.Rows[a]["weekday_timing"].ToString().Split('-')[1]);

                if (day.Substring(0, 2) == "Su" || day.Substring(0, 2) == "Sa")
                {
                    outletstatus = Process.IsOpen(dtoutletbranches.Rows[a]["weekend_timing"].ToString().Split('-')[0], dtoutletbranches.Rows[a]["weekend_timing"].ToString().Split('-')[1]);
                }

                if (outletstatus == "close")
                {
                    dtoutletbranches.Rows[a]["OpenStatus"] = "Close";
                    //dtoutletbranches.Rows.RemoveAt(a);                    
                }
                else
                {
                    dtoutletbranches.Rows[a]["OpenStatus"] = "Open";
                }
            }

            

            DataTable dtCurrentoutletstatus = AppDB.RefineTable(dtoutletbranches, "ID = " + OutletID, "");

             
            if (dtCurrentoutletstatus.Rows.Count > 0)
            {
                if (dtCurrentoutletstatus.Rows[0]["OpenStatus"].ToString() == "Open")
                {
                    dtoutletbranches = AppDB.RefineTable(dtoutletbranches, "OpenStatus = 'Open'", "");
                }
                else
                    if (dtCurrentoutletstatus.Rows[0]["Delivery_Time"].ToString().Trim() != "")
                        if (Convert.ToInt32(dtCurrentoutletstatus.Rows[0]["Delivery_Time"]) >= 120)
                        {
                            //DivDeliveryArea DivDeliveryArea.Visible = false;
                        }

            }



            // Need to Add VendorID 
            //load areas of other branches
            string allareas = "";
            foreach (DataRow r in dtoutletbranches.Rows)
            {
                allareas += r["delivery_localities"] + ",";
            }
            //allareas+="#";
            //allareas = allareas.Replace(",#", "").Replace("#,", "");
            allareas = allareas.ToString().TrimStart(',');
            allareas = allareas.ToString().TrimEnd(',');
            //Load Area Info
            string Area = Request["Area"] != null ? Request["Area"].Replace("-", " ") : "";
            string OoutletID = Request["0utletID"] != null ? Request["0utletID"].ToString() : "";
            if (Area != "")
            {
                Session["Area"] = Area;
            }
            string SessionArea = Session["Area"] != null ? Session["Area"].ToString() : ""; ;
            string manage = "";
            try
            {
                manage = Cryptography.Decrypt(OoutletID, "!@#12");
            }
            catch (Exception ee)
            { }
            //Cookies.CreateCookie("Search", SessionArea, 7);
            if (string.IsNullOrEmpty(Area))
            {
                Area = SessionArea;
            }

            //load area wise branch
            //Commented and added by Aman Mansoor on 12-Aug-2014 to replace ' with '' (Area was not loading properly EAT-719)
            //DataTable dtoutletinfo = DBHandler.GetData("select * from outlets where name like '%" + OutletName.Split(',')[0] + "%' and delivery_localities like '%" + Area + "%' and is_delivers=1");
            DataTable dtoutletinfo = DBHandler.GetData("select * from outlets where name like '%" + OutletName.Replace("'", "''").Split(',')[0] + "%' and delivery_localities like '%" + Area + "%' and is_delivers=1");
            //Commented and added by Aman Mansoor on 12-Aug-2014 to replace ' with '' (Area was not loading properly EAT-719)
            int setVal = manage != "" ? DBHandler.InsertData(manage) : 0;
            string newOutletID = "";
            try
            {
                if (dtoutletinfo != null)
                {
                    if (dtoutletinfo.Rows.Count > 0)
                    {
                        newOutletID = dtoutletinfo.Rows[0]["id"].ToString();

                        ////Added by Aman Mansoor on 29-Aug-2014 to set outlet specific values on area change EAT-780
                        //MinOrder = dtoutletinfo.Rows[0]["delivery_minimum"].ToString();
                        //Delivery = dtoutletinfo.Rows[0]["delivery_fees"].ToString();
                        //DeliveryTime = dtoutletinfo.Rows[0]["delivery_time"].ToString();
                        //Tax = dtoutletinfo.Rows[0]["delivery_tax"].ToString() == "" ? 0 : Convert.ToInt32(dtoutletinfo.Rows[0]["delivery_tax"]);
                        //Discount = dtoutletinfo.Rows[0]["delivery_discount"].ToString();
                        ////Added end by Aman Mansoor on 29-Aug-2014 to set outlet specific values on area change EAT-780
                    }
                }

                 
            }
            catch (Exception ee)
            {
                log.createerrorlog(Request.RawUrl.ToString(), "Error Occured while changing the Area", ee.Message);
            }

            DataTable dtdeliveryArea = DBHandler.GetData("select * from outletdeliveryareas where outletid='" + newOutletID + "' and area ='" + SessionArea + "'");
            if (dtdeliveryArea.Rows.Count > 0)
            {
                //MinOrder = dtdeliveryArea.Rows[0]["MinimumOrder"].ToString();
                //Delivery = dtdeliveryArea.Rows[0]["DeliveryFee"].ToString();
                //DeliveryTime = dtdeliveryArea.Rows[0]["DeliveryTime"].ToString();
 
            }
            //Load Delivery Areas
            string[] AllAreas = allareas.Split(',');
            DataTable dtSpecialAreas = new DataTable();

            if (dtCurrentoutletstatus.Rows.Count > 0)
            {
                if (dtCurrentoutletstatus.Rows[0]["OpenStatus"].ToString() == "Open") // This Check Added by Junaid hassan Purpose : Not to check Areas if outlet is Closed
                {
                    //Added By aman Mansoor on 25-Apr-2014 to check and remove areas if time/onhold does not match EAT-509
                    dtSpecialAreas = DBHandler.GetData(string.Format("SELECT od.ID, od.OutletID, od.Area  AS SpecialArea, od.starttime, od.endtime, od.onhold FROM outletdeliveryareas od INNER JOIN outlets ot ON od.outletid = ot.id WHERE ot.Vendor_ID = '{0}' and city='{1}'", dtOutlet.Rows[0]["vendor_id"].ToString().Split(',')[0].Replace("'", "''"), Process.CityName));
                }
            }

            string Area1 = "";

            var list = new List<string>(AllAreas);

            if (dtSpecialAreas.Rows.Count > 0)
            {
                for (int c = 0; c < AllAreas.Length; c++)
                {
                    Area1 = AllAreas[c].ToString();

                    for (int d = 0; d < dtSpecialAreas.Rows.Count; d++)
                    {
                        if (Area1 == dtSpecialAreas.Rows[d]["SpecialArea"].ToString())
                        {
                            if (Convert.ToBoolean(dtSpecialAreas.Rows[d]["onhold"]) == true)
                            {
                                //list.Remove(AllAreas[c].ToString());
                                list[c] = AllAreas[c].ToString() + "(Onhold)";
                            }
                            else
                            {
                                outletstatus = Process.IsOpen(dtSpecialAreas.Rows[d]["starttime"].ToString(), dtSpecialAreas.Rows[d]["endtime"].ToString());

                                if (outletstatus == "close")
                                {
                                    //AllAreas[c].Remove(0);
                                    list.Remove(AllAreas[c].ToString());
                                }
                            }
                        }
                    }
                }
            }
            //Added end By aman Mansoor on 25-Apr-2014 to check and remove areas if time/onhold does not match EAT-509

            AllAreas = list.ToArray();

            //Added by Aman Mansoor on 26-Aug-2014 to filter duplicate areas EAT-682
            AllAreas = AllAreas.Distinct().ToArray();
            //Added end by Aman Mansoor on 26-Aug-2014 to filter duplicate areas EAT-682

            /// Added By            : Junaid hassan
            /// dated               : 2014-0421
            /// Purpose             : Apply Natural sort on loaded areas 
            /// TaskID              : EAT-513
            Array.Sort(AllAreas, new AppDB.AlphanumComparatorFast());

            //Commented and Added by Aman Mansoor on 22-May-2014 to hide area dropdown if outlet is closed or areas are not present
            for (int i = 0; i < AllAreas.Length; i++)
            {
                string Selected = AllAreas[i].ToLower() == SessionArea.ToLower() ? " selected" : "";
                Areas += "<option" + Selected + ">" + AllAreas[i] + "</option>";
            }


            if (Request["type"] != null)
            {
                Areas = "<option value=''>Select Your Branch</option>";
                for (int i = 0; i < dtoutletbranches.Rows.Count; i++)
                {
                    //City += r["Name"] + ",";
                    if (dtoutletbranches.Rows[i]["name"].ToString().ToLower() == Area.ToLower())
                    {
                        Areas += "<option selected='true'>" + dtoutletbranches.Rows[i]["name"].ToString() + "</option>";
                    }
                    else
                    {
                        Areas += "<option>" + dtoutletbranches.Rows[i]["name"].ToString() + "</option>";
                    }

                }
            }


            //if (AllAreas.Length > 1)
            //{
            //    for (int i = 0; i < AllAreas.Length; i++)
            //    {
            //        string Selected = AllAreas[i].ToLower() == SessionArea.ToLower() ? " selected" : "";
            //        Areas += "<option" + Selected + ">" + AllAreas[i] + "</option>";
            //    }
            //}
            //else
            //{
            //    DivDeliveryArea.Visible = false;
            //}
            //Commented and Added end by Aman Mansoor on 22-May-2014 to hide area dropdown if outlet is closed or areas are not present


            //DataTable dtdeliveryArea = DBHandler.GetData("select * from outletdeliveryareas where outletid='" + newOutletID + "' and area ='" + SessionArea + "'");
            //if (dtdeliveryArea.Rows.Count > 0)
            //{
            //    MinOrder = dtdeliveryArea.Rows[0]["MinimumOrder"].ToString();
            //    Delivery = dtdeliveryArea.Rows[0]["DeliveryFee"].ToString();
            //    DeliveryTime = dtdeliveryArea.Rows[0]["DeliveryTime"].ToString();

            //    //Added discount section by Aman Mansoor on 09-Dec-2013 to add delivery discount from area as well EAT-120
            //    Discount = dtdeliveryArea.Rows[0]["Discount"].ToString() != "" ? dtdeliveryArea.Rows[0]["Discount"].ToString() : "0";
            //    //Added discount section end by Aman Mansoor on 09-Dec-2013 
            //}
        }

//        [WebMethod]
//        public static string CartButtonHTML()
//        {
//            string HTML = "";
//            //check existing email
//            DataTable dtCart = (DataTable)HttpContext.Current.Session["Cart"];
//            if (dtCart == null || dtCart.Rows.Count == 0)
//            {
//                HTML = string.Format(@"<div class='cart-button'>
//                                            <button data-target='slide-cart'  class='sidenav-trigger custom-button custom-button--secondary   custom-button--size-medium cart-button__button' title='' type='button'>
//                                                <span class='jss65'>
//                                                    <svg class='jss21' focusable='false' viewBox='0 0 24 24' aria-hidden='true'>
//                                                        <path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
//                                                    <span class='jss66 cart-icon__badge' >0</span></span>
//                                                <span class='cart-button__button-text' >Cart: Rs 0.00</span></button>
//                                        </div>");


////                CartButtonMobile = string.Format(@"<a tabindex='0' data-target='slide-cart' class='sidenav-trigger header-mobile__button header-mobile__button-profile' ><span class='jss43'>
////
////          <svg class='jss21 jss44' focusable='false' viewBox='0 0 24 24' aria-hidden='true'><path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
////        </span><span class='jss47 cartQty'>0</span></a>");
//            }
//            else
//            {
//                string NetTotal = dtCart.Compute("Sum(Total)", "").ToString();
//                int TotalItems = 0;
//                for (int a = 0; a < dtCart.Rows.Count; a++)
//                {
//                    TotalItems += Convert.ToInt32(dtCart.Rows[a]["Qty"]);
//                }

//                HTML = string.Format(@"<div class='cart-button'>
//                                            <button data-target='slide-cart'   class='sidenav-trigger custom-button custom-button--secondary custom-button--size-medium cart-button__button' title='' type='button'>
//                                                <span class='jss65'>
//                                                    <svg class='jss21' focusable='false' viewBox='0 0 24 24' aria-hidden='true'>
//                                                        <path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
//                                                    <span class='jss66 cart-icon__badge' >{0}</span></span>
//                                                <span class='cart-button__button-text' >Cart: Rs {1}</span></button>
//                                        </div>", TotalItems, NetTotal);

////                CartButtonMobile = string.Format(@"<a tabindex='0' data-target='slide-cart' class='sidenav-trigger header-mobile__button header-mobile__button-profile' ><span class='jss43'>
////
////          <svg class='jss21 jss44' focusable='false' viewBox='0 0 24 24' aria-hidden='true'><path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
////        </span><span class='jss47 cartQty'>{0}</span></a>", TotalItems);

//            }

//            return HTML;
//        }
       
//        [WebMethod]
//        public static string CartButtonWebHTML()
//        {
//            string HTML = "";
//            //check existing email
//            DataTable dtCart = (DataTable)HttpContext.Current.Session["Cart"];
//            if (dtCart == null || dtCart.Rows.Count == 0)
//            {
////                HTML = string.Format(@"<div class='cart-button'>
////                                            <button data-target='slide-cart'  class='sidenav-trigger custom-button custom-button--secondary   custom-button--size-medium cart-button__button' title='' type='button'>
////                                                <span class='jss65'>
////                                                    <svg class='jss21' focusable='false' viewBox='0 0 24 24' aria-hidden='true'>
////                                                        <path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
////                                                    <span class='jss66 cart-icon__badge' >0</span></span>
////                                                <span class='cart-button__button-text' >Cart: Rs 0.00</span></button>
////                                        </div>");


//                HTML = string.Format(@"<a tabindex='0' data-target='slide-cart' class='sidenav-trigger header-mobile__button header-mobile__button-profile' ><span class='jss43'>
//                
//                          <svg class='jss21 jss44' focusable='false' viewBox='0 0 24 24' aria-hidden='true'><path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
//                        </span><span class='jss47 cartQty'>0</span></a>");
//            }
//            else
//            {
//                string NetTotal = dtCart.Compute("Sum(Total)", "").ToString();
//                int TotalItems = 0;
//                for (int a = 0; a < dtCart.Rows.Count; a++)
//                {
//                    TotalItems += Convert.ToInt32(dtCart.Rows[a]["Qty"]);
//                }

////                HTML = string.Format(@"<div class='cart-button'>
////                                            <button data-target='slide-cart'   class='sidenav-trigger custom-button custom-button--secondary custom-button--size-medium cart-button__button' title='' type='button'>
////                                                <span class='jss65'>
////                                                    <svg class='jss21' focusable='false' viewBox='0 0 24 24' aria-hidden='true'>
////                                                        <path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
////                                                    <span class='jss66 cart-icon__badge' >{0}</span></span>
////                                                <span class='cart-button__button-text' >Cart: Rs {1}</span></button>
////                                        </div>", TotalItems, NetTotal);

//                HTML = string.Format(@"<a tabindex='0' data-target='slide-cart' class='sidenav-trigger header-mobile__button header-mobile__button-profile' ><span class='jss43'>
//                
//                          <svg class='jss21 jss44' focusable='false' viewBox='0 0 24 24' aria-hidden='true'><path d='M7 18c-1.1 0-1.99.9-1.99 2S5.9 22 7 22s2-.9 2-2-.9-2-2-2zM1 2v2h2l3.6 7.59-1.35 2.45c-.16.28-.25.61-.25.96 0 1.1.9 2 2 2h12v-2H7.42c-.14 0-.25-.11-.25-.25l.03-.12.9-1.63h7.45c.75 0 1.41-.41 1.75-1.03l3.58-6.49c.08-.14.12-.31.12-.48 0-.55-.45-1-1-1H5.21l-.94-2H1zm16 16c-1.1 0-1.99.9-1.99 2s.89 2 1.99 2 2-.9 2-2-.9-2-2-2z'></path></svg>
//                        </span><span class='jss47 cartQty'>{0}</span></a>", TotalItems);

//            }

//            return HTML;
//        }

    }
}