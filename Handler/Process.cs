using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; 
using System.Web.Hosting;
using Jewar;

namespace Jewar.Handler
{
    public static class Process
    {
        public static string _MetaKeyword, _MetaDesc,_MetaTitle,_CityName,_Number,_Name,_DeliverAddress;
        
        /*******Methods********/
        
        //New Refiner for with less loops
        public static string CreateRefiner(DataTable dtTable, string ColumnName, string Heading)
        {
            string strRefineList="";
            //Refine Facilities for delivery
            DataTable dtRefine=AppDB.Gettaxonomy(ColumnName);
            if (ColumnName == "facilities" || ColumnName == "tags")
            {
                dtRefine = AppDB.RefineTable(dtRefine, "alias='Delivery'");
            }
            //Write Refiners on page
            foreach (DataRow r in dtRefine.Rows)
            {
                //Load Querry Strings variables
                
                string qs_k = HttpContext.Current.Request["k"] != "" ? "?k=" + HttpContext.Current.Request["k"] : "";
                string k_s = qs_k == "" ? "?" : "&";
                string qs_r = HttpContext.Current.Request["r"] != "" ? k_s + "r=" + HttpContext.Current.Request["r"] : k_s + "r=";
                string qs_s = HttpContext.Current.Request["k"] != "" ? k_s+"s=" + HttpContext.Current.Request["s"] : "";
                string SearchType = HttpContext.Current.Request["t"] != "d" ? "delivery" : "delivery";
                
                //Refiner Info ie Value & Count
                string RValue=r["value"].ToString().Replace("'","''");
                int RefinerCount = AppDB.RefineTable(dtTable, ColumnName + " like '%" + RValue + "%'").Rows.Count;
                    
                
                //Creating Refiner Markup
                if (RefinerCount > 0)
                {
                    string Selected = HttpContext.Current.Request.RawUrl.ToLower().Replace("%20", " ").IndexOf(RValue.ToLower()) < 1 ? "" : " class=\"selected\"";
                    if (qs_r.Contains(RValue.ToLower()))
                    {
                        qs_r = qs_r.Replace("," + RValue.ToLower(), "").Replace( RValue.ToLower()+",", "").Replace("?r="+RValue.ToLower(), "").Replace(RValue.ToLower(), "");
                    }
                    else
                    {
                        qs_r += qs_r!=k_s+"r="?"," + RValue:RValue;
                    }
                    string Href = "/{city_link}/"+SearchType + qs_k.ToLower() + qs_r.ToLower() + qs_s.ToLower();
                    //create link no follow
                    string LinkRef="";
                    if (qs_k != "" || qs_s != "" || qs_r.IndexOf(",") > 0)
                    { 
                        LinkRef = " rel=\"nofollow\"";
                    }

                    //Create Link
                    if (HttpContext.Current.Request.RawUrl.ToLower().Replace("%20", " ").IndexOf(RValue.ToLower()) < 1)
                    {
                        strRefineList += "<li><a href='" + Href + "'"+LinkRef+">" + RValue + " <span>(" + RefinerCount + ")</span></a></li>";
                    }
                    else
                    {
                        strRefineList += "<li><strong>" + RValue + " <a rel=\"nofollow\" href='" + Href + "'" + LinkRef + ">clear</a></strong></li>";
                    }
                    
                }
            }
            strRefineList = strRefineList != "" ? "<li>" + Heading + "<span class='icon-caret-down pull-right' aria-hidden='true'></span></li><ul>" + strRefineList + "</ul>" : "";
            return strRefineList;
        }

