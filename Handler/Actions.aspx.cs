using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Globalization;
using Jewar.CodeLibrary;
using System.Text;
using Amazon.ElasticBeanstalk;
using System.IO;
using secureacceptance;

namespace Jewar.Handler
{
    public partial class Actions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check for tranfer of key, log entry.
        }
  
        [WebMethod]
        public static string Login(string Email, string Password)
        {
            string message = "";
            if (!checkForSQLInjection(Email) && !checkForSQLInjection(Password))
            {
                //check existing email
                DataTable dtSubscribe = DBHandler.GetData("SELECT * FROM users WHERE email = '" + Email.Replace("'", "''") + "' and password = '" + Cryptography.EncryptMessage(Password.Replace("'", "''")) + "'");
                if (dtSubscribe.Rows.Count > 0)
                {
                    HttpContext.Current.Session["UserID"] = dtSubscribe.Rows[0]["ID"].ToString();
                    HttpContext.Current.Session["Type"] = dtSubscribe.Rows[0]["Type"].ToString();

                    if (dtSubscribe.Rows[0]["Type"].ToString() == "Agent")
                    {
                        HttpContext.Current.Session["AgentID"] = "0";
                        dtSubscribe = DBHandler.GetData("SELECT * FROM agent WHERE userid  = '" + dtSubscribe.Rows[0]["ID"].ToString() + "'");
                        if (dtSubscribe.Rows.Count > 0)
                        {
                            HttpContext.Current.Session["AgentID"] = dtSubscribe.Rows[0]["ID"].ToString();
                            HttpContext.Current.Session["AgentImage"] = dtSubscribe.Rows[0]["Image"].ToString();
                            HttpContext.Current.Session["AgentName"] = dtSubscribe.Rows[0]["FirstName"].ToString() + " " + dtSubscribe.Rows[0]["LastName"].ToString();

                            message = "{ \"success\": true, \"message\" : \"Login successfull.\" }";
                        }

                    }
                    else
                    {
                        message = "{ \"success\": false, \"message\" : \"Invalid username or password.\" }";
                    }

                }
                else
                {
                    message = "{ \"success\": false, \"message\" : \"Invalid username or password.\" }";
                }
            }
            return message;
        }


        [WebMethod] 
        public static string Test()
        {
            string s1 = "abc";

            try
            {
                HttpRequest request = HttpContext.Current.Request;

                if (request.Files.Count > 0)
                {

                    HttpPostedFile uploadedFile = request.Files[0];

                    if (uploadedFile != null && uploadedFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(uploadedFile.FileName);
                        string filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + fileName);
                        uploadedFile.SaveAs(filePath);

                        return "File uploaded successfully";
                    }


                    //string s1 = "abc";


                    //context.Response.ContentType = "text/plain";
                    //try
                    //{
                    //    string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                    //    string[] files;
                    //    int numFiles;
                    //    files = System.IO.Directory.GetFiles(dirFullPath);
                    //    numFiles = files.Length;
                    //    numFiles = numFiles + 1;
                    //    string str_image = "";

                    //    foreach (string s in context.Request.Files)
                    //    {
                    //        HttpPostedFile file = context.Request.Files[s];
                    //        string fileName = file.FileName;
                    //        string fileExtension = file.ContentType;

                    //        if (!string.IsNullOrEmpty(fileName))
                    //        {
                    //            fileExtension = Path.GetExtension(fileName);
                    //            str_image = "MyPHOTO_" + numFiles.ToString() + fileExtension;
                    //            string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;
                    //            file.SaveAs(pathToSave_100);
                    //        }
                    //    }
                    //    //  database record update logic here  ()

                    //    context.Response.Write(str_image);
                }
            }
            catch (Exception ac)
            {

            }

            return s1;
        }


        [WebMethod]
        public static string AddProperty(string Title, string Description, string Category, string Listed, string Status, string Price, string YearlyTaxRate, string AfterPriceLabel, string VideoFrom, string EmbedVideoid, string VirtualTour, string Address, string State, string City, string Neighborhood, string Zip, string Country, string Latitude, string Longitude, string Size, string LotSize, string Rooms, string Bedrooms, string Bathrooms, string CustomID, string Garages, string GarageSize, string YearBuilt, string AvailableFrom, string Basement, string ExtraDetails, string Roofing, string ExteriorMaterial, string Structure, string Floors, string AgentNotes, string EnergyClass, string EnergyIndex, string Attic, string BasketballCourt, string AirConditioning, string Lawn, string SwimmingPool, string Barbeque, string Microwave, string TVCable, string Dryer, string OutdoorShower, string Washer, string Gym, string OceanView, string PrivateSpace, string LakeView, string WineCellar, string FrontYard, string Refrigerator, string WiFi, string Laundry, string Sauna, string ImageLinks)
        {
            string message = "";
            //if (!checkForSQLInjection(Email) && !checkForSQLInjection(Password))
            //{
            //check existing email

            string AgentID = HttpContext.Current.Session["AgentID"].ToString();



            string InsertSQL = string.Format(@"insert  into `properties`(`AgentID`,`Title`,`Description`,`Category`,`Listed`,`Status`,`Price`,`YearlyTaxRate`,`AfterPriceLabel`,`VideoFrom`,`EmbedVideoid`,`VirtualTour`,
`Address`,`State`,`City`,`Neighborhood`,`Zip`,`Country`,`Latitude`,`Longitude`,`Size`,`LotSize`,`Rooms`,`Bedrooms`,`Bathrooms`,`CustomID`,`Garages`,`GarageSize`,`YearBuilt`,`AvailableFrom`,`Basement`,`ExtraDetails`,`Roofing`,
`ExteriorMaterial`,`Structure`,`Floors`,`AgentNotes`,`EnergyClass`,`EnergyIndex`,`Attic`,`BasketballCourt`,`AirConditioning`,`Lawn`,`SwimmingPool`,`Barbeque`,`Microwave`,`TVCable`,`Dryer`,`OutdoorShower`,`Washer`,`Gym`,
`OceanView`,`PrivateSpace`,`LakeView`,`WineCellar`,`FrontYard`,`Refrigerator`,`WiFi`,`Laundry`,`Sauna`) 
values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}',
'{33}','{34}','{35}','{36}','{37}','{38}','{39}',{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54},{55},{56},{57},{58},{59})",
AgentID, Title, Description, Category, Listed, Status, Price, YearlyTaxRate, AfterPriceLabel, VideoFrom, EmbedVideoid, VirtualTour,
Address, State, City, Neighborhood, Zip, Country, Latitude, Longitude, Size, LotSize, Rooms, Bedrooms, Bathrooms, CustomID, Garages, GarageSize, YearBuilt, AvailableFrom, Basement, ExtraDetails, Roofing,
ExteriorMaterial, Structure, Floors, AgentNotes, EnergyClass, EnergyIndex, Attic, BasketballCourt, AirConditioning, Lawn, SwimmingPool, Barbeque, Microwave, TVCable, Dryer, OutdoorShower, Washer, Gym,
OceanView, PrivateSpace, LakeView, WineCellar, FrontYard, Refrigerator, WiFi, Laundry, Sauna);
            int Insert = DBHandler.InsertDataWithoutLogin(InsertSQL); //    if (dtSubscribe.Rows.Count > 0)
            if (Insert > 0)
            {
                ImageLinks = ImageLinks.TrimEnd(',');
                string[] ImageLinkSingle = ImageLinks.Split(',');

                if (ImageLinkSingle.Length > 0)
                {
                    for (int a = 0; a < ImageLinkSingle.Length; a++)
                    {
                        int Insert1 = DBHandler.InsertDataWithoutLogin(string.Format("insert into propertyimages (ImageURL, PropertyID)values('{0}','{1}')", Insert, ImageLinkSingle[a])); //    i
                    }
                }


                message = "{ \"success\": true, \"message\" : \"Property added successfull.\" }";
            }
            else
            {
                message = "{ \"success\": false, \"message\" : \"Cannot add property at the moment.\" }";
            }
            return message;
        }

        [WebMethod]
        public static string UpdateProfile(string Username, string Email, string FirstName, string LastName, string Position, string Language, string CompanyName, string TaxNumber, string Address, string Aboutme)
        {
            string message = "";
            //if (!checkForSQLInjection(Email) && !checkForSQLInjection(Password))
            //{
            //check existing email
            int Insert =0;
            string AgentID = HttpContext.Current.Session["AgentID"].ToString();
            string InsertSQL = "";
           DataTable dtAgentProfile = DBHandler.GetData(string.Format("select * from agent where id = '{0}'", AgentID));

            if (dtAgentProfile.Rows.Count == 0)
            {
                InsertSQL = string.Format(@"insert  into `profile`(Username, Email, FirstName, LastName, Position, Language, CompanyName, TaxNumber, Address, Aboutme, Created) 
values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
    Username, Email, FirstName, LastName, Position, Language, CompanyName, TaxNumber, Address, Aboutme, DateTime.Now.ToString("yyy-MM-dd H:mm:ss"));

                Insert = DBHandler.InsertDataWithoutLogin(InsertSQL);

                HttpContext.Current.Session["AgentID"] = Insert;
            }
            else
            {
                InsertSQL = string.Format(@"update  `profile` set Username = '{0}', Email = '{1}', FirstName = '{2}', LastName = '{3}', Position = '{4}', Language = '{5}', CompanyName = '{6}', TaxNumber = '{7}', 
Address = '{8}', Aboutme = '{9}' , Modified = '{10}' where id = '{11}'",
  Username, Email, FirstName, LastName, Position, Language, CompanyName, TaxNumber, Address, Aboutme, DateTime.Now.ToString("yyy-MM-dd H:mm:ss"), AgentID);
                Insert = DBHandler.InsertDataWithoutLogin(InsertSQL);
            }
             
            if (Insert > 0)
            {
                message = "{ \"success\": true, \"message\" : \"Profile updated successfull.\" }";
            }
            else
            {
                message = "{ \"success\": false, \"message\" : \"Cannot update profile at the moment.\" }";
            }
            return message;
        }
        
        [WebMethod]
        public static string UpdateSocial(string FacebookUrl, string PinterestUrl, string InstagramUrl, string TwitterUrl, string LinkedinUrl, string WebsiteUrl)
        {
            string message = "";
            //if (!checkForSQLInjection(Email) && !checkForSQLInjection(Password))
            //{
            //check existing email
            int Insert = 0; //
            string AgentID = HttpContext.Current.Session["AgentID"].ToString();
            string InsertSQL = "";
            DataTable dtAgentProfile = DBHandler.GetData(string.Format("select * from agent where id = '{0}'", AgentID));

            if (dtAgentProfile.Rows.Count == 0)
            {
                InsertSQL = string.Format(@"insert  into `profile`( FacebookUrl,  PinterestUrl,  InstagramUrl,  TwitterUrl,  LinkedinUrl,  WebsiteUrl, Created) 
values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
     FacebookUrl, PinterestUrl, InstagramUrl, TwitterUrl, LinkedinUrl, WebsiteUrl, DateTime.Now.ToString("yyy-MM-dd H:mm:ss"));

                Insert = DBHandler.InsertDataWithoutLogin(InsertSQL);
                HttpContext.Current.Session["AgentID"] = Insert;
            }
            else
            {
                InsertSQL = string.Format(@"update  `profile` set FacebookUrl = '{0}', PinterestUrl = '{1}', InstagramUrl = '{2}', TwitterUrl = '{3}', LinkedinUrl = '{4}', WebsiteUrl = '{5}' , Modified = '{6}' where id = '{7}'",
  FacebookUrl, PinterestUrl, InstagramUrl, TwitterUrl, LinkedinUrl, WebsiteUrl,  DateTime.Now.ToString("yyy-MM-dd H:mm:ss"), AgentID);

                Insert = DBHandler.InsertDataWithoutLogin(InsertSQL);
            }
             
            if (Insert > 0)
            {
                message = "{ \"success\": true, \"message\" : \"Profile updated successfull.\" }";
            }
            else
            {
                message = "{ \"success\": false, \"message\" : \"Cannot update profile at the moment.\" }";
            }
            return message;
        }

        [WebMethod]
        public static string ChangePassword(string OldPassword, string NewPassword, string ConfirmNewPassword)
        {
            string message = "";
            //if (!checkForSQLInjection(Email) && !checkForSQLInjection(Password))
            //{
            //check existing email
            int Insert = 0; //
            string AgentUserID = HttpContext.Current.Session["UserID"].ToString();
            string InsertSQL = "";

            if (Convert.ToInt32(AgentUserID) > 0)
            {
                if (NewPassword == OldPassword)
                {
                    InsertSQL = string.Format(@"update  `users` set password = '{0}'  where password = '{1}' and  id = '{2}'", Cryptography.EncryptMessage(NewPassword) , Cryptography.EncryptMessage(OldPassword), AgentUserID);

                    Insert = DBHandler.InsertDataWithoutLogin(InsertSQL);

                }
            }

            if (Insert > 0)
            {
                message = "{ \"success\": true, \"message\" : \"Profile updated successfull.\" }";
            }
            else
            {
                message = "{ \"success\": false, \"message\" : \"Cannot update profile at the moment.\" }";
            }
            return message;
        }

        [WebMethod]
        public static string SearchHome(string SearchType, string SearchText, string Price1, string Price2, string PropertyType, string PropertyID, string Location, string MinSqFeet, string MaxSqFeet,  string Bedrooms, string Bathrooms, string SearchAttic, string SearchAirConditioning, string SearchLawn,   string SearchTVCable, string SearchDryer, string SearchOutdoorShower, string SearchWasher, string SearchLakeview,string SearchWinecellar, string SearchFrontyard, string SearchRefrigerator)        
        {            
            string message = "";
            //if (!checkForSQLInjection(Email) && !checkForSQLInjection(Password))
            //{
            //check existing email
            int Insert = 0;

            string QueryString = string.Format(@"Price1={0}&Price2={1}&PropertyType={2}&PropertyID={3}&Location={4}&MinSqFeet={5}&MaxSqFeet={6}&Bedrooms={7}&Bathrooms={8}&SearchAttic={9}&SearchAirConditioning={10}
                &SearchLawn={11}&SearchTVCable={12}&SearchDryer={13}&SearchOutdoorShower={14}&SearchWasher={15}&SearchLakeview={16}&SearchWinecellar={17}&SearchFrontyard={18}&SearchRefrigerator={19}&SearchType={20}&SearchText={21}",
                Price1, Price2, PropertyType, PropertyID, Location, MinSqFeet, MaxSqFeet, Bedrooms, Bathrooms, SearchAttic, SearchAirConditioning, SearchLawn, SearchTVCable, SearchDryer, SearchOutdoorShower,
             SearchWasher, SearchLakeview, SearchWinecellar, SearchFrontyard, SearchRefrigerator, SearchType, SearchText);

            QueryString = Cryptography.EncryptMessage(QueryString);


            if (Insert > 0)
            {
                message = "{ \"success\": true, \"message\" : \"" + QueryString +  "\"  }";
            }
            else
            {
                message = "{ \"success\": false, \"message\" : \"Cannot update profile at the moment.\" }";
            }
            return message;
        }



        public static Boolean checkForSQLInjection(string userInput)
        {

            bool isSQLInjection = false;

            string[] sqlCheckList = { "--","'",

                                       ";--",

                                       ";",

                                       "/*",

                                       "*/",

                                        "@@",


                                        "char",

                                       "nchar",

                                       "varchar",

                                       "nvarchar",

                                       "alter",

                                       "begin",

                                       "cast",

                                       "create",

                                       "cursor",

                                       "declare",

                                       "delete",

                                       "drop",

                                       "end",

                                       "exec",

                                       "execute",

                                       "fetch",

                                            "insert",

                                          "kill",

                                             "select",

                                           "sys",

                                            "sysobjects",

                                            "syscolumns",

                                           "table",

                                           "update"

                                       };

            string CheckString = userInput.Replace("'", "''");

            for (int i = 0; i <= sqlCheckList.Length - 1; i++)
            {

                if ((CheckString.IndexOf(sqlCheckList[i],

    StringComparison.OrdinalIgnoreCase) >= 0))

                { isSQLInjection = true; }
            }

            return isSQLInjection;
        }

    }

    public class SearchItem
    {

        public string Name
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string logo
        {
            get;
            set;
        }

        public SearchItem(string name, string address, string logo)
        {
            this.Name = name;
            this.Address = address;
            this.logo = logo;
        }
    }

    public class MyDataTables
    {
        public DataTable DTmyOrder;
        public DataTable DTmyorderdetailOptions;
    }
}