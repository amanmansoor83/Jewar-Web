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
using System.IO;

namespace Jewar.CodeLibrary
{
    public class FileManager
    {
        /// <summary>
        /// Read html,txt file & return string.
        /// How to Use:
        /// string FileText=OvrLod.FileManager.ReadFile("/TextFile/NewFile.txt");
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string ReadFile(string FileName)
        {
            string FileText = "";
            try
            {
                StreamReader sdr = new StreamReader(HttpContext.Current.Server.MapPath(FileName));
                string buffer = "";

                while ((buffer = sdr.ReadLine()) != null)
                {
                    FileText += buffer;
                }

                sdr.Close();
            }
            catch (Exception ee)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.FileManager.ReadFile :-" + ee.Message);
            }
            return FileText;
        }

        /// <summary>
        /// Write text in html & text files and return flag.
        /// How to Use:
        /// bool WritetoFile=OvrLod.FileManager.WriteFile("/TextFile/NewFile.txt","New Text");
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static bool WriteFile(string FileName, string strText)
        {
            bool WriteFile=false;
            try
            {
                TextWriter tx = new StreamWriter(HttpContext.Current.Server.MapPath(FileName));
                tx.WriteLine(strText);
                tx.Close();
                WriteFile = true;
            }
            catch (Exception ee)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.FileManager.WriteFile :-" + ee.Message);
            }
            return WriteFile;
        }
        
        /// <summary>
        /// Upload File & return filename
        /// How to Use:
        /// string UploadedFile=OvrLod.FileManager.UploadFile(HTMLFileUploader1,"/Uploads/");
        /// </summary>
        /// <param name="file"></param>
        /// <param name="FolderName"></param>
        /// <returns></returns>
        public static string UploadFile(HtmlInputFile file, string FolderName)
        {
            string output = " ", path = " ";
            try
            {
                path = HttpContext.Current.Server.MapPath("/" + FolderName + "/") ;
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                if (file.PostedFile.FileName.Length > 0)
                {
                    path = path + GetFileName(file);
                    file.PostedFile.SaveAs(path);
                    output = GetFileName(file);
                }
                else
                {
                    output = " ";
                }
            }
            catch (Exception ee)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.FileManager.UploadFile :-" + ee.Message);
            }
            return output;
        }

        /// <summary>
        /// Return FileName from the HTMLInputFile Control
        /// string FileName=OvrLod.FileManager.GetFileName(HTMLFileUploader1);
        /// </summary>
        /// <param name="uplTheFile"></param>
        /// <returns></returns>
        public static string GetFileName(HtmlInputFile uplTheFile)
        {
            string result = string.Empty;
            try
            {
                string strFileNameOnServer = uplTheFile.PostedFile.FileName;
                strFileNameOnServer = System.IO.Path.GetFileName(strFileNameOnServer);
                result = strFileNameOnServer;
            }
            catch (Exception ee)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.FileManager.GetFileName :-" + ee.Message);
            }
            return result;
            
        }
    }
}