        //Load All Outlet with Keyword & Refiner
        public static DataTable Loadoutlets()
        {
            //Load All outlets in City/Country
            DataTable dtoutlets = CityName == "Pakistan" ? AppDB.outlets() : AppDB.OutletByCity(CityName);
            if (dtoutlets == null)
            {
                return null;
            }
            if (dtoutlets.Rows.Count==0)
            {
                return null;
            }
            
            //Create Refiner & Search Keyword on All Records
            string k = HttpContext.Current.Request["k"] != null ?HttpContext.Current.Request["k"].ToString() : "";
            string r = HttpContext.Current.Request["r"] != null ? HttpContext.Current.Request["r"].ToString() : "";
            string s = HttpContext.Current.Request["s"] != null ? HttpContext.Current.Request["s"].ToString() : "";
            string t = HttpContext.Current.Request["t"] != null ? HttpContext.Current.Request["t"].ToString() : "";
            if (t != "")
            {
                dtoutlets = t != "d" ? AppDB.RefineTable(dtoutlets, "is_reservation=1") : AppDB.RefineTable(dtoutlets, "is_delivers=1");
            }
            if (t == "all")
            {
                dtoutlets = CityName == "Pakistan" ? AppDB.Alloutlets() : AppDB.AllOutletByCity(CityName);
            }
            
            dtoutlets = RefineFoodSearch(dtoutlets);

            //Load customer Favourite Restaurants - Yasir
            string customermobile = Jewar.CodeLibrary.Cookies.GetCookie("customerMobile").ToString();
            customermobile = customermobile.IndexOf("=") > 0 ? customermobile.Split('=')[1] : "";
            DataTable dtFavOultets = Jewar.CodeLibrary.DBHandler.GetData("SELECT id,totalorders FROM outlets AS o1,(SELECT COUNT(id) AS totalorders,outletid FROM orders WHERE customermobile='"+customermobile+"' GROUP BY outletid) AS o2 WHERE o1.id=o2.outletid") ;
            string outletids = ""; 
            foreach (DataRow r1 in dtFavOultets.Rows)
            {
                outletids += r1["id"].ToString() + ",";
            }

            //Sorting
            if (s == "discount")
            {
                //Commented and added by Aman Mansoor on 02-Dec-2013 to show open restaurants by top (EAT-99)
                //s = "reservation_discount desc";
                /// Modified By Junaid Hassan
                /// Added Rank by Desc 
                /// JIRA : EAT-388
                /// 
                s = " is_open desc, IsSponsored DESC,Favourite Desc, reservation_discount desc, outletstatus ASC, Rank Desc";
                if (t == "d")
                {
                    /// Modified By Junaid Hassan
                    /// Added Rank by Desc 
                    /// JIRA : EAT-388
                    /// 
                    //Commented and added by Aman Mansoor on 02-Dec-2013 to show open restaurants by top (EAT-99)
                    //s = "delivery_discount desc";
                    s = "is_open desc, IsSponsored DESC,Favourite Desc,delivery_discount desc, outletstatus ASC, Rank Desc ";
                }
            }
            else if (s == "rating")
            {
                //Commented and added by Aman Mansoor on 02-Dec-2013 to show open restaurants by top (EAT-99)
                //s = "rating desc,review_count desc";
                /// Modified By Junaid Hassan
                /// Added Rank by Desc 
                /// JIRA : EAT-388
                /// 
                s = "is_open desc, IsSponsored DESC,Favourite Desc,rating desc,review_count desc, outletstatus ASC, Rank Desc";
            }
            else if (s == "budget")
            {
                s = "budget";
                if (t == "d")
                {
                    //Commented and added by Aman Mansoor on 02-Dec-2013 to show open restaurants by top (EAT-99)
                    //s = "delivery_minimum";
                    
                    /// Modified By Junaid Hassan
                    /// Added Rank by Desc 
                    /// JIRA : EAT-388
                    /// 
                    s = "is_open desc, IsSponsored DESC,Favourite Desc, delivery_minimum, outletstatus ASC, Rank Desc";
                }
            }
            else if (s == "a-z")
            {
                //Commented and added by Aman Mansoor on 02-Dec-2013 to show open restaurants by top (EAT-99)
                //s = "name";
                s = "is_open desc, IsSponsored DESC,Favourite Desc,name, Rank Desc, outletstatus ASC";
            }
            else
            {
                /// Modified By Junaid Hassan
                /// Added Rank by Desc 
                /// JIRA : EAT-388
                // OLD s = "is_open desc,review_count desc,rating desc,review_count desc";
                s = "is_open desc, IsSponsored DESC,Favourite Desc, outletstatus ASC, Rank Desc";
            }
            string sortby = s;

            string[] RefinerArray = r.Split(',');
            string[] KeywordArray = k.Split(',');
            if (!dtoutlets.Columns.Contains("is_open"))
            {
                //Load Timing Column
                dtoutlets.Columns.Add("is_open");
                dtoutlets.Columns.Add("Timing");
                dtoutlets.Columns.Add("RatingValue");
                dtoutlets.Columns.Add("Favourite");
            }
            
            foreach (DataRow dtr in dtoutlets.Rows)
            {
                ///////////////////////////////////////////// START /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                //Add Favourite : Yasir
                if (outletids.Contains(dtr["id"].ToString()))
                {
                    foreach (DataRow rf in dtFavOultets.Rows)
                    {
                        if (rf["id"].ToString() == dtr["id"].ToString())
                        {
                            dtr["Favourite"] = rf["totalorders"].ToString();
                        }
                    }
                }
                
                // Modified By Junaid Hassan 
                // Purpose: Handle WeekEnd Timings as well we were not handling weekEnd Timings.

                // Commented By Junaid Hassan dtr["is_open"] = IsOpen(dtr["weekday_timing"].ToString().Split('-')[0], dtr["weekday_timing"].ToString().Split('-')[1]);
                if (dtr["ID"].ToString() == "2085")
                {
                   // string aa = dtr["break_timing"].ToString() ;
                }
                string day = DateTime.Now.DayOfWeek.ToString();
                if (day.Substring(0, 2) == "Su" || day.Substring(0, 2) == "Sa")
                {
                    dtr["is_open"] = IsOpen(dtr["weekend_timing"].ToString().Split('-')[0], dtr["weekend_timing"].ToString().Split('-')[1]);
                    dtr["Timing"] = dtr["weekend_timing"].ToString() == "1-0" ? "24 Hrs" : FormatTime(dtr["weekend_timing"].ToString().Split('-')[0].ToString()) + " - " + FormatTime(dtr["weekend_timing"].ToString().Split('-')[0].ToString());
                }
                else
                {
                    dtr["is_open"] = IsOpen(dtr["weekday_timing"].ToString().Split('-')[0], dtr["weekday_timing"].ToString().Split('-')[1]);
                    dtr["Timing"] = dtr["weekday_timing"].ToString() == "1-0" ? "24 Hrs" : FormatTime(dtr["weekday_timing"].ToString().Split('-')[0].ToString()) + " - " + FormatTime(dtr["weekday_timing"].ToString().Split('-')[0].ToString());
                }

                //Added by Yasir : To manage Outlet Break timing
                if (dtr["break_timing"].ToString() != "")
                {
                    string isBreak = IsOpen(dtr["break_timing"].ToString().Split('-')[0], dtr["break_timing"].ToString().Split('-')[1]);
                    dtr["is_open"] = isBreak == "open" ? "close" : "open";
                    
                }

                // Below lines Commented By Junaid Hassan Handling it in Above WeekEnd WeekDay logic.
                // dtr["Timing"] = dtr["weekday_timing"].ToString() == "1-0" ? "24 Hrs" : FormatTime(dtr["weekday_timing"].ToString().Split('-')[0].ToString()) + " - " + FormatTime(dtr["weekday_timing"].ToString().Split('-')[0].ToString());
                // string day = DateTime.Now.DayOfWeek.ToString();


                // Modified By Junaid Hassan 
                ///////////////////////////////////////////// END /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                string outletdays = dtr["open_days"].ToString();

                if (!outletdays.Contains(","))
                {
                    outletdays = "," + outletdays;
                }
                
                if (!outletdays.Contains(day.Substring(0, 1) + ","))
                {
                    if (!outletdays.ToString().Contains("," + day.Substring(0, 1)))
                    {
                        dtr["is_open"] = "close";
                    }
                }

                if (day.Substring(0, 1) == "T")
                {
                    outletdays = outletdays + ",";
                    if (!outletdays.Contains(day.Substring(0, 1) + ","))
                    {
                        dtr["is_open"] = "close";

                        if (day.Substring(0, 2) == "Th")
                        {
                            outletdays = outletdays + ",";
                            if (outletdays.Contains(day.Substring(0, 2) + ","))
                            {
                                dtr["is_open"] = "open";
                            }
                        }
                    }
                }

                if (day.Substring(0, 2) == "Su" || day.Substring(0, 2) == "Sa" || day.Substring(0, 2) == "Th")
                {
                    if (!outletdays.Contains(day.Substring(0, 2)))
                    {
                        dtr["is_open"] = "close";
                    }
                }

                

                DataTable dtAreaFee = AppDB.DeliveryCharges(dtr["id"].ToString(), k);
                if (dtAreaFee.Rows.Count > 0)
                {
                    dtr["delivery_fees"] = dtAreaFee.Rows[0]["DeliveryFee"].ToString();
                    dtr["delivery_minimum"] = dtAreaFee.Rows[0]["MinimumOrder"].ToString();
                }

                if (DateTime.Now.DayOfWeek.ToString().ToLower().StartsWith("s"))
                {
                    dtr["weekday_timing"] = dtr["weekend_timing"];
                }
                else
                {
                    dtr["weekday_timing"] = dtr["weekday_timing"];
                }

                string RatingValue = "Not Rated";
                decimal rating = Math.Round(decimal.Parse(dtr["rating"].ToString()));

                if (rating == 1)
                {
                    RatingValue = "Poor";
                }
                else if (rating == 2)
                {
                    RatingValue = "Fair";
                }
                else if (rating == 3)
                {
                    RatingValue = "Good";
                }
                else if (rating == 4)
                {
                    RatingValue = "Very Good";
                }
                else if (rating == 5)
                {
                    RatingValue = "Excellent";
                }

                dtr["RatingValue"] = RatingValue;
            }
            
            //Create SQL for Refiner
            r = "";
            for (int i = 0; i < RefinerArray.Length; i++)
            {
                r += "(cuisines like '%" + RefinerArray[i] + "%' OR facilities like '%" + RefinerArray[i] + "%' OR meal_times like '%" + RefinerArray[i] + "%' OR tags like '%" + RefinerArray[i] + "%') AND ";
            }
            r += "#";
            r = r.Replace("AND #", "");
            
            string AreaField = t != "d" ? "outlet_area" : "delivery_localities"; 
            string kquery = "";
            for (int i = 0; i < KeywordArray.Length; i++)
            {
                kquery += "(name like '%" + KeywordArray[i] + "%' OR " + AreaField + " like '%" + KeywordArray[i] + ",%') AND ";
            }
            kquery += "#";
            kquery = kquery.Replace("AND #", "");

            //Apply Refiners to DataTable
            k = k.Replace("'", "''");
            
            string RefineSql = kquery+" AND(" + r + ")";

            // Added by Aman Mansoor on 07-May-2014 to check if area sponsored ship exist and not expired EAT-540
            if (KeywordArray[0] != "")
            {
                //check from outletdeliveryareas if IsSponsored exist for that area and not expired
                DataTable dtSponsored =  Jewar.CodeLibrary.DBHandler.GetData(string.Format("SELECT OutletID FROM outletdeliveryareas WHERE IsSponsored = 1 and AREA LIKE '{0}' AND SponsorExpiry > CURRENT_DATE - 1", KeywordArray[0]));

                if (dtSponsored.Rows.Count > 0)
                {
                    //declare string array variable with length of datatable count
                    string[] strSponsored = new string[dtSponsored.Rows.Count];

                    //run a loop in sponsored areas dt
                    for (int b = 0; b < dtSponsored.Rows.Count; b++)
                    {
                        //add outlet id's in string array
                        strSponsored[b] = dtSponsored.Rows[b]["OutletID"].ToString();
                    }

                    //run a loop in string variables
                    for (int c = 0; c < strSponsored.Length; c++)
                    {
                        //run a nested loop in outlet dt
                        for (int d = 0; d < dtoutlets.Rows.Count; d++)
                        {
                            //check if both id's match
                            if (strSponsored[c] == dtoutlets.Rows[d]["ID"].ToString())
                            {
                                //set the issponsored in outlet dt true so it will come on top
                                dtoutlets.Rows[d]["IsSponsored"] = true;
                            }
                        }
                    }
                }
            }
            // Added end by Aman Mansoor on 07-May-2014 to check if area sponsored ship exist and not expired EAT-540

            // Added by Aman Mansoor on 13-May-2014 to reservation sponsored ship exist and not expired EAT-536
            //check if type is reservation
            if (t == "r")
            {
                //loop through the outlet datatable
                foreach (DataRow row in dtoutlets.Rows)
                {
                    if (Convert.ToDateTime(row["ResSponsorExpiry"]) > DateTime.Now.AddDays(-1))         //check if reservation sponsorship is not expired
                    {
                        //replace is sponsored with is reservation sponsored for reservation purpose and remove open desc check coz its not required in reservation case
                        sortby = sortby.Replace("is_open desc, ", "").Replace("IsSponsored", "IsResSponsored");

                        //check if reservation sponsor is true
                        if (Convert.ToBoolean(row["IsResSponsored"]) == true)
                        {
                            //set the is sponsor to true so css can apply
                            row["IsSponsored"] = true;
                        }
                    }
                    else                                                                    //if its expired then set its flag to false
                    {
                        row["IsResSponsored"] = false;
                        row["IsSponsored"] = false;
                    }
                }
            }
            // Added end by Aman Mansoor on 13-May-2014 to reservation sponsored ship exist and not expired EAT-536

            dtoutlets = dtoutlets.Select(RefineSql).Count() > 0 ? dtoutlets.Select(RefineSql, sortby).CopyToDataTable() : null;
            
            if (dtoutlets == null)
            {
               return null;
            }


           
                
            dtoutlets.Columns.Add("Sno");
            int a = 1;
            foreach (DataRow dtr in dtoutlets.Rows)
            {
                dtr["Sno"] = a;
                a++;
            }
            return dtoutlets;
        }

