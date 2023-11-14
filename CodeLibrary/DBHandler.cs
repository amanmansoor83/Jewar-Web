/**
* Copyright (c) 2013, Broadway
* All rights reserved.
* @author Yasir Ahmed <yasir@Broadway.pk>
* @version 1.0.1
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Timers;
using Npgsql;

namespace Jewar.CodeLibrary
{
    public static class DBHandler
    {
        //Connection String for MySQl Database on local 

        public static string ConnectString = Cryptography.DecryptMessage(ConfigurationManager.ConnectionStrings["MySQL"].ToString());
        public static string ConnectStringWrite = Cryptography.DecryptMessage(ConfigurationManager.ConnectionStrings["MySQL"].ToString());
        public static string ConnectStringfromReplica = Cryptography.DecryptMessage(ConfigurationManager.ConnectionStrings["mysqlarchive"].ToString());
        //Method to get datatable from SQL query with Cache
        public static DataTable GetDataCache(string SqlQuery,string CacheKey)
        {
            DataSet objDS = null;
            DataTable dsTable = null;
            try
            {
                dsTable =(DataTable)HttpContext.Current.Cache[CacheKey];
                if (dsTable == null)
                {
                    MySqlConnection objConn = new MySqlConnection(ConnectString);
                    MySqlCommand objCommand = new MySqlCommand(SqlQuery, objConn);
                    MySqlDataAdapter objSDA = new MySqlDataAdapter(objCommand);
                    objDS = new DataSet();
                    objSDA.Fill(objDS);
                    dsTable = (DataTable)objDS.Tables[0];
                    EnableCaching(dsTable, CacheKey);


                    try
                    {

                        //long a = DBHandler.InsertDataWithIDForOrder(string.Format("insert into apicalllogs(APIName, Platform,Created) values('{0}','{1}','{2}')", CacheKey, "", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")));

                    }
                    catch (Exception ee)
                    { }

                }
                else
                {
                    if (dsTable != null)
                    {
                        if (dsTable.Rows.Count == 0)
                        {
                            MySqlConnection objConn = new MySqlConnection(ConnectString);
                            MySqlCommand objCommand = new MySqlCommand(SqlQuery, objConn);
                            MySqlDataAdapter objSDA = new MySqlDataAdapter(objCommand);
                            objDS = new DataSet();
                            objSDA.Fill(objDS);
                            dsTable = (DataTable)objDS.Tables[0];
                            EnableCaching(dsTable, CacheKey);


                            try
                            {

                                //long a = DBHandler.InsertDataWithIDForOrder(string.Format("insert into apicalllogs(APIName, Platform,Created) values('{0}','{1}','{2}')", CacheKey, "", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")));

                            }
                            catch (Exception ee)
                            { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write(SqlQuery);
                HttpContext.Current.Response.Write(ex.Message);
            }

            return dsTable;
        }

        public static DataSet GetDataCacheinDataSet(string SqlQuery, string CacheKey)
        {
            DataSet objDS = null;
            //DataTable dsTable = null;
            try
            {
                objDS = (DataSet)HttpContext.Current.Cache[CacheKey];
                if (objDS == null)
                {
                    MySqlConnection objConn = new MySqlConnection(ConnectString);
                    MySqlCommand objCommand = new MySqlCommand(SqlQuery, objConn);
                    MySqlDataAdapter objSDA = new MySqlDataAdapter(objCommand);
                    objDS = new DataSet();
                    objSDA.Fill(objDS);
                    //dsTable = objDS.Tables[0];
                    EnableCachingDataset(objDS, CacheKey);


                    try
                    {

                        //long a = DBHandler.InsertDataWithIDForOrder(string.Format("insert into apicalllogs(APIName, Platform,Created) values('{0}','{1}','{2}')", CacheKey, "", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")));

                    }
                    catch (Exception ee)
                    { }

                }
                else
                {
                    if (objDS != null)
                    {
                        if (objDS.Tables[0].Rows.Count == 0)
                        {
                            MySqlConnection objConn = new MySqlConnection(ConnectString);
                            MySqlCommand objCommand = new MySqlCommand(SqlQuery, objConn);
                            MySqlDataAdapter objSDA = new MySqlDataAdapter(objCommand);
                            objDS = new DataSet();
                            objSDA.Fill(objDS);
                            //dsTable = objDS.Tables[0];
                            EnableCachingDataset(objDS, CacheKey);


                            try
                            {

                                //long a = DBHandler.InsertDataWithIDForOrder(string.Format("insert into apicalllogs(APIName, Platform,Created) values('{0}','{1}','{2}')", CacheKey, "", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")));

                            }
                            catch (Exception ee)
                            { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(SqlQuery);
                HttpContext.Current.Response.Write(ex.Message);
            }

            return objDS;
        }


        //Method to get datatable from SQL query
        public static DataTable GetData(string SqlQuery)
        {
          
            DataSet objDS = null;
            DataTable dsTable = null;
            MySqlConnection objConn = new MySqlConnection(ConnectString);

            try
            {
                //System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["DefaultCode"]));

                MySqlCommand objCommand = new MySqlCommand(SqlQuery, objConn);
                
                MySqlDataAdapter objSDA = new MySqlDataAdapter(objCommand);
                objDS = new DataSet();
                objConn.Open();
                objSDA.Fill(objDS);
                objConn.Close();
                dsTable = objDS.Tables[0];
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }

            return dsTable;
        }

     
        
     

        public static DataTable GetDatafromReplica(string SqlQuery)
        {

            DataSet objDS = null;
            DataTable dsTable = null;
            MySqlConnection objConn = new MySqlConnection(ConnectStringfromReplica);

            try
            {
                //System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["DefaultCode"]));

                MySqlCommand objCommand = new MySqlCommand(SqlQuery, objConn);

                MySqlDataAdapter objSDA = new MySqlDataAdapter(objCommand);
                objDS = new DataSet();
                objConn.Open();
                objSDA.Fill(objDS);
                objConn.Close();
                dsTable = objDS.Tables[0];
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }

            return dsTable;
        }


        /// Added By Junaid Hassan 
        /// Added intCommandTimeout Param to handle Queries that took longer time than Normal.
        //Method to get datatable from SQL query
        public static DataTable GetData(string SqlQuery, int intCommandTimeout)
        {
            DataSet objDS = null;
            DataTable dsTable = null;
            MySqlConnection objConn = new MySqlConnection(ConnectString);

            try
            {

                MySqlCommand objCommand = new MySqlCommand(SqlQuery, objConn);

                objCommand.CommandTimeout = intCommandTimeout;

                MySqlDataAdapter objSDA = new MySqlDataAdapter(objCommand);
                objDS = new DataSet();
                objConn.Open();
                objSDA.Fill(objDS);
                objConn.Close();
                dsTable = objDS.Tables[0];
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }

            return dsTable;
        }

        public static DataSet GetDatainDataSet(string SqlQuery)
        {
            DataSet objDS = null;
            DataTable dsTable = null;
            MySqlConnection objConn = new MySqlConnection(ConnectString);

            try
            {

                MySqlCommand objCommand = new MySqlCommand(SqlQuery, objConn);

                //objCommand.CommandTimeout = intCommandTimeout;

                MySqlDataAdapter objSDA = new MySqlDataAdapter(objCommand);
                objDS = new DataSet();
                objConn.Open();
                objSDA.Fill(objDS);
                objConn.Close();
                dsTable = objDS.Tables[0];
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }

            return objDS;
        }


        //Method to insert record using SQL Insert statment
        public static int InsertData(string CommandName)
        {
            int result = 0;
            MySqlConnection objConn = new MySqlConnection(ConnectStringWrite);
            try
            {


                if (Convert.ToBoolean(HttpContext.Current.Session["Write"]))
                {

                    if (objConn.State == ConnectionState.Closed)
                        objConn.Open();
                    MySqlCommand objCommand = new MySqlCommand(CommandName, objConn);
                    result = objCommand.ExecuteNonQuery();
                }
                else
                {
                    if (objConn.State != ConnectionState.Closed)
                    {
                        objConn.Close();
                        objConn.Dispose();
                    }

                    HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings["tempURL"] + "NotAuthorized.aspx", false);
                    //AdminMaster obj = new AdminMaster();
                    //HtmlGenericControl a = (HtmlGenericControl)obj.FindControl("divAlert");
                    //a.Visible = true;
                    //HttpContext.Current.Response.Write("<script> alert('You are not Allowed to made changes'); </script>");
                }

            }
            catch (Exception ex)
            {
                int a = InsertData("insert into errorlog(Error, Detail) values('Error in Admin InsertData()', '" + CommandName.Replace("'","") + "')");
                HttpContext.Current.Response.Write(ex.Message);
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            return result;

        }

        public static int InsertData(string CommandName, bool returnid)
        {
            int result = 0;
            MySqlConnection objConn = new MySqlConnection(ConnectStringWrite);
            try
            {
                if (objConn.State == ConnectionState.Closed)
                    objConn.Open();
                MySqlCommand objCommand = new MySqlCommand(CommandName, objConn);
                result = objCommand.ExecuteNonQuery();
                if (returnid)
                    result = Int32.Parse(objCommand.LastInsertedId.ToString());

            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            return result;

        }

        //Method to insert record using SQL Insert statment return last InseertedID
        public static long InsertDataGetInsertedID(string CommandName)
        {
            long result = 0;
            MySqlConnection objConn = new MySqlConnection(ConnectStringWrite);
            try
            {

                if (objConn.State == ConnectionState.Closed)
                    objConn.Open();
                MySqlCommand objCommand = new MySqlCommand(CommandName, objConn);
                result = objCommand.ExecuteNonQuery();
                result = objCommand.LastInsertedId;

                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            catch (Exception ex)
            {

                HttpContext.Current.Response.Write(ex.Message);
            }

            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            return result;

        }

        //Method to insert record using SQL Insert statment
        public static int InsertDataWithID(string CommandName)
        {
            int result = 0;
            MySqlConnection objConn = new MySqlConnection(ConnectStringWrite);
            try
            {
               
                if (objConn.State == ConnectionState.Closed)
                    objConn.Open();
                MySqlCommand objCommand = new MySqlCommand(CommandName, objConn);
                result = objCommand.ExecuteNonQuery();
                result = Convert.ToInt32(objCommand.LastInsertedId); 
            }
            catch (Exception ex)
            {
                log.createerrorlog("", "DBHandler/InsertDataWithID Failed Via Web", ex.Message + CommandName);
                HttpContext.Current.Response.Write(ex.Message); 
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            return result;

        }

        public static int InsertDataForOrderWithoutTimeOut(string CommandName)
        {
            int result = 0;
            //try
            //{
            MySqlConnection objConn = new MySqlConnection(ConnectStringWrite);
            if (objConn.State == ConnectionState.Closed)
                objConn.Open();
            MySqlCommand objCommand = new MySqlCommand(CommandName, objConn);
            objCommand.CommandTimeout = 0;

            result = objCommand.ExecuteNonQuery();
            objConn.Close();
            //}
            //catch (Exception ex)
            //{
            //    log.createerrorlog("", "InsertDataForOrder", ex.Message + " -Seperation- " + CommandName);
            //    HttpContext.Current.Response.Write(ex.Message);
            //}
            return result;

        }


        //Apply Caching on Datatable
        private static void EnableCaching(DataTable dtToCache, string CacheKey)
        {
            try
            {
                HttpContext.Current.Cache[CacheKey] = dtToCache;
                //HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddHours(1));
                HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddMinutes(15));
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Public);
                HttpContext.Current.Response.Cache.SetSlidingExpiration(true);
            }
            catch (Exception ex)
            {

            }

        }


        private static void EnableCachingDataset(DataSet dtToCache, string CacheKey)
        {
            try
            {
                HttpContext.Current.Cache[CacheKey] = dtToCache;
                HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddHours(1));
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Public);
                HttpContext.Current.Response.Cache.SetSlidingExpiration(true);
            }
            catch (Exception ex)
            {

            }

        }

        //Apply Caching on Datatable with Cache Expire Time
        private static void EnableCaching(DataTable dtToCache, string CacheKey, int CacheHours)
        {
            try
            {
                HttpContext.Current.Cache[CacheKey] = dtToCache;
                HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddHours(CacheHours));
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Public);
                HttpContext.Current.Response.Cache.SetSlidingExpiration(true);
            }
            catch (Exception ex)
            {

            }

        }

        //Apply QueryString Base Paging on DataTable & Return HTML or Paging Numbers
        public static DataTable ApplyPaging(DataTable dtt, out string PagingText, int pagesize, string TablePrefix)
        {
            try
            {
                string PageNumber = TablePrefix + "_PageNo";
                DataTable dtpage = new DataTable();
                dtpage = dtt.Clone();

                int page_ = 0;
                if (HttpContext.Current.Request.RawUrl.IndexOf(PageNumber) < 1)
                {
                    page_ = 0;
                }
                else
                {
                    page_ = Int32.Parse(HttpContext.Current.Request.QueryString.Get(PageNumber));
                }

                int start = page_ * pagesize;
                int end = start + pagesize;

                if (end > dtt.Rows.Count)
                {
                    end = dtt.Rows.Count;
                }

                for (int a = start; a <= end - 1; a++)
                {
                    DataRow dr = dtt.Rows[a];
                    dtpage.ImportRow(dr);
                }

                // if (dtt.Rows.Count > 0)
                // PagingText = "";
                double till = (dtt.Rows.Count) / pagesize;
                if ((dtt.Rows.Count % pagesize) == 0)
                {
                    till = till - 1.0;
                }

                int newtill = 0;
                if ((page_ + 4) > till)
                {
                    newtill = Convert.ToInt32(till);
                }
                else
                {
                    newtill = page_ + 4;
                }

                int startp = 0;
                if (page_ - 3 > 0)
                {
                    startp = page_ - 3;
                }

                int newtill2 = startp + 4;
                if (newtill2 > Convert.ToInt32(till))
                {
                    newtill2 = newtill;
                }
                int counter = 0;
                PagingText = "";

                if (newtill2 <= 4)
                {
                    //newtill2 = newtil;
                    startp = 0;
                }
                else
                {

                }
                for (int i = startp; i <= newtill2; i++)
                {

                    int pgno = i + 1;
                    string oldurl = HttpContext.Current.Request.RawUrl.Replace("&" + PageNumber + "=" + HttpContext.Current.Request.QueryString.Get(PageNumber), "");
                    if (oldurl.IndexOf("?") < 0)
                    {
                        oldurl += "?";
                    }
                    string url = oldurl + "&" + PageNumber + "=" + i;
                    string selected = "";

                    if (dtt.Rows.Count > 0)
                    {
                        ++counter;
                        if (HttpContext.Current.Request.RawUrl.IndexOf(PageNumber) > 0)
                        {
                            int currpage = Int32.Parse(HttpContext.Current.Request.QueryString.Get(PageNumber)) + 1;
                            if (pgno == currpage)
                            {
                                selected = " class=\"Active\"";
                            }
                        }
                        else
                        {
                            if (counter == 1)
                            {
                                selected = " class=\"Active\"";
                            }
                        }
                        PagingText = PagingText + "<li><a href='" + url + "'>" + pgno + "</a></li>";
                    }
                }

                PagingText += "";
                if (HttpContext.Current.Request.RawUrl.IndexOf(PageNumber) > 0)
                {
                    int back = Int32.Parse(HttpContext.Current.Request.QueryString.Get(PageNumber)) - 1;
                    string urlback = HttpContext.Current.Request.RawUrl.ToString().Replace("&" + PageNumber + "=" + HttpContext.Current.Request.QueryString.Get(PageNumber), "&" + PageNumber + "=" + back);
                    if (back < 0)
                    {
                        urlback = "#";
                    }

                    int fwd = Int32.Parse(HttpContext.Current.Request.QueryString.Get(PageNumber)) + 1;
                    string urlfwd = HttpContext.Current.Request.RawUrl.ToString().Replace("&" + PageNumber + "=" + HttpContext.Current.Request.QueryString.Get(PageNumber), "&" + PageNumber + "=" + fwd);
                    if (fwd > Int32.Parse(till.ToString()))
                    {
                        urlfwd = "#";
                    }

                    int first = Int32.Parse(HttpContext.Current.Request.QueryString.Get(PageNumber)) - 1;
                    string urlfirst = HttpContext.Current.Request.RawUrl.ToString().Replace("&" + PageNumber + "=" + HttpContext.Current.Request.QueryString.Get(PageNumber), "&" + PageNumber + "=0");
                    if (first < 0)
                    {
                        urlfirst = "#";
                    }

                    int last = Int32.Parse(HttpContext.Current.Request.QueryString.Get(PageNumber)) + 1;

                    //Commented and added by aman mansoor on 05-Dec-2013 to fix the Last>> in paging EAT-105                   

                    int count = dtt.Rows.Count / 100;
                    decimal dccount = Convert.ToDecimal(dtt.Rows.Count) / 100;

                    if (dccount == count)
                    {
                        count = count - 1;
                    }
                    newtill2 = count;

                    //string urllast = HttpContext.Current.Request.RawUrl.ToString().Replace("&" + PageNumber + "=" + HttpContext.Current.Request.QueryString.Get(PageNumber), "&" + PageNumber + "=" + newtill2);
                    string urllast = HttpContext.Current.Request.RawUrl.ToString().Replace("&" + PageNumber + "=" + HttpContext.Current.Request.QueryString.Get(PageNumber), "&" + PageNumber + "=" + newtill2);
                    //Commented and added end by aman mansoor on 05-Dec-2013
                    
                    if (last > Int32.Parse(newtill2.ToString()))
                    {
                        urllast = "#";
                    }


                    string firstbtn = "<li><a href='" + urlfirst + "'>첛irst</a></li>";
                    string prebtn = "<li><a href='" + urlback + "'>첧revious</a></li>";
                    string nextbtn = "<li><a href='" + urlfwd + "'>Next�</a></li>";
                    string lastbtn = "<li><a href='" + urllast + "'>Last�</a></li>";
                   
                   


                    PagingText = firstbtn + prebtn + " " + PagingText + " " + nextbtn + lastbtn;
                }
                else
                {
                    string urls = HttpContext.Current.Request.RawUrl.ToString();
                    if (urls.IndexOf("?") < 0)
                    {
                        urls = urls + "?";
                    }
                    string FirstPage = "<li><a href='" + urls + "&" + PageNumber + "=0'>첛irst</a></li>";
                    string prebtn = "<li><a href='" + urls + "&" + PageNumber + "=0'>첧revious</a></li>";
                    string nextpage = "#";
                    if (dtt.Rows.Count > pagesize)
                    {
                        nextpage = urls + "&" + PageNumber + "=1";
                    }
                    string nextbtn = "<li><a href='" + nextpage + "'>Next�</a></li>";

                    //Commented and added by aman mansoor on 05-Dec-2013 to fix the Last>> in paging EAT-105
                    //string LastPage = "<li><a href='" + urls + "&" + PageNumber + "=" + newtill2 + "'>Last�</a></li>";

                    int count = dtt.Rows.Count / 100;
                    decimal dccount = Convert.ToDecimal(dtt.Rows.Count) / 100;

                    if (dccount == count)
                    {
                        count = count - 1;
                    }

                    string LastPage = "<li><a href='" + urls + "&" + PageNumber + "=" + count + "'>Last�</a></li>";
                    //Commented and added end by aman mansoor on 05-Dec-2013

                    PagingText = "" + FirstPage + prebtn + " " + PagingText + " " + nextbtn + LastPage;

                }

                PagingText = "<div class='dataTables_paginate paging_bootstrap pagination'><ul>" + PagingText + "</ul></div>";
                PagingText = PagingText.Replace("<a href", "<a data-toggle=\"paging\" href");
                if (pagesize > dtt.Rows.Count)
                {
                    PagingText = "";
                }
                return dtpage;
            }
            catch (Exception ee)
            {
                PagingText = "";
                return null;

            }


        }

        //Fill Dropdown with DataTable
        public static void FillDropDown(DataTable dtData, DropDownList DDL, string ColumnText, string ColumnValue)
        {
            try
            {

                DDL.DataSource = dtData;
                DDL.DataTextField = dtData.Columns[ColumnText].ToString();
                DDL.DataValueField = dtData.Columns[ColumnValue].ToString();
                DDL.DataBind();
                DDL.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {

            }

        }

        //Fill Dropdown with DataTable
        public static void FillDropDownWithoutSelection(DataTable dtData, DropDownList DDL, string ColumnText, string ColumnValue)
        {
            try
            {

                DDL.DataSource = dtData;
                DDL.DataTextField = dtData.Columns[ColumnText].ToString();
                DDL.DataValueField = dtData.Columns[ColumnValue].ToString();
                DDL.DataBind();
            }
            catch (Exception ex)
            {

            }

        }

        //Fill Dropdown with DataTable
        public static void FillDropDown(ListBox DDL, string taxonomy)
        {
            try
            {
                DataTable dtData = GetData("select * from taxonomy where name = '" + taxonomy + "' order by value");

                
                DDL.DataSource = dtData;
                DDL.DataTextField = dtData.Columns["value"].ToString();
                DDL.DataValueField = dtData.Columns["value"].ToString();
                DDL.DataBind();
                DDL.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {

            }
        }
        //Fill Dropdown with DataTable
        public static void FillDropDown(DropDownList DDL, string taxonomy)
        {
            try
            {
                DataTable dtData = GetData("select * from taxonomy where name = '" + taxonomy + "' order by value");


                DDL.DataSource = dtData;
                DDL.DataTextField = dtData.Columns["value"].ToString();
                DDL.DataValueField = dtData.Columns["value"].ToString();
                DDL.DataBind();
                DDL.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {

            }
        }

        //Fill Dropdown with DataTable
        public static void FillDropDownforinspectionreport(DropDownList DDL, string taxonomy)
        {
            try
            {
                DataTable dtData = GetData("select * from vms_taxonomy where name = '" + taxonomy + "' order by ID");


                DDL.DataSource = dtData;
                DDL.DataTextField = dtData.Columns["value"].ToString();
                DDL.DataValueField = dtData.Columns["value"].ToString();
                DDL.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public static string GetDropDownValues(ListBox lst)
        {
            string Values = "";
            try
            {
                foreach (ListItem lt in lst.Items)
                {
                    if (lt.Selected)
                    {
                        Values += lt.Value.Trim() + ",";
                    }
                }
                //This text is commented by consent of junaid hassan on 21-Apr-2014 and trim end is placed
                //Values += "#";
                //Values = Values.Replace(",#", "");
                
                Values = Values.TrimEnd(',');
            }
            catch (Exception ex)
            { }
            return Values;
        }

        public static string GetDropDownValuesString(ListBox lst)
        {
            string Values = "";
            try
            {
                foreach (ListItem lt in lst.Items)
                {
                    if (lt.Selected)
                    {
                        Values += "'" + lt.Value.Trim() + "'" + ",";
                    }
                }
                //This text is commented by consent of junaid hassan on 21-Apr-2014 and trim end is placed
                //Values += "#";
                //Values = Values.Replace(",#", "");

                Values = Values.TrimEnd(',');
            }
            catch (Exception ex)
            { }
            return Values;
        }

        public static void SetDropDownValues(ListBox lst,string s)
        {
            string[] sList = s.Split(',');
            for (int a = 0; a < sList.Length; a++)
            {
                lst.Items.FindByValue(sList[a].Trim()).Selected = true;
            }
        }

        //Set Multiple selected values to drop down (For Drop Down Only)
        public static void SetDropDownValues(DropDownList lst, string s)
        {
            string[] sList = s.Split(',');
            for (int a = 0; a < sList.Length; a++)
            {
                lst.Items.Add(sList[a].Trim());
            }
        }

        public static DataTable CheckUserRights(int UserID , string PageName)
        {
            //string UserQuery = string.Format("select Designation from users where ID = '{0}'", UserID);
            //DataTable dtUser = DBHandler.GetData(UserQuery);

            //string PageQuery = string.Format("select Modules from pages where page_name = '{0}'", PageName);
            //DataTable dtPage = DBHandler.GetData(PageQuery);

            string PermissionQuery = string.Format("SELECT rm.* FROM rolesmodules rm INNER JOIN pages p ON rm.modules = p.module INNER JOIN users u ON rm.roles = u.Type WHERE u.Id = '{0}' and p.url = '{1}' and rm.is_active = 1", UserID, PageName);
            DataTable dtPermission = DBHandler.GetData(PermissionQuery);

            return dtPermission;
        }
                 
        //get string from taxonomy
        public static string GetTaxanomyValue(string taxonomy)
        {
            string value = "";
            try
            {
                DataTable dtData = GetData("select * from taxonomy where name = '" + taxonomy + "' order by value");

                if (dtData.Rows.Count > 0)
                {
                    value = dtData.Rows[0]["value"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return value;
        }
        
        //get datatable from taxonomy
        public static DataTable GetTaxanomyTable(string taxonomy)
        {
            DataTable dtData = new DataTable();
            try
            {
                dtData = GetData("select * from taxonomy where name = '" + taxonomy + "' order by value");                
            }
            catch (Exception ex)
            {

            }
            return dtData;
        }

        //Method to insert record using SQL Insert statment
        public static int InsertDataWithoutLogin(string CommandName)
        {
            int result = 0;
            MySqlConnection objConn = new MySqlConnection(ConnectStringWrite);
            try
            {
                if (objConn.State == ConnectionState.Closed)
                    objConn.Open();
                MySqlCommand objCommand = new MySqlCommand(CommandName, objConn);
                result = objCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            return result;

        }

        //Method to insert record using SQL Insert statment
        public static int InsertDataWithoutLoginWithout(string CommandName)
        {
            int result = 0;
            MySqlConnection objConn = new MySqlConnection(ConnectStringWrite);
            try
            {
                if (objConn.State == ConnectionState.Closed)
                    objConn.Open();
                MySqlCommand objCommand = new MySqlCommand(CommandName, objConn);
                result = objCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            }
            return result;

        }

        //Update Activity log Table after every transaction
        public static int Updatelog(long ObjectID, string Type, string Remarks, string logType)
        {
            string UserID = "0";
            if (HttpContext.Current.Session["UserID"] != null)
            {
                UserID = HttpContext.Current.Session["UserID"].ToString();
            }

            string InsertSQL = string.Format("INSERT INTO `log` (`object_id`, `object_type`, `date`, `user_id`, `Remarks`, `logType`) VALUES ('{0}', '{1}', '{2}', '{3}','{4}','{5}')", ObjectID, Type, DateTime.Now.AddHours(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["AddHours"])).ToString("yyyy-MM-dd H:mm"), HttpContext.Current.Session["UserID"], Remarks, logType);
            int Insert = CodeLibrary.DBHandler.InsertDataWithoutLogin(InsertSQL);

            return Insert;
        }

        //Method to insert record using SQL Insert statment
        public static int InsertDataWithIDForOrder(string CommandName)
        {
            int result = 0;
            //try
            //{
            MySqlConnection objConn = new MySqlConnection(ConnectStringWrite);
            if (objConn.State == ConnectionState.Closed)
                objConn.Open();
            MySqlCommand objCommand = new MySqlCommand(CommandName, objConn);
            result = objCommand.ExecuteNonQuery();
            result = Convert.ToInt32(objCommand.LastInsertedId);
            objConn.Close();
            //}
            //catch (Exception ex)
            //{
            //    log.createerrorlog("", "InsertDataWithIDForOrder", ex.Message + " -Seperation- " + CommandName);
            //    HttpContext.Current.Response.Write(ex.Message);
            //}
            return result;

        }

        //Method to insert record using SQL Insert statment
        public static int InsertDataForOrder(string CommandName)
        {
            int result = 0;
            //try
            //{
            MySqlConnection objConn = new MySqlConnection(ConnectStringWrite);
            if (objConn.State == ConnectionState.Closed)
                objConn.Open();
            MySqlCommand objCommand = new MySqlCommand(CommandName, objConn);
            result = objCommand.ExecuteNonQuery();
            objConn.Close();
            //}
            //catch (Exception ex)
            //{
            //    log.createerrorlog("", "InsertDataForOrder", ex.Message + " -Seperation- " + CommandName);
            //    HttpContext.Current.Response.Write(ex.Message);
            //}
            return result;

        }
    }
}
