using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Data;
namespace Jewar.CodeLibrary
{
    public  static class Utilities
    {
        public static bool ValidateEmail(string email)
        {

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static string GetOutletIDsByVendorID(string VendorID)
        {
            string strVendorIDs = "";
            DataTable DT = DBHandler.GetData("SELECT ID FROM outlets WHERE Vendor_ID = " + VendorID);

            foreach (DataRow DR in DT.Rows)
            {
                strVendorIDs = strVendorIDs + DR["ID"].ToString() + ",";
            }

            return strVendorIDs.TrimEnd(',');
        }


        public static bool ValidateInteger(string myInt)
        {

            Regex regex = new Regex(@"^[0-9]*$");
            Match match = regex.Match(myInt);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool ValidateGeneralString(string myStr)
        {

            Regex regex = new Regex(@"^[a-zA-Z0-9 -&/,'.-]*$");
            Match match = regex.Match(myStr);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool ValidateOnlyString(string myStr)
        {

            Regex regex = new Regex(@"^[a-zA-Z0-9 .'-]*$");
            Match match = regex.Match(myStr);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static string AddUpdatecustomer(string name, string mobile, string address, string area, string city, string email, string gender,DateTime DOB)
        {
            try
            {
                 name = name.Trim(); mobile = mobile.Trim(); address = address.Trim(); area = area.Trim();
                city = city.Trim(); email = email.Trim(); gender = gender.Trim();

                string ReturnMsg = "";

              
                    string strError = "";

                    strError = ValidateAllfields(name,mobile,address,area,city,email,gender);

                    if (strError != "")
                    {
                        ReturnMsg = strError;
                        
                        return ReturnMsg;
                    }
                    //Insert statement and calling insert date of db handler class

                    string strcustomerID = "";
                    if (mobile != "")
                    {
                        strcustomerID = IscustomerExist(mobile);
                    }
                    string strDOB = DOB.ToString("yyyy-MM-dd");
                    if (strDOB.Contains("0000"))
                    {
                        strDOB = null;
                    }
                    if (strcustomerID == "")
                    {

                        string InsertSQL = "";

                        InsertSQL = String.Format("insert into customer (`name`,mobile,address,area,city,email,gender,DOB) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')"
                                                                   , name, mobile, address, area, city, email, gender, strDOB);

                        long Insert = CodeLibrary.DBHandler.InsertDataGetInsertedID(InsertSQL);
                        if (Insert > 0)
                        {
                            return Insert.ToString();
                        }
                        return "";
                    }
                    else
                    {

                        string strSetClause = "";
                        if (name != "")
                        {
                            strSetClause += string.Format("`name` = '{0}'", name);
                        }
                        if (mobile != "")
                        {
                            strSetClause += string.Format("`mobile` = '{0}'", mobile);
                        }
                        if (address != "")
                        {
                            strSetClause += string.Format("`address` = '{0}'", address);
                        }

                        if (area != "")
                        {
                            strSetClause += string.Format("`area` = '{0}'", area);
                        }
                        if (city != "")
                        {
                            strSetClause += string.Format("`city` = '{0}'", city);
                        }

                        if (email != "")
                        {
                            strSetClause += string.Format("`email` = '{0}'", email);
                        }

                        if (gender != "")
                        {
                            strSetClause += string.Format("`gender` = '{0}'", gender);
                        }

                        if (strDOB != "")
                        {
                            strSetClause += string.Format("`DOB` = '{0}'", strDOB);
                        }


                        string UpdateQuery = String.Format("Update customer set {0} where ID = '{1}'", strSetClause, strcustomerID);

                        int Insert = CodeLibrary.DBHandler.InsertData(UpdateQuery);
                        if (Insert > 0)
                        {
                            return strcustomerID;
                        }
                        else
                        {
                            return "";
                        }
                        
                    }

                    return "";
                   
                }


            
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string ValidateAllfields( string name, string mobile, string address, string area, string city, string email, string gender)
        {
            string msg = "";
            if (name.Trim() != "")
            {
                if (!Utilities.ValidateOnlyString(name.Trim()))
                {
                    msg = "Invalid Name; ";
                }
            }
            if (mobile.Trim() != "")
                if (!Utilities.ValidateInteger(mobile.Trim()))
                {
                    msg += "Invalid mobile; ";
                }
            if (address.Trim() != "")
                if (!Utilities.ValidateGeneralString(address.Trim()))
                {
                    msg += "Invalid Address; ";
                }
            if (area.Trim() != "")
                if (!Utilities.ValidateGeneralString(area.Trim()))
                {
                    msg += "Invalid Area; ";
                }
            if (city.Trim() != "")
                if (!Utilities.ValidateGeneralString(city.Trim()))
                {
                    msg += "Invalid City; ";
                }
            if (email.Trim() != "")
                if (!Utilities.ValidateEmail(email.Trim()))
                {
                    msg += "Invalid Email; ";
                }


            return msg;


        }

        private static string IscustomerExist(string mobile)
        {
            DataTable dtUser = new DataTable();

            string sqlQuery = string.Format("SELECT * FROM customer WHERE mobile = {0} ", mobile);

            dtUser = DBHandler.GetData(sqlQuery);//DBHandler.GetData(strQuery);

            if (dtUser.Rows.Count > 0)
            {
                return dtUser.Rows[0]["ID"].ToString();
            }

            return "";
        }

        //Update Activity log Table after every transaction
        public static int AddUpdatecustomer(long customerID, string TransactionType, string AddUpdate, string Status)
        {
            string InsertSQL = "";
            int Insert = 1;
            //For orders
            if (TransactionType == "Order")
            {
                if (AddUpdate == "Add")
                {
                    InsertSQL = string.Format("UPDATE customer set `Transactions` = {0}, `orders` = {1}, `iscustomerVerified` = 1 where `ID` = '{2}'", "IFNULL(Transactions,0) + " + 1, "IFNULL(orders,0) + " + 1, customerID);
                }
                else
                {
                    if (Status == "Fake Order")
                    {
                        InsertSQL = string.Format("UPDATE customer set `Fakeorders` = {0}, `iscustomerVerified` = 0 where `ID` = '{1}'", "IFNULL(Fakeorders,0) + " + 1, customerID);
                    }
                    else if (Status == "Confirmed")
                    {
                        InsertSQL = string.Format("UPDATE customer set `Confirmedorders` = {0}, `iscustomerVerified` = 1 where `ID` = '{1}'", "IFNULL(Confirmedorders,0) + " + 1, customerID);
                    }
                    else if(Status == "Rejected" || Status == "Rejected From Device")
                    {
                        InsertSQL = string.Format("UPDATE customer set `iscustomerVerified` = 1 where `ID` = '{0}'",   customerID);
                    }
                }
            }
            //For reservations
            else if (TransactionType == "Reservation")
            {
                if (AddUpdate == "Add")
                {
                    InsertSQL = string.Format("UPDATE customer set `Transactions` = {0}, `reservations` = {1}, `iscustomerVerified` = 1 where `ID` = '{2}'", "IFNULL(Transactions,0) + " + 1, "IFNULL(reservations,0) + " + 1, customerID);
                }
                else
                {
                    if (Status == "No Show")
                    {
                        InsertSQL = string.Format("UPDATE customer set `NoShows` = {0} where `ID` = '{1}'", "IFNULL(NoShows,0) + " + 1, customerID);
                    }
                    else if (Status == "Reconciled")
                    {
                        InsertSQL = string.Format("UPDATE customer set  `iscustomerVerified` = 1  where `ID` = '{0}'", customerID);
                    }
                }
            }
            //For enquiry
            else if (TransactionType == "enquiry")
            {
                InsertSQL = string.Format("UPDATE customer set `Transactions` = {0}, `enquiry` = {1} where `ID` = '{2}'", "IFNULL(Transactions,0) + " + 1, "IFNULL(enquiry,0) + " + 1, customerID);
            }

            if (InsertSQL != "")
            {
                Insert = CodeLibrary.DBHandler.InsertDataWithoutLogin(InsertSQL);
            }
            return Insert;
        }

        public static bool ValidatePhone(string to)
        {
            Regex regex = new Regex(@"(03\d{9})");
            if (regex.IsMatch(to)) { return true; }
            else { return false; }
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
                //// OverRiding the Resturant Timing With Broadway Timing: /////// START ------------------------------------------------------------------------
                //if (DateTime.Now.Hour > 0 && DateTime.Now.Hour < 10)
                //{
                //    outputtime = "close";
                //}
                //// OverRiding the Resturant Timing With Broadway Timing: /////// END   ------------------------------------------------------------------------
                /// END  //////////////////////////////////////////////////////////////////////////////
            }





            catch (Exception ee)
            {
                outputtime = ee.Message;
            }
            return outputtime;
        }


        public static int Insertlog(long ObjectID, string Type, string Remarks, string channel, string logType)
        {
            string InsertSQL = string.Format("INSERT INTO `log` (`object_id`, `object_type`, `user_id`, `Remarks`, Channel, logType) VALUES " +
                                            "({0},'{1}', '{2}', '{3}', '{4}', '{5}');"
                                            , ObjectID, Type, HttpContext.Current.Session["UserID"], Remarks, channel, logType);

            int Insert = CodeLibrary.DBHandler.InsertData(InsertSQL);

            return Insert;
        }

        public static string CheckTitle(string strString)
        {
            strString = strString.ToLower().Trim();

            strString = Regex.Replace(strString, "[^a-z0-9\\//s-]", "-");

            strString = strString.Replace(" ", "-").Replace(",", "-").Replace(",", "-").Replace(":", "-").Replace(",", "-").Replace("--", "-").TrimEnd('/').TrimEnd('-').TrimStart('-');

            strString = strString.Replace("--", "-");

            return strString;
        }

        public static string CheckSlug(string Slug)
        {
            try
            {              
                Slug = Slug.ToLower().Trim();

                Slug = Regex.Replace(Slug,"[^a-z0-9\\//s-]", "-");

                Slug = Slug.Replace(" ", "-").Replace(",", "-").Replace(",", "-").Replace(":", "-").Replace(",", "-").Replace("--", "-").TrimEnd('/').TrimEnd('-').TrimStart('-');

                Slug = Slug.Replace("--", "-");
            }
            catch (Exception ex)
            { }

            return Slug.ToLower();

        }


        public static DataTable CheckOpenHours()
        {
            DataTable dtoutlets = DBHandler.GetData("select * from outlets where is_delivers = 1");
           

            // Aman Confirms that No Sec in DataBase only hh:mm OR hh OR H
            // Signed By Aman 
            string StartTime, EndTime;
            DataTable dtFilteroutlets = new DataTable();
            dtFilteroutlets.Columns.Add("ID", typeof(string));
            dtFilteroutlets.Columns.Add("Name", typeof(string)); 
            dtFilteroutlets.Columns.Add("City", typeof(string));
            dtFilteroutlets.Columns.Add("NoOfHours", typeof(string));
            dtFilteroutlets.Columns.Add("weekdaytiming", typeof(string));
            dtFilteroutlets.Columns.Add("weekendtiming", typeof(string));
            DataRow dr = null;
            for (int a = 0; a < dtoutlets.Rows.Count; a++)
            {


                //StartTime = dtoutlets.Rows[a]["weekday_timing"].ToString().Split('-')[0];
                //EndTime = dtoutlets.Rows[a]["weekday_timing"].ToString().Split('-')[1];

                StartTime = dtoutlets.Rows[a]["weekend_timing"].ToString().Split('-')[0];
                EndTime = dtoutlets.Rows[a]["weekend_timing"].ToString().Split('-')[1];

                if (dtoutlets.Rows[a]["ID"].ToString() == "1514")
                {
                }

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

                if (intNumberOfhours < 700 || intNumberOfhours > 2300)
                {
                    dr = dtFilteroutlets.NewRow();
                    dr["ID"] = dtoutlets.Rows[a]["ID"].ToString();
                    dr["Name"] = dtoutlets.Rows[a]["Name"].ToString();
                    dr["City"] = dtoutlets.Rows[a]["City"].ToString();
                    dr["NoOfHours"] = intNumberOfhours;//dtoutlets.Rows[a]["City"].ToString();                    
                    dr["weekdaytiming"] = dtoutlets.Rows[a]["weekday_timing"].ToString();
                    dr["weekendtiming"] = dtoutlets.Rows[a]["weekend_timing"].ToString();

                    dtFilteroutlets.Rows.Add(dr);
                }
            }
            return dtFilteroutlets;
        }

        public static string CheckReferrer()
        {
            string Referrer = "Self";
            try
            {
                if (HttpContext.Current.Session["Referrer"] != null)
                {
                    if (HttpContext.Current.Session["Referrer"].ToString() != "")
                    {
                        DataTable dtReferrer =  DBHandler.GetData("select * from taxonomy where name = 'contactSource'");
                        if (dtReferrer.Rows.Count > 0)
                        {
                            for (int a = 0; a < dtReferrer.Rows.Count; a++)
                            {
                                if (HttpContext.Current.Session["Referrer"].ToString().ToLower().Contains(dtReferrer.Rows[a]["value"].ToString().ToLower()))
                                {
                                    Referrer = dtReferrer.Rows[a]["alias"].ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            return Referrer;
        }
    }

   
}