        //Create list of all cities
        public static string ActivecitiesList()
        {
            //list of cities with html
            string cities = "<ul class=\"thumbnails nav nav-pills nav-stacked\">";
            foreach (DataRow r in AppDB.Activecities().Rows)
            {
                string Href = HttpContext.Current.Request.Path == "/index.aspx" ? "/" + r["Name"] : "/" + r["Name"] + "/{search_type}";
                //append each city
                //cities+="<a href='"+Href+"' title='" + r["Title"] + "'><li>" + r["Title"] + "</li></a>";
                cities += "<li class=\"span3\"><a onclick=\"dataLayer.push({'event': 'select_city','city_id': '','city_name': '" + r["Name"] + "' });\" href='" + Href.ToLower() + "'>" + r["Name"] + "</a></li>";

            }
            cities += "</ul>";
            return cities;
        }

        //Load Outlet Facilities Images
        public static string LoadFacilities(string Facilities, string Myusersearch)
        {
            string output = "";
            string[] Facility = Facilities.Split(',');

            //string Myusersearch = usersearch;

            foreach (string s in Facility)
            {
                //output += "<img src='/WebAssets/images/facilities/" + s + ".jpg'  class='FacilityImg' title='" + s + "'>";
                if (Myusersearch == "delivery")
                {
                    if (s == "Credit-Cards" || s == "Delivery" || s == "Takeaway")
                    {
                        output += "<span aria-hidden=\"true\" title=\"" + s + "\" class=\"icon-" + s.ToLower().Replace(" ", "-") + "\"></span>";
                    }
                }
                else
                {
                    output += "<span aria-hidden=\"true\" title=\"" + s + "\" class=\"icon-" + s.ToLower().Replace(" ", "-") + "\"></span>";
                }
            }
            return output;
        }

