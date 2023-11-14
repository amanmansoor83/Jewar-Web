/**
* Copyright (c) 2013, broadwaypizza
* All rights reserved.
* @author Yasir Ahmed <yasir@broadwaypizza.pk>
* @version 1.0.1
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Jewar.CodeLibrary;

namespace Jewar.Handler
{
    /// <summary>
    /// This class manages database operations at the application level.
    /// </summary>
    public static class AppDB
    {
        //Load All Tables
        public static DataTable cities() { return GetTable("cities","levelno=0 and parentid is null"); }


        /// ////////////////////////////////////////////////////////////////////// START ////////////////////////////////////////////////////////////////////// 
        /// Modified By         : Junaid Hassan
        /// Dated               : 2014-05-15
        /// Purpose             : We need a custom Query for All outlets.
        /// Task ID             : EAT-512
        // public static DataTable Alloutlets() { return GetTable("outlets"); }

        /// ////////////////////////////////////////////////////////////////////// START ////////////////////////////////////////////////////////////////////// 
        /// Modified By         : Junaid Hassan
        /// Dated               : 2014-0624
        /// Purpose             : Need To Add out of business outletstatus
        /// Task ID             : EAT-614
        
        ////public static DataTable Alloutlets() 
        ////{
        ////    return Jewar.CodeLibrary.DBHandler.GetDataCache("SELECT *, " +
        ////                                                "CASE " +
        ////                                                    "WHEN DATEDIFF(NOW(),create_Date) < 7	THEN 'new' " +
        ////                                                    "WHEN DATEDIFF(NOW(),Modified_date) < 7	THEN 'updated' " +                                                            
        ////                                                    "ELSE " +
        ////                                                    "'ZZZZZZZZ' " +
        ////                                                "END " +
        ////                                                "AS outletstatus " +
        ////                                            "FROM outlets ", "outlets"); 
        ////}
        public static DataTable Alloutlets()
        {
            //Commented and Added by Aman Mansoor on 22-Aug-2014 to set Is_Reservation = 0 when outlet is "temporarily closed" EAT-729
            //return Jewar.CodeLibrary.DBHandler.GetDataCache("SELECT `ID`,`vendor_id`, `name`, `city`, `seating`, `email`, `phone`, `address`,`geo_code` ,`about` ,`slug` ,`old_slug`, `type`, `budget` " +
            //                                            ",`cuisines`, `facilities`, `delivery_commision`, `delivery_fees` ,`delivery_localities` ,`delivery_minimum` ,`delivery_tax`,`delivery_time` " +
            //                                            ",`delivery_discount` ,`extra_details`,`owner`,`password`,`rating` ,`delivery_discount_text`,`logo` ,`meal_times`,`near_by`,`weekend_timing` " +
            //                                            ", `weekday_timing` ,`open_days`, CASE WHEN outletstatus = 'Out of Business' THEN 0 ELSE `is_delivers` END AS is_delivers,`buffet` " +
            //                                            ",`group_policy`,CASE WHEN outletstatus = 'Out of Business' THEN 0 ELSE `is_reservation` END AS is_reservation,`create_date` ,`User_ID`  " +
            //                                            ",`Cover`,`reservation_fee` ,`reservation_subscribtion` big,`reservation_fineprint` ,`reservation_discount` ,`reservation_discount_TEXT` " +
            //                                            ",`Invoice_Days` ,`Outlet_Area` ,`Review_count` ,`Tags`,`reservation_contract_start_date` ,`reservation_contract_end_date` ,`is_convert`  " +
            //                                            ",`is_advance` ,`RMSContractStart` ,`RMSContractEnd` ,`BDUserID` ,`DMSEmailSent` ,`RMSEmailSent` ,`DMSContractStart` ,`DMSContractEnd` " +
            //                                            ",`Modified_Date` ,`Modified_By` ,`Notes` ,`RMSDiscountExpiry` ,`DMSDiscountExpiry` ,`DealDescription` ,`DealImage`,`IsDealActive` ,`MachineID` " +
            //                                            ",`Rank`,`IsSponsored` ,`ResDealDescription` ,`ResDealImage`,`IsResDealActive` ,`DealExpiry` ,`ResDealExpiry`,`IsResSponsored` ,`ResSponsorExpiry` " +
            //                                            ", CASE WHEN DATEDIFF(NOW(),create_Date) < 7 THEN 'new' WHEN DATEDIFF(NOW(),Modified_date) < 7 THEN 'updated' ELSE 'ZZZZZZZZ' END  AS outletstatus " +
            //                                            ", outletstatus AS `BusinessStatus` " +
            //                                            ", RateListHTML, RateListEnabled " + // Added By Junaid Hassan under Task : EAT-607

            //                                          "FROM outlets", "outlets");

            
            return  DBHandler.GetDataCache("SELECT `ID`,`vendor_id`, `name`, `city`, `seating`, `email`, `phone`, `address`,`geo_code` ,`about` ,`slug`  , `type`, `budget` " +
                                                       ",`cuisines`, `facilities`, `delivery_commision`, `delivery_fees` ,concat(`delivery_localities`,',') as delivery_localities,`delivery_minimum` ,`delivery_tax`,`delivery_time` " +
                                                       ",`delivery_discount` ,`extra_details`,`owner`,`password`,`rating` ,`delivery_discount_text`,`logo` ,`meal_times`,`near_by`,`weekend_timing` " +
                                                       ", `weekday_timing` ,`open_days`, CASE WHEN outletstatus = 'Out of Business' THEN 0 ELSE `is_delivers` END AS is_delivers,`buffet` " +
                                                       ",`group_policy`,CASE WHEN outletstatus = 'Out of Business' THEN 0 ELSE CASE WHEN outletstatus = 'temporarily closed' THEN 0 ELSE `is_reservation` END END AS is_reservation,`create_date` ,`User_ID`  " +
                                                       ",`Cover`,`reservation_fee` ,`reservation_subscribtion` big,`reservation_fineprint` ,`reservation_discount` ,`reservation_discount_TEXT` " +
                                                       ",`Invoice_Days` ,`Outlet_Area` ,`Review_count` ,`Tags`,`reservation_contract_start_date` ,`reservation_contract_end_date` ,`is_convert`  " +
                                                       ",`is_advance` ,`RMSContractStart` ,`RMSContractEnd` ,`BDUserID` ,`DMSEmailSent` ,`RMSEmailSent` ,`DMSContractStart` ,`DMSContractEnd` " +
                                                       ",`Modified_Date` ,`Modified_By` ,`Notes` ,`RMSDiscountExpiry` ,`DMSDiscountExpiry` ,`DealDescription` ,`DealImage`,`IsDealActive` ,`MachineID` " +
                                                       ",`Rank`,`IsSponsored` ,`ResDealDescription` ,`ResDealImage`,`IsResDealActive` ,`DealExpiry` ,`ResDealExpiry`,`IsResSponsored` ,`ResSponsorExpiry` " +
                                                       ", CASE WHEN DATEDIFF(NOW(),create_Date) < 10 THEN 'new' WHEN DATEDIFF(NOW(),Modified_date) < 10 THEN 'updated' ELSE 'ZZZZZZZZ' END  AS outletstatus " +
                                                       ", outletstatus AS `BusinessStatus` " +
                                                       ", RateListHTML, RateListEnabled, Delivery, TakeAway " + // Added By Junaid Hassan under Task : EAT-607
                                                       ", Delivery_Discount_Expiry , Reservation_Discount_Expiry, IFNULL(IsCreditCardEnabled,0) as IsCreditCardEnabled ,IsCashEnabled, break_timing,closereason " +
                                                       "FROM outlets", "outlets");
            //Modified end by Aman Mansoor on 22-Aug-2014 to set Is_Reservation = 0 when outlet is "temporarily closed" EAT-729
        }
        /// Task ID             : EAT-614
        /// ////////////////////////////////////////////////////////////////////// END ////////////////////////////////////////////////////////////////////// 

        /// Task ID             : EAT-512
        /// ////////////////////////////////////////////////////////////////////// END ////////////////////////////////////////////////////////////////////// 
        /// 

        public static DataTable Seo() { return GetTable("seo"); }
        public static DataTable shots() { return GetTable("shots"); }
        public static DataTable Localities() { return GetTable("localities"); }
        public static DataTable Config() { return GetTable("config"); }
        public static DataTable users() { return GetTable("users"); }
        public static DataTable reviews() { return GetTable("reviews", " `status`='approved'"); }
        public static DataTable reservations() { return GetTable("reservations"); }
        public static DataTable Monthorders() { return Jewar.CodeLibrary.DBHandler.GetData("SELECT *  FROM orders  WHERE created BETWEEN DATE_ADD(CURDATE(),INTERVAL -30 DAY) AND CURDATE()"); }
        public static DataTable orders() { return GetTable("orders"); }
        public static DataTable Monthreservations() { return Jewar.CodeLibrary.DBHandler.GetData("SELECT *  FROM reservations WHERE created BETWEEN DATE_ADD(CURDATE(),INTERVAL -30 DAY) AND CURDATE()"); }
        
        public static DataTable taxonomy() { return GetTable("taxonomy"); }
        //public static DataTable Menu() { return GetTable("menu","outletid="+Outlet.OID); }
        // Modified BY JUNAID 
        // public static DataTable Menu() { return Jewar.CodeLibrary.DBHandler.GetData("SELECT *,MC.Name as category,MC.Description as catdesc,MC.order as catorder FROM menuitems AS MI,menucategories AS MC WHERE MC.ID=MI.CategoryID and mc.menuid=(select id from menus where outletid='" + Outlet.OID + "' limit 1) and mc.isactive=1 and mi.isactive=1"); }
        // EAT-689 public static DataTable Menu() { return Jewar.CodeLibrary.DBHandler.GetData("SELECT *,MC.Name as category,MC.Description as catdesc,MC.order as catorder FROM menuitems AS MI,menucategories AS MC WHERE MC.ID=MI.CategoryID and mc.menuid IN (select id from menus where outletid='" + Outlet.OID + "' AND IsActive=1) and mc.isactive=1 and mi.isactive=1"); }
        /// 
        //Commented and Added by Aman Mansoor on 02-Jan-2014 to check menu time EAT-194 & EAT-220 
        //public static DataTable Categories() { return Jewar.CodeLibrary.DBHandler.GetDataCache("SELECT *,mc.name as Category FROM `menucategories` AS mc,`menus` AS m,outlets AS o WHERE o.id=m.outletid AND m.id=mc.menuid AND o.id=" + Outlet.OID + "  and m.isactive=1 and mc.isactive=1 ORDER BY mc.order,mc.name", "MenuCategory" + Outlet.OID); }
        // Modififed by Junaid Hassan
        // EAT-329 
        // public static DataTable Categories() { return Jewar.CodeLibrary.DBHandler.GetDataCache("SELECT *,mc.name as Category FROM `menucategories` AS mc,`menus` AS m,outlets AS o WHERE o.id=m.outletid AND m.id=mc.menuid AND o.id=" + Outlet.OID + "  and m.isactive=1 and mc.isactive=1 and '" + DateTime.Now.ToString("HH:mm") + "' BETWEEN m.StartTime AND m.EndTime ORDER BY mc.order,mc.name", "MenuCategory" + Outlet.OID); }
        // public static DataTable Categories() { return Jewar.CodeLibrary.DBHandler.GetData("SELECT *,CASE WHEN(IsExclusiveDeal) = 1 THEN 'exclusive' ELSE '' END AS ExclusiveDealClass,CASE WHEN(IsGlobal) = 1 THEN 'IsGlobal' ELSE '' END AS IsGlobal ,mc.name as Category FROM `menucategories` AS mc,`menus` AS m,outlets AS o WHERE o.id=m.outletid AND m.id=mc.menuid AND o.id=" + Outlet.OID + "  and m.isactive=1 and mc.isactive=1 and '" + DateTime.Now.ToString("HH:mm") + "' BETWEEN m.StartTime AND m.EndTime ORDER BY mc.order,mc.name"); }
        // EAT-423
        // EAT-508 public static DataTable Categories() { return Jewar.CodeLibrary.DBHandler.GetData("SELECT *,CASE WHEN(IsExclusiveDeal) = 1 THEN 'exclusive' ELSE '' END AS ExclusiveDealClass,CASE WHEN(IsGlobal) = 1 THEN 'isglobal' ELSE '' END AS IsGlobalClass, URL, CASE WHEN(IsGlobal) = 1 THEN 'global' ELSE '' END AS GlobalMenuItemClass  , IFNULL(CASE WHEN(IsGlobal) = 1 THEN CONCAT('style=\"background-image:url(''https://cdn.broadwaypizza.pk/categories/logos/', mc.ImageName,''');\"') ELSE '' END,'') AS GlobalBackgroundImgStyle ,mc.name as Category FROM `menucategories` AS mc,`menus` AS m,outlets AS o WHERE o.id=m.outletid AND m.id=mc.menuid AND o.id=" + Outlet.OID + "  and m.isactive=1 and mc.isactive=1 and '" + DateTime.Now.ToString("HH:mm") + "' BETWEEN m.StartTime AND m.EndTime ORDER BY mc.order,mc.name"); }
        // EAT-689 public static DataTable Categories() { return Jewar.CodeLibrary.DBHandler.GetData("SELECT *,CASE WHEN(IsExclusiveDeal) = 1 THEN 'exclusive' ELSE '' END AS ExclusiveDealClass,CASE WHEN(IsGlobal) = 1 THEN 'isglobal' ELSE '' END AS IsGlobalClass, URL, CASE WHEN(IsGlobal) = 1 THEN 'global' ELSE '' END AS GlobalMenuItemClass  , IFNULL(CASE WHEN(IsGlobal) = 1 THEN CONCAT('style=\"background-image:url(''https://cdn.broadwaypizza.pk/categories/logos/', mc.ImageName,''');\"') ELSE '' END,'') AS GlobalBackgroundImgStyle ,mc.name as Category, mc.StartTime as CatStartTime, mc.EndTime as CatEndTime, 'close' as CatIsOpen FROM `menucategories` AS mc,`menus` AS m,outlets AS o WHERE o.id=m.outletid AND m.id=mc.menuid AND o.id=" + Outlet.OID + "  and m.isactive=1 and mc.isactive=1 and '" + DateTime.Now.ToString("HH:mm") + "' BETWEEN m.StartTime AND m.EndTime ORDER BY mc.order,mc.name"); }

        /// /////////////////////////////////// START /////////////////////////////////// 
        /// Modified BY     : Junaid Hassan 
        /// Dated           : 2015-03-04
        /// Purpose         : Get the Categories with menus Vendor
        /// TaskID          : EAT-872
        /// </summary>
        /// <param name="outletID"></param>
        /// <returns></returns>
        // public static DataTable Categories(string outletID) { return Jewar.CodeLibrary.DBHandler.GetData("SELECT *,CASE WHEN(IsExclusiveDeal) = 1 THEN 'exclusive' ELSE '' END AS ExclusiveDealClass,CASE WHEN(IsGlobal) = 1 THEN 'isglobal' ELSE '' END AS IsGlobalClass, URL, CASE WHEN(IsGlobal) = 1 THEN 'global' ELSE '' END AS GlobalMenuItemClass  , IFNULL(CASE WHEN(IsGlobal) = 1 THEN CONCAT('style=\"background-image:url(''https://cdn.broadwaypizza.pk/categories/logos/', mc.ImageName,''');\"') ELSE '' END,'') AS GlobalBackgroundImgStyle ,mc.name as Category, mc.StartTime as CatStartTime, mc.EndTime as CatEndTime, 'close' as CatIsOpen FROM `menucategories` AS mc,`menus` AS m,outlets AS o WHERE o.id=m.outletid AND m.id=mc.menuid AND o.id=" + outletID + "  and m.isactive=1 and mc.isactive=1 and '" + DateTime.Now.ToString("HH:mm") + "' BETWEEN m.StartTime AND m.EndTime ORDER BY mc.order,mc.name"); }
        public static DataTable Categories(string outletID) 
        {
            //" , CASE WHEN mc.StartTime = '00:00' AND mc.EndTime = '00:00' THEN '(All Day)' ELSE CONCAT('(',TIME_FORMAT(mc.StartTime, '%h:%i %p')  ,' - ', TIME_FORMAT(mc.EndTime, '%h:%i %p') , ')') END AS OpenTime " +

            return Jewar.CodeLibrary.DBHandler.GetData(
                "SELECT  * , CASE WHEN(IsExclusiveDeal) = 1 THEN 'exclusive' ELSE '' END AS ExclusiveDealClass " +
                    ", CASE WHEN(IsGlobal) = 1 THEN 'isglobal' ELSE '' END AS IsGlobalClass " +
                    ", URL, CASE WHEN(IsGlobal) = 1 THEN 'global' ELSE '' END AS GlobalMenuItemClass " +
                    ", IFNULL(CASE WHEN(IsGlobal) = 1 THEN CONCAT('style=\"background-image:url(''https://cdn.broadwaypizza.pk/categories/logos/', mc.ImageName,''');\"') ELSE '' END,'') AS GlobalBackgroundImgStyle " +
                    ", mc.name AS Category, mc.StartTime AS CatStartTime, mc.EndTime AS CatEndTime, 'close' AS CatIsOpen " +
                    
                "FROM " +
                     "`menucategories` AS mc " +
                    ",`menus` AS m " +
                    ", outlets AS o " +
                "WHERE " +
                //"--    o.id=m.outletid "+
                    "o.Vendor_ID = m.VendorID " +
                    "AND m.id=mc.menuid " +
                    "AND o.id=" + outletID + "  AND m.isactive=1 AND mc.isactive=1 " +
                    "AND '" + DateTime.Now.AddHours(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["AddHours"])).ToString("HH:mm") + "' BETWEEN m.StartTime AND m.EndTime and mc.name != 'Frozella (Frozen Pizzas)'  AND (mc.days = 'All' OR mc.days = DAYNAME(NOW())) AND  (mc.outlets = 'All' OR mc.outlets =  " + outletID + ")  " +
                "ORDER BY " +
                    "mc.order,mc.name");
        }
        /// /////////////////////////////////// END /////////////////////////////////// 
        // EAT-329 


        /// /////////////////////////////////// START /////////////////////////////////// 
        /// Modified BY     : Junaid Hassan 
        /// Dated           : 2015-03-04
        /// Purpose         : Get the Categories with menus Vendor
        /// TaskID          : EAT-872
        /// </summary>
        /// <param name="outletID"></param>
        /// <returns></returns>
        // public static DataTable Categories(string outletID) { return Jewar.CodeLibrary.DBHandler.GetData("SELECT *,CASE WHEN(IsExclusiveDeal) = 1 THEN 'exclusive' ELSE '' END AS ExclusiveDealClass,CASE WHEN(IsGlobal) = 1 THEN 'isglobal' ELSE '' END AS IsGlobalClass, URL, CASE WHEN(IsGlobal) = 1 THEN 'global' ELSE '' END AS GlobalMenuItemClass  , IFNULL(CASE WHEN(IsGlobal) = 1 THEN CONCAT('style=\"background-image:url(''https://cdn.broadwaypizza.pk/categories/logos/', mc.ImageName,''');\"') ELSE '' END,'') AS GlobalBackgroundImgStyle ,mc.name as Category, mc.StartTime as CatStartTime, mc.EndTime as CatEndTime, 'close' as CatIsOpen FROM `menucategories` AS mc,`menus` AS m,outlets AS o WHERE o.id=m.outletid AND m.id=mc.menuid AND o.id=" + outletID + "  and m.isactive=1 and mc.isactive=1 and '" + DateTime.Now.ToString("HH:mm") + "' BETWEEN m.StartTime AND m.EndTime ORDER BY mc.order,mc.name"); }
        public static DataTable Categories(string outletID, string Category)
        {
            //" , CASE WHEN mc.StartTime = '00:00' AND mc.EndTime = '00:00' THEN '(All Day)' ELSE CONCAT('(',TIME_FORMAT(mc.StartTime, '%h:%i %p')  ,' - ', TIME_FORMAT(mc.EndTime, '%h:%i %p') , ')') END AS OpenTime " +

            return Jewar.CodeLibrary.DBHandler.GetData(
                "SELECT  * , CASE WHEN(IsExclusiveDeal) = 1 THEN 'exclusive' ELSE '' END AS ExclusiveDealClass " +
                    ", CASE WHEN(IsGlobal) = 1 THEN 'isglobal' ELSE '' END AS IsGlobalClass " +
                    ", URL, CASE WHEN(IsGlobal) = 1 THEN 'global' ELSE '' END AS GlobalMenuItemClass " +
                    ", IFNULL(CASE WHEN(IsGlobal) = 1 THEN CONCAT('style=\"background-image:url(''https://cdn.broadwaypizza.pk/categories/logos/', mc.ImageName,''');\"') ELSE '' END,'') AS GlobalBackgroundImgStyle " +
                    ", mc.name AS Category, mc.StartTime AS CatStartTime, mc.EndTime AS CatEndTime, 'close' AS CatIsOpen " +

                "FROM " +
                     "`menucategories` AS mc " +
                    ",`menus` AS m " +
                    ", outlets AS o " +
                "WHERE " +
                //"--    o.id=m.outletid "+
                    "o.Vendor_ID = m.VendorID " +
                    "AND m.id=mc.menuid " +
                    "AND o.id=" + outletID + "  AND m.isactive=1 AND mc.isactive=1 " +
                    "AND '" + DateTime.Now.AddHours(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["AddHours"])).ToString("HH:mm") + "' BETWEEN m.StartTime AND m.EndTime and mc.name = '"  + Category + "'" +
                "ORDER BY " +
                    "mc.order,mc.name");
        }
        /// /////////////////////////////////// END /////////////////////////////////// 
        // EAT-329 


        
        //Commented and Added end by Aman Mansoor on 02-Jan-2014 
        
        //public static DataTable Categories() { return RefineTable(Menu(),"category <> ''","catorder","category"); }
        public static DataTable blogs() { return RefineTable(GetTable("blogs"), "", "id desc"); }

        public static DataTable Deliverareas() { return GetTable("outletdeliveryareas"); }
        public static DataTable DeliveryCharges(string outletid, string area) { return RefineTable(Deliverareas(), "outletid='" + outletid + "' and area='" + area + "'", ""); }

        //public static DataTable Discountoutlets(int rows) { return GetTable("outlets", "reservation_discount>0 and city='" + Process.CityName + "' order by RAND() limit "+rows); }
        public static DataTable Discountoutlets(int rows) { return Jewar.CodeLibrary.DBHandler.GetData("select * from outlets where is_reservation=1 and reservation_discount>0 "+CityCheck()+" order by RAND() limit " + rows); }

        public static DataTable DiscountDelivery(int rows) { return Jewar.CodeLibrary.DBHandler.GetData("select * from outlets where is_delivers=1 and delivery_discount>0 " + CityCheck() + " order by RAND() limit " + rows); }

        public static string CityCheck() { return Process.CityName == "Pakistan" ? "" : "and city='"+Process.CityName+"'"; }
        
        //Load outlets with reservations & delivery facilities
        public static DataTable outlets() {

            return RefineTable(Alloutlets(), "is_reservation=1 or is_delivers=1", "name");
        }

        public static DataTable Reservationoutlets()
        {
            return RefineTable(OutletByCity(Process.CityName), "is_reservation=1");
        }

        public static DataTable Deliveryoutlets()
        {
            return RefineTable(OutletByCity(Process.CityName), "is_delivers=1");
        }

        //Load outlets with reservations & delivery facilities
        public static DataTable Suggestoutlets(string cuisines,string area,string budget,string outletid)
        {
            string DefaultClause = "is_reservation=1 and city='" + Process.CityName + "' and id<>'"+outletid+"'";
            
            //Generate clause for cuisines
            string[] AllCuisine = cuisines.Split(',');
            string c = " AND (";
            for (int i = 0; i < AllCuisine.Length; i++)
            {
                c += " cuisines like '%" + AllCuisine[i] + "%' OR ";
            }
            c += ")";
            c = c.Replace("OR )", ")");
            
            string Clauses1 = " AND outlet_area LIKE '%"+area+"%' "+c+" AND budget LIKE '%"+budget+"%'";
            string Clauses2 = " AND outlet_area LIKE '%" + area + "%'" + c;
            string Clauses3 = c;
            
            DataTable dtSuggestOutlet= RefineTable(Alloutlets(),DefaultClause+Clauses1);
            
            if (dtSuggestOutlet.Rows.Count == 0)
            { dtSuggestOutlet = RefineTable(Alloutlets(), DefaultClause + Clauses2); }

            if (dtSuggestOutlet.Rows.Count == 0)
            { dtSuggestOutlet = RefineTable(Alloutlets(), DefaultClause + Clauses3); }

            if (dtSuggestOutlet.Rows.Count == 0)
            { dtSuggestOutlet = Discountoutlets(6); }

            return dtSuggestOutlet.Rows.Count > 0 ? dtSuggestOutlet.Rows.Cast<DataRow>().Take(6).CopyToDataTable() : dtSuggestOutlet; ;

        }

        //Load outlets with reservations & delivery facilities
        //public static DataTable Suggestoutlets(string area, string outletid)
        public static DataTable Suggestoutlets(string cuisines, string outletid)
        {
            //string DefaultClause = "is_delivers=1 and delivery_localities like '%" + area + "%' and id<>'" + outletid + "' and city like '"+Process.CityName+"'";
            //string DefaultClause = "delivery_localities like '%dha%' and id<>'" + outletid + "'";v
            
            string[] s = cuisines.Split(',');

            cuisines = " and cuisines like '%" + cuisines + "%'";
            
            if (cuisines.Contains(","))
            {
                cuisines = " and (";
                for (int a = 0; a < s.Length; a++)
                {
                    cuisines += " cuisines like '%" + s[a] + "%' OR";
                }
                cuisines += ")";
                cuisines = cuisines.Replace("OR)", ")");            
            }
            

            //string DefaultClause = "is_delivers=1 and delivery_localities like '%" + area + "%' and id<>'" + outletid + "' and city like '" + Process.CityName + "'";
            string DefaultClause = "is_delivers=1 " + cuisines + " and id<>'" + outletid + "' and city like '" + Process.CityName + "'";

            DataTable dtSuggestOutlet = RefineTable(Alloutlets(), DefaultClause);

            return dtSuggestOutlet.Rows.Count > 0 ? dtSuggestOutlet.Rows.Cast<DataRow>().Take(6).CopyToDataTable() : dtSuggestOutlet; ;

        }

        //Load all distinct areas from outlets
        public static DataTable Activecities()
        {
            return Jewar.CodeLibrary.DBHandler.GetDataCache("select distinct city as name from outlets where is_reservation=1 or is_delivers =1 order by city", "Activecities");
        }
        
        //Load all distinct areas from outlets
        public static DataTable Areas() {

            //return Jewar.CodeLibrary.DBHandler.GetDataCache("select distinct outlet_area, city from outlets where is_reservation=1 order by outlet_area", "Areas");
            return Jewar.CodeLibrary.DBHandler.GetDataCache("SELECT a.id,a.name as outlet_area,a.parentid,b.name AS city FROM cities AS a,cities AS b  WHERE a.levelno=1 AND a.parentid=b.id", "Areas");            
            
        }

        //Load outlets by SEOID
        public static DataTable OutletByID(string SEOID)
        {
            //TODO: What do have the city mentiond here
            return RefineTable(Alloutlets(),"slug like'%" + SEOID + "' and city='" + HttpContext.Current.Request["city"] + "'");
        }

        //Load outlets by Old Slug SEOID
        public static DataTable OutletByIDOLD(string SEOID)
        {
            //TODO: What do have the city mentiond here
            return RefineTable(Alloutlets(), "old_slug like'%" + SEOID + "' and city='" + HttpContext.Current.Request["city"] + "'");
        }

        //Load outlets by ID
        public static DataTable OutletByIDNum(string ID)
        {
            //TODO: What do have the city mentiond here
            return RefineTable(Alloutlets(), "id ='" + ID + "'");
        }

        public static DataTable OutletByName(string name)
        {
            //TODO: What do have the city mentiond here
            return RefineTable(Alloutlets(), "name like '" + name + "%'");
        }

        //Load outlets by City
        public static DataTable OutletByCity(string City)
        {
            string where = "";
            if (City != "Pakistan")
            {
                where = "city='" + City + "'";
            }
            return RefineTable(outlets(),where);
        }

        //Load outlets by City
        public static DataTable AllOutletByCity(string City)
        {
            string where = "";
            if (City != "Pakistan")
            {
                where = "city='" + City + "'";
            }
            return RefineTable(Alloutlets(), where);
        }

        //load outlet by Alphabit
        public static DataTable OutletByChar(string character)
        {
            string city = Process.CityName != "Pakistan" ? "city='" + Process.CityName + "' and (" : "(";
            string whereClause = city+" name like'" + character + "%')";
            if (character == "0-9")
            {
                whereClause = Process.CityName!="Pakistan"?"city='" + Process.CityName + "' and (":"(";
                for (int a = 0; a <= 9; a++)
                {
                    whereClause += "name like'" + a + "%' or ";
                }
                whereClause += ")";
                whereClause = whereClause.Replace("or )",")");
            }
            return RefineTable(Alloutlets(),whereClause);
        }

        //load area by Alphabit
        public static DataTable AreaByChar(string character)
        {
            string city = Process.CityName != "Pakistan" ? "city='" + Process.CityName + "' and (" : "(";
            string whereClause = city + " outlet_area like'" + character + "%')";
            if (character == "0-9")
            {
                whereClause = Process.CityName != "Pakistan" ? "city='" + Process.CityName + "' and (" : "(";
                for (int a = 0; a <= 9; a++)
                {
                    whereClause += "outlet_area like'" + a + "%' or ";
                }
                whereClause += ")";
                whereClause = whereClause.Replace("or )", ")");
            }
            
            return RefineTable(Areas(),whereClause);
        }

        //Load Areas by City
        public static DataTable AreasByCity(string City)
        {
            return City != "Pakistan" ? RefineTable(Areas(), "city='" + City + "'") : Areas();
        }

        //Load selected Keyword
        public static DataTable RefineKeyword(string Keyword)
        {
            return RefineTable(Seo(),"slug like '%" + Keyword.Replace("'", "''") + "%'");
        }

        //Load reviews by Outlet
        public static DataTable ReviewByOutlet(string OutletID)
        {
            DataTable dtreviews = RefineTable(reviews(), "outlet_id='" + OutletID + "'", "created Desc");
            return dtreviews;
           // return dtreviews.Rows.Count>0?dtreviews.Rows.Cast<DataRow>().Take(10).CopyToDataTable():dtreviews;
        }

        //Load reviews by Outlet
        public static DataTable ImagesByOutletID(string OutletID)
        {
            return RefineTable(shots(),"outlet_id='" + OutletID + "'", "order Desc");
        }

        //Load taxonomy by name
        public static DataTable Gettaxonomy(string TaxName)
        {
            return RefineTable(taxonomy(),"name='" + TaxName + "' and value<>''", "value");
        }

        public static string GetTaxValue(string TaxName)
        {
            DataTable dtTax=RefineTable(taxonomy(), "name='" + TaxName + "' and value<>''", "value");
            return dtTax.Rows.Count>0 ? dtTax.Rows[0]["value"].ToString() : "";
        }

        //Load Category by name
        //public static DataTable MenuByCategory(string Category)
        //{
        //    // Commented by Junaid Hassan
        //    // Task ID : EAT-173
        //    // return RefineTable(Menu(), "category='" + Category + "'","name");
        //    return RefineTable(Menu(), "category='" + Category + "'", "order, name");
        //}


        //Load Itemsize by menuitem id
        public static DataTable menuitemsizes(string MenuItemID, string CatIsOpen)
        {
            return Jewar.CodeLibrary.DBHandler.GetData("Select *,'" + CatIsOpen + "' as CatIsOpen from menusizes where MenuItemID='" + MenuItemID + "' ORDER BY Price, ID");
        }

        /// <summary>
        ///  Added By Junaid Hassan. 
        ///  Get the menuItem by Category ID no other efficiency
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public static DataTable MenuByCategoryID(string CategoryID, string MenuID, string CatIsOpen)
        {
            /// ////////////////////////////////////////////////////////////// START ////////////////////////////////////////////////////////////// 
            /// modified by     : Junaid Hassan
            /// Dated           : 20140217
            /// Purpose         : We need to calculate top 10 faviorite Item in a menu 
            /// JIRA            : EAT-376
            // OLD Query // Modified as bellow on 2014-02-17 return Jewar.CodeLibrary.DBHandler.GetData(string.Format("Select MenuID, ID, `Name`, Description, CategoryID, OrderCount, IsActive,`Order` FROM menuitems where CategoryID ={0} AND MenuID = {1} AND ISActive =1 ORDER BY `Order`,`name`", CategoryID, MenuID));

            return Jewar.CodeLibrary.DBHandler.GetData(string.Format("SELECT " +
                                                                "MenuID, a.ID, `Name`, Description, CategoryID, IsActive,`Order` , case when a.IsItemImageActive is TRUE then a.ItemImage else '' end as ItemImage, a.IsItemImageActive, a.Serving  " +
                                                                ", IFNULL(CAST(OrderCount AS UNSIGNED),0) AS OrderCount " +
                                                                ", '" + CatIsOpen + "' AS CatIsOpen , '' as ItemOptions , CAST(CASE WHEN IFNULL(( SELECT COUNT(ID) FROM optiongroup WHERE menuitemid = a.id) , 0) > 0 THEN IFNULL(( SELECT COUNT(ID) FROM optiongroup WHERE menuitemid = a.id) , 0) ELSE IFNULL(( SELECT COUNT(ID) FROM optiongroupadditional WHERE menuitemid = a.id) , 0) END AS UNSIGNED) AS OptionGroupCount " +
                // ", ( SELECT CASE WHEN IsGlobal =1 THEN 'global' ELSE '' END FROM menucategories C  WHERE C.ID = CategoryID ) AS GlobalItemClass "+
                                                                "FROM ( " +

                                                                    "SELECT MenuID, ID, `Name`, Description, CategoryID, IsActive,`Order`, ItemImage, IsItemImageActive, Serving " +
                                                                    "FROM menuitems " +
                                                                    "WHERE  MenuID = {1} " +
                                                                        "AND CategoryID ={0} " +

                                                                        "AND ISActive =1 ) " +
                                                                        "a " +
                                                                        "LEFT JOIN " +
                                                                    "( " +

                                                                        "SELECT ID " +
                //////////////////////////////////////////////
                // I am not using formula to calculate as we don't need at this moment 
                // Following formula can calculate the item Ordered precentage
                // ", ( (ORDERCount * 100 ) / (SELECT SUM(ORDERCount) FROM menuitems m WHERE m.menuID IN ({1}) AND IsActive=1  )  ) AS OrderCount " +
                ///////////////////////////////////////
                                                                            ",ORDERCount " +
                                                                        "FROM menuitems " +
                                                                        "WHERE menuID IN ({1}) " +
                                                                        " AND CategoryID IN (SELECT ID FROM menucategories WHERE menuID IN ({1}) AND ISActive = 1 ) " +
                                                                        "AND IsActive=1 " +
                                                                        "ORDER BY OrderCount " +
                                                                        "DESC LIMIT 0, 10 " +
                                                                    ") b ON a.ID = b.ID " +

                                                                    "ORDER BY `Order`,`name`", CategoryID, MenuID));
            /// ////////////////////////////////////////////////////////////// END ////////////////////////////////////////////////////////////// 
        }


        public static DataTable topTenFavoriteMenuItemByoutletID(string outletID)
        {
            return Jewar.CodeLibrary.DBHandler.GetData(string.Format("SELECT ID , IFNULL(CAST(( ORDERCount / (SELECT COUNT(ID) FROM menuitems m WHERE m.menuID IN (124) ) * 100 )AS UNSIGNED),0 )AS OrderCount " +
                                                    ", OrderCount AS TOrderCount " +
                                                    "FROM menuitems " +
                                                    "WHERE menuID IN ( " +
                                                        "SELECT ID FROM menus WHERE outletID =  {0} " +
                                                        "AND isActive =1 " +
                                                    ") " +
                                                    "AND OrderCount IS NOT NULL " +
                                                    "AND isActive =1 " +
                                                    "ORDER BY " +
                                                    "OrderCount DESC LIMIT 0, 10", outletID));
        }

        public static DataTable Brands(string City) 
        {
            DataTable dtBrands = Jewar.CodeLibrary.DBHandler.GetDataCache("SELECT vendor_id,NAME,COUNT(NAME),city FROM outlets where city like '%"+City+"%' GROUP BY vendor_id HAVING COUNT(NAME) > 1","Brands");
            foreach (DataRow r in dtBrands.Rows)
            {
                r["name"] = r["name"].ToString().Split(',')[0];
            }
            return RefineTable(dtBrands, "", "name", "name");
        }

        public static DataTable Refiners()
        {
            return RefineTable(taxonomy(), "name='facilities' or  name='meal_times' or name='cuisines' or name='budget' ");
        }

        /// <summary>
        /// Helper methods for appdb.
        /// </summary>
        //Return datatable by tablename
        private static DataTable GetTable(string TableName)
        {
            return Jewar.CodeLibrary.DBHandler.GetDataCache(string.Format("select * from {0}", TableName), TableName);
        }
        //Return datatable by tablename & condition
        private static DataTable GetTable(string TableName,string WhereClause)
        {
            return Jewar.CodeLibrary.DBHandler.GetDataCache(string.Format("select * from {0} where {1}", TableName,WhereClause), TableName+WhereClause);
        }
        //refine datatable with where clause
        public static DataTable RefineTable(DataTable dt, string WhereClause)
        {
            DataTable dtFiltered = dt.Select(WhereClause).Count() > 0 ? dt.Select(WhereClause).CopyToDataTable() : dt.Clone();
            return dtFiltered;
        }
        //refine datatable with where clause and sorting
        public static DataTable RefineTable(DataTable dt, string WhereClause, string Sort)
        {
            DataTable dtFiltered = dt.Select(WhereClause).Count() > 0 ? dt.Select(WhereClause, Sort).CopyToDataTable() : dt.Clone();
            return dtFiltered;
        }
        //refine datatable with where clause, sorting & distinct column
        public static DataTable RefineTable(DataTable dt, string WhereClause, string Sort, string DistinctColumns)
        {
            DataTable dtFiltered = dt.Select(WhereClause).Count() > 0 ? dt.Select(WhereClause, Sort).CopyToDataTable() : dt.Clone();
            DataTable dtDistinct = dtFiltered.Rows.Count > 0 ? new DataView(dtFiltered).ToTable(true, DistinctColumns) : dt.Clone();
            return dtDistinct;
        }

        /// <summary>
        /// Added by        : Junaid Hassan
        /// Dated           : 2014-04-21
        /// Purpose         : To Apply Natural sorting on Array.
        /// TaskId          : It can be used as general but specifically generated for EAT-513
        /// </summary>
        public class AlphanumComparatorFast : IComparer
        {
            public int Compare(object x, object y)
            {
                string s1 = x as string;
                if (s1 == null)
                {
                    return 0;
                }
                string s2 = y as string;
                if (s2 == null)
                {
                    return 0;
                }

                int len1 = s1.Length;
                int len2 = s2.Length;
                int marker1 = 0;
                int marker2 = 0;

                // Walk through two the strings with two markers.
                while (marker1 < len1 && marker2 < len2)
                {
                    char ch1 = s1[marker1];
                    char ch2 = s2[marker2];

                    // Some buffers we can build up characters in for each chunk.
                    char[] space1 = new char[len1];
                    int loc1 = 0;
                    char[] space2 = new char[len2];
                    int loc2 = 0;

                    // Walk through all following characters that are digits or
                    // characters in BOTH strings starting at the appropriate marker.
                    // Collect char arrays.
                    do
                    {
                        space1[loc1++] = ch1;
                        marker1++;

                        if (marker1 < len1)
                        {
                            ch1 = s1[marker1];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                    do
                    {
                        space2[loc2++] = ch2;
                        marker2++;

                        if (marker2 < len2)
                        {
                            ch2 = s2[marker2];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                    // If we have collected numbers, compare them numerically.
                    // Otherwise, if we have strings, compare them alphabetically.
                    string str1 = new string(space1);
                    string str2 = new string(space2);

                    int result;

                    if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                    {
                        int thisNumericChunk = int.Parse(str1);
                        int thatNumericChunk = int.Parse(str2);
                        result = thisNumericChunk.CompareTo(thatNumericChunk);
                    }
                    else
                    {
                        result = str1.CompareTo(str2);
                    }

                    if (result != 0)
                    {
                        return result;
                    }
                }
                return len1 - len2;
            }
        }

    }
}