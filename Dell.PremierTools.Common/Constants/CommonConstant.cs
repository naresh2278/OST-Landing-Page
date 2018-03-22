using System.Collections.Generic;

namespace Dell.PremierTools.Common.Constants
{
    public class CommonConstant
    {
        #region Alerts and Updates

        public const string StoreIDEmpty = "StoreID is empty or Max length (9) exceeded";
        public const string StoreIDInvalidForOLROrNotExists = "StoreID is invalid for OLR or does not exists";
        public const string AccessGroupNameEmpty = "Access group name is empty or Max length (30) exceeded";
        public const string AccessGroupNameInvalidSepecialCharacter = "Special characters are not allowed in Access group name";
        public const string AccessGroupDescriptionEmpty = "Access group description is empty or Max length (250) exceeded";
        public const string AccessGroupTypeEmpty = "Please provide valid access group type (1-19)";
        public const string CreatorUserNameEmpty = "Creator name is empty or Max length (90) exceeded";
        public const string AccessGroupAlreadyExists = "An Access group already exists with this name";
        public const string Created = "New Access group name is created successfull";
        public const string CreateFailed = "New Access group name creation is failed";
        public const string InvalidRequestObject = "Request object is invalid. Please provide valid request";
        public const string InvalidGuidMsg = "Please provide a valid Guid";
        public const string ExpectationFailed = "An exception has occurred";
        public const string UserNameEmpty = "UserName is empty";
        public const string InvalidAttributeGroupId = "AttributeGroupId should be greater 0";
        public const string InvalidAttributeValue = "Attribute value is invalid. It should either be 0\\1\\Required\\Optional\\Off\\Locked\\Readonly\\Editable";
        public const string UpdatedSuccessfully = "Settings updated successfully";
        public const string UpdatedFailed = "Settings update failed";
		public const string StoreInvalidOrNoConfigs = "Store is invalid or has no corresponding configs";
		public const string NoConfigsForGUID = "No configs for given GUID";
		public const string Success = "Query Successful";
		public const string Updated = "Access group updated successfully";
		public const string EmptyOrderCodes = "Please enter valid ordercode details";

		public const int StoreIDLength = 9;
        public const int AccessGroupNameLength = 30;
        public const int AccessGroupDescriptionLength = 250;
        public const int CreatorUserNameLength = 90;

        #endregion

        #region Connection string

        public const string ConnPCE = "conn_ICU_Staging";
        public const string ConnCHAM = "conn_CHAMELEON";
        public const string ConnMyDellDB = "conn_MyDell";
        public const string sConfigConnString = "conn_Configserver";
        public const string connMetadataContext = "MetadataContext";
		public const string ConnConfigserveremea = "conn_Configserveremea";
		public const string ConnConfigserverjp = "conn_Configserverjp";
		public const string ConnPCE_Staging = "conn_PCE_Staging";



		#endregion

		#region "ActionNames"
		public const string ACCESSGROUP = "OstServices/AccessGroup";
        public const string GETACCESSGROUPSETTINGS = "OstServices/Stores/{storeId}/AccessGroups/{guids}/Settings";
        public const string UPDATEACCESSGROUPSETTINGS = "OstServices/Store/AccessGroup/Settings/UpdateSettings";
		public const string GETACCESSGROUPSTDCONFIGS = "OstServices/Store/{storeId}/AccessGroups/{guid}/StdConfigs";
		public const string POSTACCESSGROUPSTDCONFIGS = "OstServices/Store/AccessGroup/StdConfigs/UpdateStdConfigs";


		#endregion
		#region Settings

		public const string PremierSchema = "Premier";
        public const string ChannelMethod = "Commercial";
        public const string CustType = "Premier";
        public const string PaymentSchema = "Payment";
        public const string SchemaTypeName = "EnterpriseSettings";
        public const string Administrator = "Administrator";
        public const string GeneralAccess = "General Access";
        public const string Management = "Management";
        public const string Standard = "Standard";
        public const string Public = "Public";
        public const string EMEA = "EMEA";
        public const string MastheadAndFooterSchema = "MastheadAndFooter";

        public const string MastheadandFooter = "MastheadandFooter";
        public const string GcmCartService = "GcmCartService";
        public const string Browse = "Browse";
        public const string AccountManagement = "AccountManagement";


        public IDictionary<int, string> PremierESTemplateIdMapping = new Dictionary<int, string>()
                                                                {

                                                                   {1, "General"},
                                                                   {2, "B2B"}  ,
                                                                   {3, "Other"} ,
                                                                   {4, "SiteAdmin"} ,
                                                                   {5, "UnAuthenticated"} ,
                                                                   {6, "General"},
                                                                   {7, "SiteAdmin"},
                                                                   {8, "B2B"},
                                                                   {9, "GlobalPortalGeneral"},
                                                                   {10, "GlobalPortalSiteAdmin"},
                                                                   {11, "General"},
                                                                   {12, "SiteAdmin"},
                                                                   {13, "B2B"},
                                                                   {14, "General"},
                                                                   {15, "SiteAdmin"},
                                                                   {16, "B2B"},
                                                                   {17, "General"},
                                                                   {18, "SiteAdmin"},
                                                                   {19, "B2B"}
                                                                };