        //Load Outlet Facilities Images
        public static string GetTaxSet(string Column)
        {
            string output = "";
            string[] ColumnValues = Column.Split(',');
            foreach (string s in ColumnValues)
            {
                //output += "<img src='/WebAssets/images/facilities/" + s + ".jpg'  class='FacilityImg' title='" + s + "'>";
                output += "<a href='/{city_link}/delivery?r=" + s.ToLower().Trim() + "'>" + s.Trim() + "</a>, ";
            }
            return output.TrimEnd(' ').TrimEnd(',');
        }

        //Load Outlet Facilities Images
        public static string GetTaxSetStrong(string Column)
        {
            string output = "";
            string[] ColumnValues = Column.Split(',');
            foreach (string s in ColumnValues)
            {
                //output += "<img src='/WebAssets/images/facilities/" + s + ".jpg'  class='FacilityImg' title='" + s + "'>";
                output += "<strong><a href='/{city_link}/delivery?r=" + s.ToLower().Trim() + "'>" + s.Trim() + "</a></strong>, ";
            }
            return output.TrimEnd(' ').TrimEnd(',');
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------------
        /// --- Junaid hassan 2013-11-26
        /// <summary>
        /// Check Outlet Time & Return status
        /// </summary>
        /// <param name="StartTime">outlet Start Time</param>
        /// <param name="EndTime">outlet End Time</param>
        /// <returns>string status: open or close</returns>
        public static string IsOpen(string StartTime, string EndTime)
        {
            // Aman Confirms that No Sec in DataBase only hh:mm OR hh OR H
            // Signed By Aman

            //This was added as 0 is equal to 11:59 PM 
            if (EndTime == "0")
            {
                EndTime = "23:59";
            }
            //This was added as 0 is equal to 11:59 PM 

            string outputtime = "close";
            string endtimeraw = EndTime;


            int intStartTime = Convert.ToInt32(StartTime.Replace(":", ""));
            int intEndTime = Convert.ToInt32(EndTime.Replace(":", ""));
            
            string[] strEndHr = EndTime.Split(':');

            string[] strStartHr = StartTime.Split(':');

            if (Convert.ToInt32(strEndHr[0]) == 0)
            {
                if (strEndHr.Length > 1)
                {
                    intEndTime = Convert.ToInt32("24" + strEndHr[1]);
                }
                else
                {
                    intEndTime = Convert.ToInt32("2400");
                }
            }
            else if (Convert.ToInt32(strEndHr[0]) >= 0 && Convert.ToInt32(strEndHr[0]) <= 9)
            {
                intEndTime = Convert.ToInt32(intEndTime.ToString().PadRight(3, '0')) + 2400;
            }

            
            StartTime = StartTime.Replace(":", "");
            EndTime = EndTime.Replace(":", "");

            if (strStartHr[0].Length == 1)
            {
                intStartTime = Convert.ToInt32(StartTime.ToString().PadRight(3, '0'));
            }
            else
            {
                intStartTime = Convert.ToInt32(StartTime.ToString().PadRight(4, '0'));
            }

            intEndTime = Convert.ToInt32(intEndTime.ToString().PadRight(4, '0'));

            int intNumberOfhours = 0;

            intNumberOfhours = intEndTime - intStartTime;

            if (intNumberOfhours == 0)
            {
                intNumberOfhours = 2400;
            }
            try
            {

                if (intNumberOfhours == 2400)
                {
                    outputtime = "open";
                }
                else
                {


                    DateTime dtcur1 = DateTime.Now;

                    string strCurr = dtcur1.ToString("HHmm");


                    //Include colons in time START
                    StartTime = StartTime.Replace(":", "");

                    if (StartTime.Length >= 3)
                        StartTime = StartTime.Insert(StartTime.Length - 2, ":");

                    StartTime = StartTime.IndexOf(":") > 0 ? StartTime : StartTime + ":00";


                    //EndTime = EndTime.Replace(":", "");

                    //if (EndTime.Length >= 3)
                    //    EndTime = EndTime.Insert(EndTime.Length - 2, ":");

                    //EndTime = EndTime.IndexOf(":") > 0 ? EndTime : EndTime + ":00";
                    ////Include colons in time END

                    //Apply dates 
                    DateTime dtstart = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + StartTime);
                    // DateTime dtendd = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + EndTime);
                    DateTime dtcur = DateTime.Now;
                    DateTime myDT;
                    
                    // myDT = StartTime
                    /// Adding intNumberOfhours(Hour Part) to myDT 
                    if (intNumberOfhours.ToString().Length == 3) // 600
                    {
                        myDT = dtstart.AddHours(Convert.ToInt32(intNumberOfhours.ToString().Substring(0, 1)));
                    }
                    else
                    {
                        myDT = dtstart.AddHours(Convert.ToInt32(intNumberOfhours.ToString().Substring(0, 2)));
                    }

                    /// Adding intNumberOfhours(Minute Part) to myDT 
                    if (intNumberOfhours.ToString().Length == 3)
                    {
                        // myDT = myDT.AddMinutes(Convert.ToInt32(intNumberOfhours.ToString().Substring(2, 1)));
                        // Modified by Junaid Hassan  
                        myDT = myDT.AddMinutes(Convert.ToInt32(intNumberOfhours.ToString().Substring(1, 2)));
                    }
                    else if (intNumberOfhours.ToString().Length == 4)
                    {
                        myDT = myDT.AddMinutes(Convert.ToInt32(intNumberOfhours.ToString().Substring(2, 2)));
                    }

                    if (dtcur.Hour >= 0 && dtcur.Hour <= 10 && myDT.Hour >= 0 && myDT.Hour <= 10 && dtcur.Hour <= myDT.Hour)
                    {
                        dtcur = dtcur.AddDays(1);
                    }

                    if (dtcur >= dtstart && dtcur <= myDT)
                    {
                        outputtime = "open";
                    }

                }

                /// START //////////////////////////////////////////////////////////////////////////////
                /// Commented By : Junaid Hassan
                /// Dated        : 2014-01-09
                /// Jira         : EAT-275
                //// OverRiding the Resturant Timing With broadwaypizza Timing: /////// START ------------------------------------------------------------------------
                //if (DateTime.Now.Hour > 0 && DateTime.Now.Hour < 10)
                //{
                //    outputtime = "close";
                //}
                //// OverRiding the Resturant Timing With broadwaypizza Timing: /////// END   ------------------------------------------------------------------------
                /// END  //////////////////////////////////////////////////////////////////////////////
            }





            catch (Exception ee)
            {
                outputtime = ee.Message;
            }
            return outputtime;
        }


