/**
* Copyright (c) 2013, 
 * 
* All rights reserved.
* @author Yasir Ahmed <yasir@Broadway.pk>
* @version 1.0.1
*/

using System;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using System.Security.Cryptography;
using System.Text;

namespace Jewar.CodeLibrary
{
    /// <summary>
    /// Validates a string using various functions
    /// </summary>
    public class ValidationManager
    {
        private static string word = "1$34)[+-@#";

        public static bool IsValidKeywordToSearch(String strToCheck)
        {
            bool result = false;
            try
            {
                if (strToCheck != null && strToCheck.Trim() == "")
                    result = false;

                Regex objAlphaPattern = new Regex("^[\\s\\w'-.@/]+$", RegexOptions.CultureInvariant);

                result = objAlphaPattern.Match(strToCheck.Trim()).Success;
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);                                
            }
            return result;
            
        }


        public static bool IsValidString(String strToCheck)
        {
            bool result = false;
            try
            {
                if (strToCheck != null && strToCheck.Trim() == "")
                    result = true;

                Regex objAlphaPattern = new Regex("^[\\s\\w'-.@/]+$", RegexOptions.CultureInvariant);

                result = objAlphaPattern.Match(strToCheck.Trim()).Success;
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);  
            }
            return result;
            
        }

        /// <summary>
        /// Function to test for Positive Integers with zero inclusive  
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsWholeNumber(string strNumber)
        {
            bool result = false;
            try
            {
                if (strNumber == "")
                    result = false;

                Regex objNotWholePattern = new Regex("[^0-9]");
                result = !objNotWholePattern.IsMatch(strNumber);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }
            return result;
        }

        /// <summary>
        ///Function to Check for AlphaNumeric. 
        /// </summary>
        /// <param name="strToCheck"> String to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsAlphaNumeric(string strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    return false;

                Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9 ]");

                valid = !objAlphaNumericPattern.IsMatch(strToCheck);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }
            
            return valid;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="strToCheck"> String to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidWebsiteUri(string strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                //Regex objAlphaNumericPattern=new Regex("[/][a-zA-Z_0-9 _-]+[^/]");
                Regex objTagRegex = new Regex("[a-zA-Z_0-9_/\\.-]", RegexOptions.ECMAScript);
                valid = objTagRegex.Match(strToCheck).Success;  
            }
            catch (Exception ex)
            {                 
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }            
            return valid;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="strToCheck"> String to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidWebsiteName(string strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objAlphaNumericPattern = new Regex("[a-zA-Z_0-9\\s]");

                valid = objAlphaNumericPattern.IsMatch(strToCheck);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);                                
            }
            
            return valid;
        }

        /// <summary>
        /// Function to check for valid color
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <returns></returns>
        public static bool isValidColor(string strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objColorPattern = new Regex("^[#][A-Za-z0-9]{6}$");

                valid = objColorPattern.IsMatch(strToCheck);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);                                               
            }            
            
            return valid;
        }


        /// <summary>
        /// Function to check for valid color
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <returns></returns>
        public static bool isValidUri(string strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objColorPattern = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\/\\\+&amp;%\$#_]*)?$");

                valid = objColorPattern.IsMatch(strToCheck);
                
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message); 
            }
            return valid;
        }


        /// <summary>
        ///Function to Check for valid system path
        /// </summary>
        /// <param name="strToCheck"> String to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidSystempath(string strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objAlphaNumericPattern = new Regex(@"^\\{2}[\w-]+\\(([\w-][\w-\s]*[\w-]+[$$]?$)|([\w-][$$]?$))");

                if (objAlphaNumericPattern.IsMatch(strToCheck))
                    valid = true;
                else
                    valid = false;

            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }           
            
            return valid;
        }

        /// <summary>
        ///Function to Check for valid alphanumeric input
        /// </summary>
        /// <param name="strToCheck"> String to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphaNumeric(string strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");

                valid = !objAlphaNumericPattern.IsMatch(strToCheck);

            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }           
            return valid;
        }

        /// <summary>
        ///Function to Check for valid alphanumeric input with space chars also
        /// </summary>
        /// <param name="strToCheck"> String to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphaNumericWithSpace(string strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9\\s]");

                valid = !objAlphaNumericPattern.IsMatch(strToCheck);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }           
            return valid;
        }
        /// <summary>
        ///Function to Check for valid alphanumeric input with space chars.
        ///Also includes a boolean to indicate whether Hyphen is included in the match
        /// </summary>
        /// <param name="strToCheck"> String to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphaNumericWithSpace(string strToCheck, bool IncludeHyphen)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9\\s\\-]");

                valid = !objAlphaNumericPattern.IsMatch(strToCheck);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }              
            return valid;
        }


        /// <summary>
        /// Function to Check for valid alphabet input with space chars also
        /// </summary>
        /// <param name="strToCheck"> String to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphabetWithSpace(string strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objAlphaNumericPattern = new Regex("[^a-zA-Z\\s]");

                valid = !objAlphaNumericPattern.IsMatch(strToCheck);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }            
            return valid;
        }


        /// <summary>
        ///  Function To test for Alphabets.
        /// </summary>
        /// <param name="strToCheck">Input string to check for validity</param>
        /// <returns>True if valid alphabetic string, False otherwise</returns>
        public static bool IsAlpha(String strToCheck)
        {
            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objAlphaPattern = new Regex("[^a-zA-Z]");

                valid = !objAlphaPattern.IsMatch(strToCheck);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }  
            
            return valid;
        }


        /// <summary>
        /// Function to test whether the string is valid number or not
        /// </summary>
        /// <param name="strNumber">Number to check for </param>
        /// <returns>True if valid number, False otherwise</returns>
        public static bool IsNumber(String strNumber)
        {
            if (strNumber != null && strNumber.Trim() == "")
                return true;
            try
            {
                Convert.ToDouble(strNumber);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInteger"></param>
        /// <returns></returns>
        public static bool IsInteger(string strInteger)
        {
            try
            {
                Convert.ToInt32(strInteger);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns></returns>
        public static bool IsDateTime(string strDateTime)
        {
            try
            {
                Convert.ToDateTime(strDateTime);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool IsValidDate(string year, string month, string day)
        {
            try
            {
                System.DateTime dt = new System.DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
                dt.ToUniversalTime();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }


        public static string RemoveAllHTML(string strHTML)
        {
            string strValue = string.Empty;
            try
            {
                Regex objPattern = new Regex("<[^>]*>", RegexOptions.IgnoreCase);
                strValue = objPattern.Replace(strHTML, "");
            }
            catch (System.Exception)
            {
            }
            return strValue;

        }


        public static bool isValidPhone(string strToCheck)
        {

            bool valid = false;
            try
            {
                if (strToCheck == "")
                    valid = false;

                Regex objAlphaPattern = new Regex("[^einostxEINOSTX0-9 -()]");

                valid = !objAlphaPattern.IsMatch(strToCheck);
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return valid;
        }

        public static bool IsNotNull(string Str)
        {
            return Str == String.Empty;
        }

        public static bool IsValidHTMLInjection(string strBuff)
        {
            return (!Regex.IsMatch(HttpUtility.HtmlDecode(strBuff), "<(.|\n)+?>"));
        }

        /// <summary>
        /// Checks whether a valid Email address was input
        /// </summary>
        /// <param name="inputEmail">Email address to validate</param>
        /// <returns>True if valid, False otherwise</returns>
        public static bool isEmail(string inputEmail)
        {
            if (inputEmail != null && inputEmail != "")
            {
                string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                Regex re = new Regex(strRegex);

                if (re.IsMatch(inputEmail))
                {
                    return true;
                }
            }

            return false;
        }

        //Validation with setting Label Class
        public int ValidateText(string strValue, System.Web.UI.HtmlControls.HtmlGenericControl lblctrl, bool AllowNull)
        {
            int ReturnNum = 0;
            try
            {
                if (!AllowNull)
                {
                    if (!IsNotNull(strValue))
                    {
                        lblctrl.Attributes.Add("class", "Error");
                        ReturnNum = 1;
                    }
                    else
                    {
                        lblctrl.Attributes.Add("class", "");
                        ReturnNum = 0;
                    }
                }
                else if (!IsValidHTMLInjection(strValue) || !IsAlpha(strValue))
                {
                    lblctrl.Attributes.Add("class", "Error");
                    ReturnNum = 1;
                }
                else
                {
                    lblctrl.Attributes.Add("class", "");
                    ReturnNum = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.ValidationManager.ValidateText :-" + ex.Message);
            }
            
            return ReturnNum;
        }

        public int ValidateString(string strValue, System.Web.UI.HtmlControls.HtmlGenericControl lblctrl, bool AllowNull)
        {
            int ReturnNum = 0;
            try
            {
                if (!AllowNull)
                {
                    if (!IsNotNull(strValue))
                    {
                        lblctrl.Attributes.Add("class", "Error");
                        ReturnNum = 1;
                    }
                }
                else
                {
                    lblctrl.Attributes.Add("class", "");
                    ReturnNum = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.ValidationManager.ValidateString :-" + ex.Message);
            }
            
            return ReturnNum;
        }

        public int ValidateNumber(string strValue, System.Web.UI.HtmlControls.HtmlGenericControl lblctrl, bool AllowNull)
        {
            int ReturnNum = 0;
            try
            {
                if (!AllowNull)
                {
                    if (!IsNotNull(strValue))
                    {
                        lblctrl.Attributes.Add("class", "Error");
                        ReturnNum = 1;
                    }
                }
                else if (!IsValidHTMLInjection(strValue) || !IsNumber(strValue))
                {
                    lblctrl.Attributes.Add("class", "Error");
                    ReturnNum = 1;
                }
                else
                {
                    lblctrl.Attributes.Add("class", "");
                    ReturnNum = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.ValidationManager.ValidateNumber :-" + ex.Message);
            }
            
            return ReturnNum;
        }

        public int ValidateEmail(string strValue, System.Web.UI.HtmlControls.HtmlGenericControl lblctrl, bool AllowNull)
        {
            int ReturnNum = 0;
            try
            {
                if (!AllowNull)
                {
                    if (!IsNotNull(strValue) || !isEmail(strValue))
                    {
                        lblctrl.Attributes.Add("class", "Error");
                        ReturnNum = 1;
                    }
                    else
                    {
                        lblctrl.Attributes.Add("class", "");
                        ReturnNum = 0;
                    }
                }
                else if (!IsValidHTMLInjection(strValue) || !isEmail(strValue))
                {
                    lblctrl.Attributes.Add("class", "Error");
                    ReturnNum = 1;
                }
                else
                {
                    lblctrl.Attributes.Add("class", "");
                    ReturnNum = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.ValidationManager.ValidateEmail :-" + ex.Message);
            }
            
            return ReturnNum;
        }

        public static string validateMobileNumber(string contactNumber)
        {

            contactNumber = contactNumber.Trim();
            contactNumber = contactNumber.Replace(" ", "");
            contactNumber = contactNumber.Replace("-", "");
            contactNumber = contactNumber.Replace("#", "");

            StringBuilder strBuilder = new StringBuilder(contactNumber);

            strBuilder.Replace("009203", "03", 0, 6);// EAT-823
            strBuilder.Replace("923", "03", 0, 3);
            strBuilder.Replace("9203", "03", 0, 4);

            return strBuilder.ToString();
        }
    }
}
