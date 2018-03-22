using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dell.PremierTools.Common.Shared
{
    public static class CommonShared
    {
        public static int CountryCodeReturn(string CountryCode)
        {
            int CountryReturnVal = 0;
            switch (CountryCode)
            {
                case "UK":
                case "AT":
                case "BE":
                case "CZ":
                case "DK":
                case "FI":
                case "FR":
                case "DE":
                case "GR":
                case "IE":
                case "IT":
                case "LU":
                case "NL":
                case "NO":
                case "PL":
                case "PT":
                case "SK":
                case "ES":
                case "SE":
                case "CH":
                case "ZA":
                case "RU":
                case "TR":
                case "AE":
                case "SA":
                case "UA":
                case "HU":
                case "IL":
                    CountryReturnVal = 1;
                    break;
                case "AU":
                case "CN":
                case "HK":
                case "IN":
                case "KR":
                case "MY":
                case "NZ":
                case "SG":
                case "TH":
                case "TW":
                case "SX":
                    CountryReturnVal = 2;
                    break;
                case "JP":
                    CountryReturnVal = 3;
                    break;
                case "BR":
                case "AR":
                case "CL":
                case "CO":
                case "VE":
                case "PE":
                case "MX":
                    CountryReturnVal = 4;
                    break;
            }

            return CountryReturnVal;
        }


        /// <summary>
        /// Check empty string and null
        /// </summary>
        /// <param name="inputString">String to check empty and null</param>
        /// <returns>true or false</returns>
        public static bool CheckEmptyStringAndNull(string inputString, int textLength)
        {
            if (string.IsNullOrEmpty(inputString) || inputString.Length > textLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check empty string and null
        /// </summary>
        /// <param name="inputString">String to check empty and null</param>
        /// <returns>true or false</returns>
        public static bool CheckEmptyStringAndNull(string inputString)
        {
            if (string.IsNullOrEmpty(inputString) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckAttributeValues(string attributeValue, Dictionary<string,int> dicAttributes)
        {

            if (dicAttributes.ContainsKey(attributeValue))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckCount(string accessGroupGuids)
        {
            List<string> accessGroupGuidList = accessGroupGuids.Split(',').ToList<string>();
            if (accessGroupGuidList.Count <= 0)
            {                

                return true;
            }
            else
            {
                foreach (string ag in accessGroupGuidList)
                {
                    if(string.IsNullOrEmpty(ag))
                    {
                        return true;
                    }

                }
                 return false;
            }
        }

        /// <summary>
        ///  Check special charters
        /// </summary>
        /// <param name="inputString">String to be validated</param>
        /// <returns>true or false</returns>
        public static bool IsSpecialCharacters(string inputString)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (regexItem.IsMatch(inputString))
            {
                return false;
            }

            return true;
        }

        public static bool IsInterger(string number)
        {
            int no = 0;
            if (int.TryParse(number, out no))
            {
                return true;
            }

            return false;
        }

        public static int GetInterger(string number)
        {
            int numberVal = 0;
            if (int.TryParse(number, out numberVal))
            {
                return numberVal;
            }

            return 0;
        }


        public static bool IsValidGUID(Guid guidInput)
        {
            if (guidInput != Guid.Empty)
            {
                Guid guidOutput = Guid.Empty;

                if (Guid.TryParse(guidInput.ToString(), out guidOutput))
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}

public class ESDateRequest
{
    public string AccessGroupID = string.Empty;
    public string SchemaName = string.Empty;
    public string ChildSchemaName = string.Empty;
    public string Value = string.Empty;
    public List<ESDateRequest> esDateRequestList = new List<ESDateRequest>();

    public ESDateRequest()
    {
    }

    public ESDateRequest(string guid)
    {
        ESDateRequest esDateRequest = new ESDateRequest();

        #region "Premier"
        //**Premier
        esDateRequest = new ESDateRequest
        {
            AccessGroupID = guid,
            SchemaName = "Premier",
            ChildSchemaName = "EnableStdConfigs",
            Value = "true"
        };

        esDateRequestList.Add(esDateRequest);

        esDateRequest = new ESDateRequest
        {
            AccessGroupID = guid,
            SchemaName = "Premier",
            ChildSchemaName = "EnableFullCatalog",
            Value = "true"
        };

        esDateRequestList.Add(esDateRequest);

        esDateRequest = new ESDateRequest
        {
            AccessGroupID = guid,
            SchemaName = "Premier",
            ChildSchemaName = "EnableSnP",
            Value = "true"
        };
        esDateRequestList.Add(esDateRequest);

        esDateRequest = new ESDateRequest
        {
            AccessGroupID = guid,
            SchemaName = "Premier",
            ChildSchemaName = "EnableStdConfigExtendedDisplay",
            Value = "true"
        };
        esDateRequestList.Add(esDateRequest);


        #endregion
    }
}