        public static DateTime TimeRoundOff(DateTime dt)
        {


            //declare variable
            int TimeRound = 0;
            // DateTime dt = DateTime.Now;


            //check if minutes is less then 30. if yes then minus the actual minutes from 30 and add those minutes to round up time
            if (dt.Minute > 0 && dt.Minute <= 30)
            {
                TimeRound = 30 - dt.Minute;
                dt = dt.AddMinutes(TimeRound);
            }

            //check if minutes is less then 45. if yes then minus the actual minutes from 45 and add those minutes to round up time

            //check if minutes is less then 60. if yes then minus the actual minutes from 60 and add those minutes to round up time
            else if (dt.Minute > 30 && dt.Minute <= 60)
            {
                TimeRound = 60 - dt.Minute;
                dt = dt.AddMinutes(TimeRound);
            }

            return dt;


        }

        /// <summary>
        /// Added BY            : Junaid hassan
        /// Dated               : 2014-06-30
        /// Purpose             : Get Start Time and End Time of Outlet and Generate Values for Pre-Order DropDown
        /// TaskId              : EAT-586
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="intDurationInMinutes"></param>
        /// <returns></returns>
        public static string PreOrderTimeSplit(string strStartTime, string strEndTime, int intDurationInMinutes, string strIsOpen)
        {
            // Making Start Time and End Time in proper Format of HH:MM AM/PM
            
            //This code(samajh to aap gaye hongay hi) was added due to junaid bhand and after his approval
            if (strEndTime == "0")
            {
                strEndTime = "23:59";
            }
            //end This code(samajh to aap gaye hongay hi) was added due to junaid bhand and after his approval


            string [] strStartTimeHHMM = strStartTime.Split(':');
            string [] strEndTimeHHMM = strEndTime.Split(':');

            
            int intStartTime = Convert.ToInt32(strStartTimeHHMM[0]);
            int intEndTime = Convert.ToInt32(strEndTimeHHMM[0]);

            /// //////////////////////////////////////////////// START : Start Time Adding AM or PM ////////////////////////////////////////////////
            if (intStartTime >= 0 && intStartTime < 12) 
            {
                if (strStartTimeHHMM.Length == 2)
                {
                    strStartTime = strStartTimeHHMM[0].ToString() + ":" + strStartTimeHHMM[1].ToString() + " AM";
                }
                else
                {
                    strStartTime = strStartTimeHHMM[0].ToString() + " AM";
                }
                
            }
            else
            {
                if (strStartTimeHHMM.Length == 2)
                {
                    strStartTime = (Convert.ToInt16(strStartTimeHHMM[0]) - 12).ToString() + ":" + strStartTimeHHMM[1].ToString() + " PM";
                }
                else
                {
                    strStartTime = (Convert.ToInt16(strStartTimeHHMM[0]) - 12).ToString() + " PM";
                }
            }

            // END Time Adding AM or PM 
            if (intEndTime >= 0 && intEndTime < 12)
            {
                if (strEndTimeHHMM.Length == 2)
                {
                    strEndTime = strEndTimeHHMM[0].ToString() + ":" + strEndTimeHHMM[1].ToString() + " AM";
                }
                else
                {
                    strEndTime = strEndTimeHHMM[0].ToString() + " AM";
                }

            }
            else
            {
                if (strEndTimeHHMM.Length == 2)
                {
                    strEndTime = (Convert.ToInt16(strEndTimeHHMM[0]) - 12).ToString() + ":" + strEndTimeHHMM[1].ToString() + " PM";
                }
                else
                {
                    strEndTime = (Convert.ToInt16(strEndTimeHHMM[0]) - 12).ToString() + " PM";
                }
            }
            /// //////////////////////////////////////////////// END : Start Time Adding AM or PM ////////////////////////////////////////////////

            


            // DateTime StartTime = DateTime.Parse("08:00 AM");
            DateTime StartTime = DateTime.Parse(strStartTime);

            StartTime = TimeRoundOff(StartTime);

            //DateTime EndTime = DateTime.Parse("04:00 PM"); // It converts this to 1600.
            DateTime EndTime = DateTime.Parse(strEndTime); // It converts this to 1600.
            EndTime = TimeRoundOff(EndTime);

            TimeSpan OneHour = new TimeSpan(1, 0, 0);
            StartTime = StartTime.Add(OneHour);

            TimeSpan TSDuration = new TimeSpan(0, intDurationInMinutes, 0);

            //for (TimeSpan a = Convert(TimeSpan, StartTime); a <= EndTime; a = a + thirtyMin)

            string str = "";

            TimeSpan TS = EndTime - StartTime;
            decimal loop = 240000;
            if (Convert.ToDecimal(TS.ToString().Replace(":", "")) < 0)
            {
                loop = loop + Convert.ToDecimal(TS.ToString().Replace(":", ""));
                loop = loop * 2 / 10000;
            }
            else
            {
                decimal Time = Convert.ToDecimal(TS.ToString().Replace(":", ""));
                Time = Time * 2 / 10000;
                loop = Time;
            }

            str = "";
            if (strIsOpen == "open")
            {
                str += "<option Selected>ASAP</option>";
            }
            else
            {
                str += "<option Selected>Select Delivery Time</option>";            
            }

            for (int a = 0; a < loop; a++)
            {
                string[] words = StartTime.ToString().Split(' ');

                str += "<option value=\"" + Convert.ToDateTime(words[1]).ToString("H:mm") + ' ' + words[2] + "\">"+DateTime.Today.DayOfWeek+" " + Convert.ToDateTime(words[1]).ToString("H:mm") + ' ' + words[2] + "</option>";

                StartTime = StartTime + TSDuration;
            }
            return str;
        }

        #region Yasir Jamal OLD logic For IsOpen
        /// Yasir Jamal OLD logic
        /// Commented By Junaid Hassan Included new logic above for IsOpen
        /// Dated : 2013-11-26
        ////Check Outlet Time & Return status
        //public static string IsOpen(string StartTime, string EndTime)
        //{
        //    string outputtime = "close";
        //    string endtimeraw = EndTime;
        //    try
        //    {
                
        //        //Include colons in time
        //        StartTime = StartTime.IndexOf(":") > 0 ? StartTime : StartTime + ":00";
        //        EndTime = EndTime.IndexOf(":") > 0 ? EndTime : EndTime + ":00";
        //        StartTime = StartTime == "24:00" ? "23:59" : StartTime;
        //        EndTime = EndTime == "24:00" ? "23:59" : EndTime;
        //        //Apply dates 
        //        DateTime dtstart = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + StartTime);
        //        DateTime dtendd = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + EndTime);
        //        DateTime dtcur = DateTime.Now;
        //        //Check closing
        //        if (dtendd.TimeOfDay.Hours <= 12)
        //        {
        //            dtendd=dtendd.AddDays(1);
        //        }
        //        //Check day
        //        if (dtcur.TimeOfDay.Hours < 12)
        //        {
        //            dtcur = dtcur.AddDays(1);
        //        }
        //        //Check Status
        //        if (dtcur >= dtstart && dtcur <= dtendd)
        //        {
        //            outputtime = "open"; 
        //        }

