/**
* Copyright (c) 2013, Broadway
* All rights reserved.
* @author Yasir Ahmed <yasir@Broadway.pk>
* @version 1.0.1
*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.IO;

namespace Jewar.CodeLibrary
{
    public class ImportExport
    {
        /// <summary>
        /// Export to Excel File 
        /// How to use:
        /// DataTable table = new DataTable();        
        /// DataTableToExcel(table, Server.MapPath("~/Test.xls"));
        /// </summary>
        /// <param name="table">DataTable.</param>
        /// <param name="excelFilePath">File Path to save Excel File</param>
        /// <param name="htmlHeading">Table Heading otherwise null if not requried</param>
        public static void DataTableToExcel(DataTable table, string excelFilePath, string htmlHeading)
        {
            try
            {
                System.Web.UI.WebControls.DataGrid grid = new System.Web.UI.WebControls.DataGrid();
                grid.HeaderStyle.Font.Bold = true;
                grid.Caption = htmlHeading;
                grid.CaptionAlign = TableCaptionAlign.Left;

                grid.DataSource = table;
                grid.DataBind();

                using (System.IO.StreamWriter objStreamWriter = new System.IO.StreamWriter(excelFilePath, false, System.Text.Encoding.Unicode))
                {
                    using (HtmlTextWriter objHtmlTextWriter = new Html32TextWriter(objStreamWriter))
                    {
                        grid.RenderControl(objHtmlTextWriter);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.ImportExport.DataTableToExcel :-" + ex.Message);
            }

        }

        /// <summary>
        /// Export to .CSV file
        /// How to use:
        /// DataTable table = new DataTable();        
        /// DataTableToCSV(table, Server.MapPath("~/Test.csv"));
        /// </summary>
        /// <param name="table">Datable</param>
        /// <param name="filePath">File Path to save .CSV File</param>
        public void DataTableToCSV(DataTable table, string filePath)
        {
            try
            {
                // Create the CSV file to which dataTable will be exported.
                System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false);
                // First we will write the headers.
                System.Data.DataTable dt = table;
                int iColCount = dt.Columns.Count;
                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                // Now write all the rows.
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();

            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.ImportExport.DataTableToCSV :-" + ex.Message);
            }

        }

        /// <summary>
        /// Return Dataset
        /// How to use:
        /// ReadCSV(Server.MapPath("~/Test.csv"));
        /// </summary>
        /// <param name="FilePath">A Valid <Flie.CSV></param>
        /// <returns></returns>
        public DataSet ReadCSV(string FilePath)
        {
            DataSet ds = new DataSet();
            OleDbConnection conn = new OleDbConnection();
            try
            {
                string FileName = String.Empty;
                string query = "SELECT * FROM " + System.IO.Path.GetFileName(FilePath);
                FileName = System.IO.Path.GetFullPath(FilePath);
                FilePath = FileName.Replace(System.IO.Path.GetFileName(FilePath), null);
                string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetFullPath(FilePath) + ";" + "Extended Properties=Text;";

                conn.ConnectionString = connStr;
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                adapter.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                ds = new DataSet();
                ExceptionHandling.AddSystemerrorlog("OvrLod.ImportExport.ReadCSV :-" + ex.Message);
            }

            return ds;
        }

        /// <summary>
        /// Returns DataSet
        /// How to use:
        /// ReadXLS(Server.MapPath("~/Test.xls"));
        /// </summary>
        /// <param name="FilePath">A valid <File.XLS> </param>
        /// <param name="SheetName">Spread sheet name</param>
        /// <returns></returns>
        public DataSet ReadXLS(string FilePath, string SheetName)
        {
            DataSet ds = new DataSet();
            OleDbConnection conn = new OleDbConnection();
            try
            {
                string FileExtention = String.Empty;
                string connStr = string.Empty;
                string query = "SELECT * FROM [" + SheetName + "$]";
                FileExtention = System.IO.Path.GetExtension(FilePath);
                if (FileExtention == ".xlsx")
                {
                    connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetFullPath(FilePath) + ";" + "Extended Properties=Excel 9.0;";
                }
                else
                {
                    connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetFullPath(FilePath) + ";" + "Extended Properties=Excel 8.0;";

                    //connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetFullPath(FilePath) + ";Extended Properties=\"Excel 8.0;HDR=YES\"";

                }
                conn.ConnectionString = connStr;
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                adapter.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                ds = new DataSet();
                ExceptionHandling.AddSystemerrorlog("OvrLod.ImportExport.ReadXLS :-" + ex.Message);
            }
            return ds;
        }

        /// <summary>
        /// Created By Junaid Hassan Dated : 2013
        /// </summary>
        /// <param name="FilePathWithName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataSet ReadmyExcel(string FilePathWithName, string fileName)
        {
            String queryAll = "SELECT * FROM [" + fileName + "$]";
            String xlsPath = System.IO.Path.GetFullPath(FilePathWithName);   //Directory.GetCurrentDirectory() + "\\Bella_Vita20130805_.xls";

            String strConn;

            string FileExtention = System.IO.Path.GetExtension(xlsPath);
            if (FileExtention == ".xlsx")
            {
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + xlsPath + ";Extended Properties='Excel 9.0;IMEX=1';";

            }
            else
            {
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + xlsPath + ";Extended Properties='Excel 8.0;IMEX=1';";
            }

            DataSet dsPaidXls = new DataSet();

            try
            {
                OleDbDataAdapter m_dbDA = new OleDbDataAdapter(queryAll, strConn);

                m_dbDA.Fill(dsPaidXls, "[mySheet]");

                return dsPaidXls;
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return dsPaidXls;
            }
        }

        public static void ExportDataSetToExcel(DataTable DT, string filename)
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = DT;// ds.Tables[0];
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }

        /// <summary>
        /// Returns DataSet
        /// How to use:
        /// ReadXLS(Server.MapPath("~/Test.xls"));
        /// </summary>
        /// <param name="FilePath">A valid <File.XLS> </param>
        /// <param name="SheetName">Spread sheet name</param>
        /// <returns></returns>
        public static void DataTableToExcel(DataTable table, string htmlHeading)
        {
            try
            {
                System.Web.UI.WebControls.DataGrid grid = new System.Web.UI.WebControls.DataGrid();
                grid.HeaderStyle.Font.Bold = true;
                grid.Caption = htmlHeading;
                grid.CaptionAlign = TableCaptionAlign.Left;

                grid.DataSource = table;
                grid.DataBind();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

                //Added By Aman Mansoor on 06-12-2013 to Download Data excel file name with appropriate name  EAT-113
                //HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=MyFiles.xls");
                if (htmlHeading == "")
                {
                    htmlHeading = "MyFiles";
                }
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + htmlHeading.Replace("<h2>", "").Replace("</h2>","") + ".xls");
                //Added By Aman Mansoor on 06-12-2013 EAT-113

                HttpContext.Current.Response.Charset = "";


                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


                grid.RenderControl(htw);

                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();

            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.ImportExport.DataTableToExcel :-" + ex.Message);
            }

        }
    }
}