        #endregion
        public const string ATTRIBUTEIDTOSECTIONID = "ReplaceAGAttribute/AttributeToSectionId";
        public const string ATTRIBUTETOBEREPLACED = "ReplaceAGAttribute/AttributeToBeReplaced";
        public static int[] FilterMagAttributes = new[]
                                                   {
                                                        AttributeConstants.ANAV,
                                                        264, //My Product and Service Masthead Tab 
                                                        AttributeConstants.COUPONS,
                                                        AttributeConstants.CUSTOMER_MANAGE_LIST,
                                                        AttributeConstants.ENABLE_LIST_ADMINISTRATOR,
                                                        AttributeConstants.INTNATL_CHECKOUT,
                                                        AttributeConstants.EPP ,
                                                        251, //birdseed
                                                        252 ,//footer
                                                        
                                                        117  ,// Custom (Generic) Excel Report
                                                        135 ,//Global Purchase History Reports
                                                        //Removing B2b Payment options so that it wil be displayed in MAG 
                                                        AttributeConstants.B2B_PURCH,
                                                        AttributeConstants.B2B_LEASE, 
                                                        //AttributeConstants.DELL_PREFERRED_ACCOUNT,
                                                        //AttributeConstants.DELL_BUSINESS_CREDIT,

                                                     //Commented below Payment options for defect 480045 fix
                                                        //AttributeConstants.DINERSCLUB,
                                                        //AttributeConstants.CHEQUE, 
                                                        //AttributeConstants.WIRETRANSFER , 
                                                        //AttributeConstants.PROCUREMENT_CARD,
                                                        AttributeConstants.BILLTO_EDITS,
                                                        AttributeConstants.SHIPTO_EDITS,
                                                        AttributeConstants.ADDR_WIZ_B,
                                                        AttributeConstants.ADDR_WIZ_S,
                                                        AttributeConstants.ADDRESS_WIZ_M,
                                                        AttributeConstants.ADDRESS_WIZ_B,
                                                        AttributeConstants.ADDRESS_WIZ_S,
                                                        AttributeConstants.BILLTO_LOCK,
                                                        AttributeConstants.SHIPTO_LOCK,
                                                        //Added below 3 new B2B settings to filter at Page and SiteAdmin though it will be available at AG level for B2B.
                                                        AttributeConstants.CONFIG_Display_Billing_Checkout,
                                                        AttributeConstants.CONFIG_Display_Payment_Checkout,
                                                        AttributeConstants.MASTHEAD_AND_FOOTER_SHOW_ADDRESS_BOOK,

                                                        AttributeConstants.DISPLAY_BILLING_ADDRESS_IN_EQUOTE,
                                                        AttributeConstants.BILLING_ADDRESS_IN_EQUOTE_REQUIRED,
                                                        AttributeConstants.DISPLAY_BILLING_ADDRESS_IN_EQUOTE_WORKFLOW,
                                                        AttributeConstants.DISPLAY_SHIPPING_ADDRESS_IN_EQUOTE,
                                                        AttributeConstants.DISPLAY_SHIPPING_ADDRESS_IN_EQUOTE_WORKFLOW,
                                                        AttributeConstants.SHIPPING_ADDRESS_IN_EQUOTE_REQUIRED,
                                                        AttributeConstants.DISPLAY_PAYMENT_INFORMATION_IN_EQUOTE,
                                                        AttributeConstants.DISPLAY_PAYMENT_INFO_IN_EQUOTE_WORKFLOW,
                                                        AttributeConstants.PAYMENT_INFO_IN_EQUOTE_REQUIRED,
                                                        AttributeConstants.DISPLAY_CONTACT_INFORMATION_SECTION_IN_EQUOTE,
                                                        AttributeConstants.DISPLAY_CONTACT_INFO_IN_EQUOTE_WORKFLOW,
                                                        AttributeConstants.DISPLAY_SHIP_TO_ADDRESS_IN_CHECKOUT_WORKFLOW,
                                                        AttributeConstants.DISPLAY_MAILING_ADDRESS_IN_EQUOTE_WORKFLOW,
                                                        AttributeConstants.DISPLAY_MAIL_TO_ADDRESS_IN_CHECKOUT_WORKFLOW,
                                                        AttributeConstants.SHIPPING_INSTRUCTION_MODE
                                                     };
    }
}