        //        if (dtcur >= DateTime.Parse(dtcur.ToShortDateString()+" 1:00:00") && dtcur <= DateTime.Parse(dtcur.ToShortDateString()+" 5:00:00"))
        //        {
        //            outputtime = "close";
        //        }
                
        //        /*
        //        //Include colons in time
        //        StartTime = StartTime.IndexOf(":") > 0 ? StartTime : StartTime + ":00";
        //        EndTime = EndTime.IndexOf(":") > 0 ? EndTime : EndTime + ":00";

        //        //Modifiy 0:00 & 24:00
        //        StartTime = StartTime == "0:00" ? "23:59" : StartTime;
        //        EndTime = EndTime == "0:00" ? "23:59" : EndTime;
        //        StartTime = StartTime == "24:00" ? "23:59" : StartTime;
        //        EndTime = EndTime == "24:00" ? "23:59" : EndTime;
        //        EndTime = TimeSpan.Parse(EndTime) < TimeSpan.Parse("12:00") ? "23:59" : EndTime;

        //        if (DateTime.Now.TimeOfDay >= TimeSpan.Parse(StartTime) && DateTime.Now.TimeOfDay <= TimeSpan.Parse(EndTime))
        //        {
        //            outputtime = "open";
        //        }
        //        */
               

        //    }
        //    catch (Exception ee)
        //    {
        //        outputtime = ee.Message;
        //    }
        //    return outputtime;
        //}

        //Create A-z List
        // -------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion Yasir Jamal OLD logic For IsOpen


        public static string AtoZList()
        {
            string strOutput = "<li><a href='/{city_link}/restaurants/0-9'>0-9</a></li>";
            for (char a = 'A'; a <= 'Z'; a++)
            {
                strOutput += "<li><a href='/{city_link}/restaurants/" + a.ToString().ToLower() + "'>" + a + "</a></li>";
            }
           // strOutput += "#";
            return strOutput;
        }

        //Return Current City of user
        public static string CityName
        {
            get
            {
                try
                {
                    _CityName = Jewar.CodeLibrary.Cookies.GetCookie("UserCity").ToString().IndexOf("=") > 0 ? Jewar.CodeLibrary.Cookies.GetCookie("UserCity").ToString().Split('=')[1] : Handler.Process.GetLocationFromIP();
                }
                catch(Exception ex)
                {
                    _CityName = "karachi";
                }
                 if (_CityName == "assets")
                 {
                     _CityName = "pakistan";
                 }


                // Jewar.CodeLibrary.Cookies.CreateCookie("UserCity", _CityName.ToLower(), 10);
                 if (HttpContext.Current.Request.QueryString["city"] != null)
                {

                    _CityName = HttpContext.Current.Request.QueryString["city"].ToString() != "" ? HttpContext.Current.Request.QueryString["City"].ToString() : _CityName;
                     Jewar.CodeLibrary.Cookies.CreateCookie("UserCity", _CityName.ToLower(),30);
                }
                else
                {
                  //  _CityName =  Jewar.CodeLibrary.Cookies.GetCookie("UserCity").ToString().Split('=')[1];
                }
                
                System.Globalization.TextInfo cityTI = new System.Globalization.CultureInfo("en-US", false).TextInfo;

                
                return cityTI.ToTitleCase(_CityName);
            }
            set { _CityName = value; }
        }
        
        //Return Total outlet current page
        public static string OutletCount
        {
            get 
            {
                return Loadoutlets()!=null?Loadoutlets().Rows.Count.ToString():"0";
            }
        }

        //Load All keywords from k & r
        public static DataTable LoadKeyword()
        {
            string k = "";
            string r = "";
            string URL = "";
            if (HttpContext.Current.Request.Url.PathAndQuery.IndexOf("search.aspx") > 0)
            {
                k = HttpContext.Current.Request["k"] != "" ? "?k=" + HttpContext.Current.Request["k"].ToString() : "";
                r = HttpContext.Current.Request["r"] != "" ? "&r=" + HttpContext.Current.Request["r"].ToString() : "";
                URL = "/" + CityName + "/{search_type}" + k + r;
            }
            else
            {
                string SEOID = HttpContext.Current.Request["SEOID"] != null ? HttpContext.Current.Request["SEOID"].ToString() : "";
                string OutletCity = HttpContext.Current.Request["city"] != null ? HttpContext.Current.Request["city"].ToString() : "";
                URL = "/" + OutletCity + "/" + SEOID;
            }
            
            //URL = HttpContext.Current.Request.Url.PathAndQuery.IndexOf("Search.aspx") > 0 ? : "/" + OutletCity + "/" + SEOID;
            DataTable dtKeyword = AppDB.RefineKeyword(URL);
            if (dtKeyword.Rows.Count>0)
            {
                _MetaDesc = dtKeyword.Rows[0]["meta_description"].ToString();
                _MetaTitle = dtKeyword.Rows[0]["meta_title"].ToString();
            }
            return dtKeyword;
        }

        //Load Current Meal Time
        public static string CurrentTime()
        {
            string refine = "Mid Night Snacks";
            try
            {
                int currentHr = DateTime.Now.TimeOfDay.Hours;
                //currentHr = 0;
                currentHr = currentHr == 0 ? 24 : currentHr;
                currentHr = currentHr == 1 ? 25 : currentHr;
                currentHr = currentHr == 2 ? 26 : currentHr;
                currentHr = currentHr == 3 ? 27 : currentHr;
                currentHr = currentHr == 4 ? 28 : currentHr;


                if (currentHr >= 5 && currentHr < 10)
                    refine = "Iftar";

                if (currentHr >= 10 && currentHr < 11)
                    refine = "Iftar";

                if (currentHr >= 11 && currentHr < 16)
                    refine = "Iftar";

                if (currentHr >= 16 && currentHr < 18)
                    refine = "Iftar";

                if (currentHr >= 18 && currentHr < 19)
                    refine = "Iftar";

                if (currentHr >= 19 && currentHr < 24)
                    refine = "Dinner";

                if (currentHr >= 24 && currentHr < 26)
                    refine = "Sehri";

                if (currentHr >= 26 && currentHr <= 28)
                    refine = "Sehri";

                //if (currentHr >= 5 && currentHr < 10)
                //    refine = "Breakfast";

                //if (currentHr >= 10 && currentHr < 11)
                //    refine = "Brunch";

                //if (currentHr >= 11 && currentHr < 16)
                //    refine = "Lunch";

                //if (currentHr >= 16 && currentHr < 18)
                //    refine = "Hi-Tea";

                //if (currentHr >= 18 && currentHr < 19)
                //    refine = "Iftar";

                //if (currentHr >= 19 && currentHr < 24)
                //    refine = "Dinner";

                //if (currentHr >= 24 && currentHr < 26)
                //    refine = "Mid-Night snacks";

                //if (currentHr >= 26 && currentHr <= 28)
                //    refine = "Sehri";
            }
            catch (Exception ee)
            {

            }
            return refine;
        }

        //Format Time
        public static string FormatTime(string Time)
        {
            string output = "";
            try
            {
                Time = Time.IndexOf(":") > 0 ? Time : Time + ":00";
                Time = Time == "24:00" ? "23:59" : Time;
                Time = Time == ":00" ? "0:00" : Time;
                output = DateTime.Parse(DateTime.Now.Date.ToShortDateString() + " " + Time).ToString("h:m tt").Replace(":0 ", "");
            }
            catch (Exception ee)
            { }
            return output;
        }

        //Format Time
        public static string FormatTime(string Time,string Format)
        {
            Time = Time.IndexOf(":") > 0 ? Time : Time + ":00";
            return DateTime.Parse(DateTime.Now.Date.ToShortDateString() + " " + Time).ToString(Format);
        }
        
        //Load Reservation Count
        public static string ReservationCount
        {
            get { return Handler.AppDB.reservations()!=null?Handler.AppDB.reservations().Rows.Count.ToString():"0"; }
        }
        
        public static string TextCase(string text)
        { 
            System.Globalization.TextInfo textinfo = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            return textinfo.ToTitleCase(text.ToLower());
        }

        public static void BindData(System.Web.UI.WebControls.Repeater rptToBind,DataTable dt)
        {
            rptToBind.DataSource = dt;
            rptToBind.DataBind();
        }

        public static string GetLocationFromIP()
        {
            try
            {
                //string ip = "127.0.0.1";
                //if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"])){
                //    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0];
                //}
                //else
                //{  ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];}
                
                //LookupService locationService = new LookupService(HostingEnvironment.MapPath("~/App_Data/GeoIPCitypk.dat"));
                
                //string city=locationService.getLocation(ip).city;

                //if (city == "")
                //{
                //    LookupService locationServicenew = new LookupService(HostingEnvironment.MapPath("~/App_Data/GeoLiteCity.dat"));
                //    city = locationServicenew.getLocation(ip).city;
                //}

                //return city!=null?city:"Pakistan";

            }
            catch (Exception oex)
            {
                return "Pakistan";
            }

            return "Pakistan";

        }

        public static void SetUserCookie(string UserName, string UserNumber)
        {
            //Commented and Added By Aman Mansoor on 20-Aug-2014 to change cookie functionality EAT-760
            // Jewar.CodeLibrary.Cookies.CreateCookie("UserInfoCook", UserName + "," + UserNumber, 7);
             Jewar.CodeLibrary.Cookies.CreateCookie("customerName", UserName, 7);
             Jewar.CodeLibrary.Cookies.CreateCookie("customerMobile", UserNumber, 7);

            _Name = UserName;
            _Number = UserNumber;
            //Commented and Added end By Aman Mansoor on 20-Aug-2014 to change cookie functionality EAT-760
        }

     

        public static void GetUserCookie()
        {
            //Commented and Added By Aman Mansoor on 20-Aug-2014 to change cookie functionality EAT-760
            //string userCookie =  Jewar.CodeLibrary.Cookies.GetCookie("UserInfoCook");
            //_Name = "";
            //_Number = "";
            //if (userCookie != "")
            //{
            //    try
            //    {
            //        string CookieValue = userCookie.Split('=')[1];
            //        _Name = CookieValue.Split(',')[0];
            //        _Number = CookieValue.Split(',')[1].ToString().Split('*')[0];
            //    }
            //    catch (Exception ee)
            //    { }
            //}
           // string customerName = "", ; 

            _Name = "";
            _Number = "";
            try
            {
                _Name = HttpContext.Current.Session["customerName"].ToString();
            }
            catch (Exception ex)
            {
                try
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("customerName");
                    _Name = cookie["customerName"];
                    HttpContext.Current.Session["customerName"] = _Name;
                }
                catch (Exception ex1)
                { }
            }
            try
            {
                _Number = HttpContext.Current.Session["customerMobile"].ToString();
            }
            catch (Exception ex)
            {
                try
                {
                    HttpCookie cookie1 = HttpContext.Current.Request.Cookies.Get("customerMobile");
                    _Number = cookie1["customerMobile"];
                    HttpContext.Current.Session["customerMobile"] = _Number;
                }
                catch (Exception ex1)
                { }
            }
            //Commented and Added end By Aman Mansoor on 20-Aug-2014 to change cookie functionality EAT-760
        }

        /// <summary>
        /// Modified By : Junaid Hassan so that we can return the string Array, for customer Status Verified or Unverified
        /// dated       : 2014-07-23
        /// taskID      : EAT-658 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string[] customer(string Name, string Number)
        {
            string[] strArr = { "0", "Unverified", "0" };
            int customerID;
            DataTable dtcustomer = Jewar.CodeLibrary.DBHandler.GetData("SELECT id, IFNULL(iscustomerVerified,0) AS iscustomerVerified, IsCustomerReviewed  FROM customer WHERE mobile='" + Number + "'");
            if (dtcustomer.Rows.Count > 0)
            {
                customerID = Int32.Parse(dtcustomer.Rows[0]["ID"].ToString());
                strArr[0] = customerID.ToString();
                if (dtcustomer.Rows[0]["iscustomerVerified"].ToString() == "1")
                {
                    strArr[1] = "";
                }
                else
                {
                    strArr[1] = " Unverified";
                }
                if (Convert.ToBoolean(dtcustomer.Rows[0]["IsCustomerReviewed"]) == true)
                {
                    strArr[2] = "true";
                }
                else
                {
                    strArr[2] = "false";
                }
            }
            else
            {
                customerID =  Jewar.CodeLibrary.DBHandler.InsertDataWithID("insert into customer(name,mobile,city) values('" + Name.Replace("'", "''") + "','" + Number + "','" + CityName + "')");
                strArr[0] = customerID.ToString();
                strArr[1] = " Unverified";
                strArr[2] = "0";
            }
            return strArr;
        }

        /*******Public Properties********/

        public static string usersearch
        {
            get 
            {
                string SearchType =  Jewar.CodeLibrary.Cookies.GetCookie("SearchType").ToString().IndexOf("=") > 0 ?  Jewar.CodeLibrary.Cookies.GetCookie("SearchType").ToString().Split('=')[1] : "delivery";

                // Jewar.CodeLibrary.Cookies.CreateCookie("UserCity", _CityName.ToLower(), 10);
                if (HttpContext.Current.Request.QueryString["t"] != null)
                {
                    string type = HttpContext.Current.Request.QueryString["t"].ToString() != "d" ? "delivery" : "delivery";

                     Jewar.CodeLibrary.Cookies.CreateCookie("SearchType", type, 30);
                }

                return SearchType;
            }
        }

        public static string Duration(string minutes)
        {
            string output = "";
            try
            {
                int mints = Int32.Parse(minutes);
                output = mints+" mins";
                if (mints >= 60)
                {
                    mints = mints / 60;

                    /// Added By Junaid Hassan
                    /// EAT-196

                    if (mints > 1)
                    {
                        output = mints + " hrs";
                    }
                    else
                    {
                        output = mints + " hr";
                    }
                    // ////////////////////////////
                    if (mints >= 24)
                    {
                        mints = mints / 24;
                        //output = mints + " days";

                        /// Added By Junaid Hassan
                        /// EAT-196

                        if (mints > 1)
                        {
                            output = mints + " days";
                        }
                        else
                        {
                            output = mints + " day";
                        }
                        // ////////////////////////////
                    }
                }
                
            }
            catch (Exception ee)
            { }
            return output;
        }

        //Set Meta Properties
        public static string MetaKeyword
        {
            get { return _MetaKeyword; }
        }
        public static string Metadesc
        {
            get { return _MetaDesc; }
        }
        public static string MetaTitle
        {
            get { return _MetaTitle; }
        }
        public static string UserName
        {
            get { return _Name; }
        }
        public static string UserNumber
        {
            get { return _Number; }
        }

        public static DataTable RefineFoodSearch(DataTable ds)
        {
            DataTable dtFoodoutlets = new DataTable();
            try
            {
                //Added By Aman Mansoor on 22-Oct-2014 for Food Item base search work EAT-817
               
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["i"]))
                {
                    dtFoodoutlets =  Jewar.CodeLibrary.DBHandler.GetData(String.Format("SELECT DISTINCT o.ID FROM menuitems mi INNER JOIN menucategories mc ON mi.CategoryID = mc.ID " +
                                                                    "INNER JOIN menus m ON m.ID = mc.MenuID INNER JOIN outlets o ON m.VendorID = o.vendor_id " +
                                                                    "WHERE mi.name LIKE '%{0}%' AND o.city = '{1}'", HttpContext.Current.Request["i"], Handler.Process.CityName));
                    if (dtFoodoutlets.Rows.Count == 0)
                    {
                        dtFoodoutlets =  Jewar.CodeLibrary.DBHandler.GetData(String.Format("SELECT DISTINCT o.ID FROM menucategories mc " +
                                                                    "INNER JOIN menus m ON m.ID = mc.MenuID INNER JOIN outlets o ON m.VendorID = o.vendor_id " +
                                                                    "WHERE mc.name LIKE '%{0}%' AND o.city = '{1}'", HttpContext.Current.Request["i"], Handler.Process.CityName));
                    }
                }
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["ct"]))
                {
                    dtFoodoutlets =  Jewar.CodeLibrary.DBHandler.GetData(String.Format("SELECT DISTINCT o.ID FROM menucategories mc INNER JOIN menus m ON m.ID = mc.MenuID " +
                                                                    "INNER JOIN outlets o ON m.VendorID = o.vendor_id " +
                                                                    "WHERE mc.name LIKE '%{0}%' AND o.city = '{1}'", HttpContext.Current.Request["ct"], Handler.Process.CityName));
                }
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["i"]) && !string.IsNullOrEmpty(HttpContext.Current.Request["ct"]))
                {
                    dtFoodoutlets =  Jewar.CodeLibrary.DBHandler.GetData(String.Format("SELECT DISTINCT o.ID FROM  menuitems mi INNER JOIN menucategories mc ON mi.CategoryID = mc.ID " +
                                                                    "INNER JOIN menus m ON m.ID = mc.MenuID INNER JOIN outlets o ON m.VendorID = o.vendor_id " +
                                                                    "WHERE mi.name LIKE '%{0}%' AND mc.name LIKE '%{1}%' AND o.city = '{2}'", HttpContext.Current.Request["i"], HttpContext.Current.Request["ct"], Handler.Process.CityName));
                }

                if (dtFoodoutlets.Rows.Count > 0)
                {
                    string FoodIDs = "";
                    for (int a = 0; a < dtFoodoutlets.Rows.Count; a++)
                    {
                        FoodIDs += dtFoodoutlets.Rows[a]["ID"].ToString() + ",";
                    }
                    FoodIDs = FoodIDs.TrimEnd(',');

                    ds = Handler.AppDB.RefineTable(ds, "id in (" + FoodIDs + ")");

                }
                //Added end By Aman Mansoor on 22-Oct-2014 for Food Item base search work EAT-817
            }
            catch (Exception ee)
            { }
            return ds;
        }

        #region News Letter Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable getBlogforNewsLetter()
        {
            string strQuery = "SELECT * FROM blogs WHERE IsNewsLetterEnable = 1 ORDER BY ModifiedDate DESC LIMIT 1 ";

            return  Jewar.CodeLibrary.DBHandler.GetData(strQuery);
        }

        public static DataTable getNewoutlets()
        {
            // string strQuery = "SELECT ID, `name`, city, Slug, logo FROM outlets WHERE IS_Delivers = 1 OR Is_reservation	=1  GROUP BY Vendor_ID ORDER BY Create_Date DESC, Modified_Date DESC LIMIT 4";
            string strQuery = "SELECT ID, `name`, city, Slug, logo FROM outlets WHERE IS_Delivers = 1 OR Is_reservation	=1  GROUP BY Vendor_ID ORDER BY Modified_Date DESC, Create_Date DESC  LIMIT 4";

            return  Jewar.CodeLibrary.DBHandler.GetData(strQuery);
        }

        public static DataTable getPromotionalDeals(string strLimit)
        {
            // string strQuery = "select ID, Slug, DealDescription, dealimage from outlets where is_delivers=1 and IsDealActive = 1 " + broadwaypizzaApp.Handler.AppDB.CityCheck() + " order by RAND() limit 6";
            string strQuery = "select ID, Slug, DealDescription, dealimage from outlets where is_delivers=1 and IsDealActive = 1 order by RAND() limit " + strLimit;
            // string strQuery = "select ID, Slug, DealDescription, dealimage from outlets where is_delivers=1 and IsDealActive = 1 order by ID limit " + strLimit;

            return  Jewar.CodeLibrary.DBHandler.GetData(strQuery);
        }

        #endregion News Letter Methods

    }
}