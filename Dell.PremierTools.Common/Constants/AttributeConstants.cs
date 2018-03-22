using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dell.PremierTools.Common.Constants
{
    public class AttributeConstants
    {
        //Attributes (from PREMIERCE.attribute table) 
        //               attrib_name        attrib_id
        //               --------------		---------		
        public const int EQUOTES = 8;		//CheckoutOptions.aspx
        public const int REQUISITION_WORKFLOW = 281;		//CheckoutOptions.aspx
        public const int REQUISITION_AMOUNT_THRESHOLD = 282;		//CheckoutOptions.aspx
        public const int REQUISITION_QUANTITY_THRESHOLD = 283;		//CheckoutOptions.aspx
        public const int JCB = 286; // Japan payment type
        public const int BOLETO_BANCARIO = 289;  // LA premier payment type
        /// <summary>
        /// Direct Credit Consumer payment option (firstly enabled only for Brazil OLR direct stores).
        /// </summary>
        public const int DIRECT_CREDIT_CONSUMER = 901;
        /// <summary>
        /// Suplier Card Consumer payment option (firstly enabled only for Brazil OLR direct stores).
        /// </summary>
        public const int SUPPLIERCARD = 902;
        /// <summary>
        /// Suplier Card Consumer payment option (firstly enabled only for Brazil OLR direct stores).
        /// <para />
        /// Used for support the addtional options in Supplier Card payment (e.g. 30days, 45days, 60days).
        /// </summary>
        public const int SC_TERM = 903;
        public const int TAX_INCLUSIVE = 294; // For CN/APJ requirement
        public const int DISPLAY_CART_SUMMARY_CHECKOUT = 295;  // For Express Checkout 
        public const int SIGN_IN_WITHIN_SHIPPING = 296;        // For Express Checkout
        public const int STREET_ADDRESS_VERIFICATION = 297; // For AVS requirement
        public const int IS_PAGE_COMMERICAL_PARTNER = 196; // Is page Commercial Partner
        public const int IS_PAGE_MIGRATED_TO_STORE_SETTINGS = 199;//Is page migrated To Store Settings
        public const int ENABLE_DBM_FOR_PREMIER = 1005;  //EnableDBMforPremier 

        public const int SAVE_CARTS = 9;
        public const int ADDR_WIZ_B = 10;
        public const int CONT_INFO = 12;
        public const int DISP_SHIP_ADDR = 13;
        public const int DISP_BILL_ADDR = 14;
        public const int DISP_TAX_STAT = 15;		//CheckoutOptions.aspx
        public const int TAX_STAT_QUE = 291;		//CheckoutOptions.aspx
        public const int SUPPRESS_TAX_FLAG = 292;		//CheckoutOptions.aspx
        public const int ENABLE_SHIP_TAX = 223;		//CheckoutOptions.aspx
        public const int PURCHORD = 16;
        public const int FLOORPLAN = 17;
        public const int LINEOFCREDAPPL = 18;
        public const int LEASING = 19;
        public const int CREDCARDCALL = 20;
        public const int VISA = 21;
        public const int MASTERCARD = 22;
        public const int DISCOVER = 23;
        public const int AMEX = 24;
        public const int DINERSCLUB = 25;
        public const int B2B_PURCH = 26;
        public const int B2B_LEASE = 27;
        public const int NXT_DAY_DELIV = 28;
        //public const int 2ND_DAY_DELIV =			29;
        //public const int 3RD_DAY_DELIV =			30;
        public const int LOWEST_COST = 31;
        public const int NO_CHARGE = 32;
        public const int DESIG_CARRIER = 33;
        //Portal gaps - Adding new attribute which holds the Designated carrier fee value
        public const int DC_VALUE = 193;
        public const int STDCFGS = 34;
        public const int FULLCAT = 35;
        public const int SnP = 36;
        public const int US_OP_ORD = 37;
        public const int PURCH_HIST = 38;
        public const int CUST_RPT = 39;
        public const int PREM_INV = 40;
        public const int STAT_RPT = 42;
        public const int CUST_LINK = 43;
        public const int CUSTOM_LINKS_NAME = 148;
        public const int LEASE_PRICIING = 45;
        public const int SHR = 46;
        public const int SURVEY = 48;		//CheckoutOptions.aspx
        public const int TEXT_DOWNLOAD = 49;		//CheckoutOptions.aspx
        public const int MULTIPLE_CC = 50;
        public const int BILLTO_LOCK = 51;
        public const int SHIPTO_LOCK = 52;
        public const int BILLTO_EDITS = 53;
        public const int SHIPTO_EDITS = 54;
        public const int BILLTO_EDIT_EMAIL = 55;
        public const int SHIPTO_EDIT_EMAIL = 56;
        public const int INTNATL_CHECKOUT = 57;		//CheckoutOptions.aspx		
        public const int DC_HANDLING_FEE = 62;
        public const int SHOPPING_LIST = 64;
        public const int EQUOTE_BILL_TO = 65;		//CheckoutOptions.aspx
        public const int EQUOTE_SHIP_TO = 66;		//CheckoutOptions.aspx
        public const int EQUOTE_PAYMENT_INFO = 67;		//CheckoutOptions.aspx
        public const int EQUOTE_BILL_TO_REQ = 68;		//CheckoutOptions.aspx
        public const int EQUOTE_SHIP_TO_REQ = 69;		//CheckoutOptions.aspx
        public const int EQUOTE_PAYMENT_INFO_REQ = 70;		//CheckoutOptions.aspx
        public const int EQUOTE_TSE = 160;
        public const int NEGOTIATED_SHIPPING = 71;
        public const int ENABLE_LIST_ADMINISTRATOR = 257;
        public const int ENABLE_DOCUMENT_SHARING_REPORT = 265;
        public const int NONAUTH = 73;
        public const int LOGO = 74;
        public const int SEGMENT_WELCOME = 75;
        public const int PAGE_WELCOME = 76;
        public const int WELCOME_POPUP = 77;
        public const int EPP_URL = 78;
        public const int LEASE_TERM = 79;
        public const int LEASE_TYPE = 80;
        public const int VIEW_BASKET_DEFAULT = 81;
        public const int VIEW_CFG_DEFAULT = 82;
        public const int CC_DEFAULT = 83;
        public const int BILL_TO_EMAIL_ADDR = 84;
        public const int SHIP_TO_EMAIL_ADDR = 85;
        public const int CHECKOUT_COUNTRY_DEFAULT = 86;		//CheckoutOptions.aspx (text)
        public const int EQUOTE_EXPIRE_DAYS = 87;		//CheckoutOptions.aspx (int)
        public const int STORESURVEY_URL = 88;		//CheckoutOptions.aspx (text)
        public const int TAX_STATUS_DEFAULT = 89;		//CheckoutOptions.aspx (text)		
        public const int PAYMENT_DEFAULT = 91;
        public const int PO_TYPE = 92;
        public const int GLOBAL_ORDER_STATUS = 93;
        public const int GLOBAL_PRICING_CHARTS = 94;
        public const int DELL_FIN_SERVICES = 95;
        public const int ADDR_WIZ_S = 96;
        public const int ALL_PYMT_OPTIONS = 97;
        public const int ALL_SHIPPING_OPTIONS = 98;
        public const int ALL_RPT_OPTIONS = 99;
        public const int SEND_EQUOTE = 100;	//CheckoutOptions.aspx
        public const int PRIMARY_COUNTRY_CODE = 140;
        public const int PRIMARY_CURRENCY_CODE = 287;
        public const int CREDIT_CARD = 142;
        public const int DELL_PREFERRED_ACCOUNT = 157;
        public const int DELL_BUSINESS_CREDIT = 158;
        public const int ADDRESS_WIZ_M = 990;
        public const int ADDRESS_WIZ_B = 997;
        public const int ADDRESS_WIZ_S = 996;
        public const int IS_CHANNEL_ENABLE = 435;
        public const int ENDUSER_INFO_ENABLE = 436;
        public const int ENDUSER_REQUIRED = 437;
        // these shouldn't be used to determine which lease type is available
        // use attrib_id 80 instead
        public const int BIZSNAP_LEASE = 161;
        public const int DFS_CUSTOM_LEASE = 162;
        public const int DGLF_CUSTOM_LEASE = 163;
        public const int QUICKLEASE = 203;  //Paymentoptions.aspx
        public const int CUSTOM_LEASE_RENTAL = 288;

        //Added For TD#743: Add prepopulation function to designated carrier shipping option
        public const int DESIG_CARRIER_PREPOP = 164;
        public const int DESIG_CARRIER_EDITABLE = 166;
        // End of TD# 743

        //Added for TD#462
        public const int NON_CATALOG = 165; //Pagesetup.aspx
        public const int MYPREMIER = 222;

        //Shipping Options
        //-------------------------------------------------
        public const int SHIPPING_INSTRUCTIONS = 72;
        public const int DELIV_DEFAULT = 90;
        public const int SHIPPING_INSTR_REQUIRED = 111;
        public const int SHIPPING_INSTR_PREPOP = 112;
        public const int SHIPPING_HELP = 113;
        public const int SHIPPING_PREPOP_READONLY = 114;
        public const int Standard_Delivery = 210;
        //public const int Overnight					=   211;  //out of scope for global Premier first round
        //public const int Airfreight					=   212;  //out of scope for global Premier first round
        public const int Dell_Direct_Delivery = 905;
        public const int EXPEDITED_SHIPPING = 30005;
        //--------------------------------------------------
        #region New Attributes added for Bundle Price 194 Doc Upload 195
        public const int BUNDLE_PRICE = 194;
        public const int DOC_UPLOAD = 195;
        #endregion

        public const int FIXED_DELIVERY_DATE = 910;

        #region New Attribute added for Feature Product for Req 490403
        public const int FEATURE_PRODUCT = 440;
        public const int CONFIG_FEATURE_PRODUCT = 991;

        #endregion

        #region 3 new B2B settings
        public const int CONFIG_Display_Billing_Checkout = 1001;
        public const int CONFIG_Display_Payment_Checkout = 1002;
        public const int DISPLAY_MAILING_ADDRESS_IN_CHEKOUT = 1020;
        public const int DISPLAY_MAILING_ADDRESS_IN_EQUOTE = 1021;
        #endregion

        public const int MASTHEAD_AND_FOOTER_SHOW_ADDRESS_BOOK = 1006;
        public const int ENABLE_INTERNATIONAL_CHECKOUT_FOR_CONUM20 = 1007; // To Show CONUM20 
        public const int DISPLAY_BILLING_ADDRESS_IN_EQUOTE = 1008;
        public const int DISPLAY_SHIPPING_ADDRESS_IN_EQUOTE = 1009;
        public const int DISPLAY_PAYMENT_INFORMATION_IN_EQUOTE = 1010;
        public const int DISPLAY_CONTACT_INFORMATION_SECTION_IN_EQUOTE = 1011;
        public const int BILLING_ADDRESS_IN_EQUOTE_REQUIRED = 1012;
        public const int DISPLAY_BILLING_ADDRESS_IN_EQUOTE_WORKFLOW = 1013;
        public const int SHIPPING_ADDRESS_IN_EQUOTE_REQUIRED = 1014;
        public const int DISPLAY_SHIPPING_ADDRESS_IN_EQUOTE_WORKFLOW = 1015;
        public const int PAYMENT_INFO_IN_EQUOTE_REQUIRED = 1016;
        public const int DISPLAY_PAYMENT_INFO_IN_EQUOTE_WORKFLOW = 1017;
        public const int DISPLAY_CONTACT_INFO_IN_EQUOTE_WORKFLOW = 1018;
        public const int ENABLE_EQUOTE_EOL_VALIDATION = 1019; // To Show equote EOL validation
        public const int DISPLAY_SHIP_TO_ADDRESS_IN_CHECKOUT_WORKFLOW = 1000;
        public const int DISPLAY_MAIL_TO_ADDRESS_IN_CHECKOUT_WORKFLOW = 1004;
        public const int DISPLAY_MAILING_ADDRESS_IN_EQUOTE_WORKFLOW = 1022;

        public const int IS_FSP_ENABLE = 441; // PremierAttribute :  Final Solution Partner (isfspenabled)
        public const int END_USER_DETAILS_LOCKDOWN = 442;
        public const int IS_VAR_INFO_REQUIRED = 443;
        public const int IS_VAR_INFO_ENABLE = 444;
        public const int IS_END_USER_ADD_ENABLE = 445;

        //New Attribute for Korea Payment Option
        public const int DACOM = 433;

        //Premier Page Info Constants
        //----------------------------------------------
        public const int ORDER_BROKER_QUEUE = 155;
        //----------------------------------------------


        //Premier Page Setup Constants
        //----------------------------------------------
        public const int SEGMENT_MARKETING = 5;
        public const int SOLUTIONS = 7;
        public const int PREMIER_NEWS = 6;  //Premier News
        public const int EPP = 1;  //Employee Purchase
        public const int LEASE_PRICING = 45;
        public const int HELP_ME_CHOOSE = 44;
        public const int SIDE_BY_SIDE = 61; //product comparison
        public const int DOMS_IMPORT = 63;
        public const int ORDER_STATUS = 3;
        public const int FEEDBACK = 2;
        public const int TEMPLATES = 64;
        public const int SUPPORT_MENU = 4;
        public const int SERVICES_FRAGMENT = 220;
        public const int SLASH_FULL_CATALOG = 58;
        public const int SLASH_STANDARD_CONFIG = 59;
        public const int SLASH_SNP_FEATURED = 60;
        public const int SOLUTION_MAST_HEAD = 246;
        public const int MY_PRODUCT_AND_SERVICE_TAB = 264;
        public const int PURCHASE_HELP = 247;
        public const int SERVICES_MASTHEAD = 253;
        public const int CUSTOMER_MANAGE_LIST = 255;

        public const int STD_EXTENDED_DISPLAY = 141;
        public const int STD_CFG_IMAGES = 141;
        public const int CONTACTS_LANDING = 144;
        public const int CUSTOMLINK_LANDING = 145;
        public const int REPORTING_LANDING = 146;
        public const int MESSAGES_LANDING = 147;


        //Premier Page Advanced Setup Constants
        //---------------------------------------	
        public const int RESELLER = 290;
        public const int EXPORT_INTENT = 11;
        public const int IMAGEWATCH = 41;
        public const int LINK_NUM = 110;
        public const int CONTRACT_COCE = 149;
        public const int CUSTOMER_AGREEMENT_NUMBER = 904;

        public const int KEY_CODE = 150;
        public const int CDC_DISABLED = 151;
        public const int NON_STANDARD_TRANSACTION = 152;
        public const int ROUTE_TO_ORDER_BROKER = 153;
        public const int ORDER_BROKER_RULE = 154;
        public const int CM_OPTOUT = 156;
        public const int OB_BOOKING_CONUM = 159;
        public const int GLOBAL_PREMIER = 167; //Pagesetup.aspx
        public const int CONFIG_FINANCE_DISPLAY = 173; //Pagesetup.aspx
        public const int EQUOTE_UPSELL = 174; //Pagesetup.aspx
        public const int CART_IMAGES = 175; //Pagesetup.aspx
        public const int DELL_RECOMMENDED = 176; //Pagesetup.aspx
        public const int CONFIG_MODULE_IMAGES = 177; //Pagesetup.aspx
        public const int DISCOUNTS_COUPONS = 178; //Pagesetup.aspx
        public const int REVIEW_SUMMARY = 179; //Pagesetup.aspx
        public const int FULL_DELL_DEALS = 180; //Pagesetup.aspx
        public const int STD_DELL_DEALS = 181; //Pagesetup.aspx
        public const int SNP_DELL_DEALS = 182; //Pagesetup.aspx
        public const int CART_UPSELL = 183; //Pagesetup.aspx
        public const int CONFIG_VALIDATION = 184; //Pagesetup.aspx
        public const int CONFIG_MODULE_BANNERS = 185; //Pagesetup.aspx
        public const int ESTIMATED_SHIP_DATE = 186; //Pagesetup.aspx
        public const int DYNAMIC_PRICING = 187; //Pagesetup.aspx
        public const int EQUOTE_PROD_VALID = 188;
        public const int EQUOTE_REPRICING = 189;
        public const int WISH_LIST = 190;
        public const int TERMS_AND_SERVICES = 200; //Paymentoptions.aspx
        public const int DELL_ACCOUNT = 201; //Paymentoptions.aspx
        public const int QUICK_LOAN = 202;  //Paymentoptions.aspx
        public const int SINGLEPAGE_STDCONFIG = 204; //Pagesetup.aspx
        public const int CHEQUE = 205; //Paymentoptions.aspx
        public const int WIRETRANSFER = 206; //Paymentoptions.aspx
        public const int SWITCH = 207; //Paymentoptions.aspx
        public const int PROCUREMENT_CARD = 208; //Paymentoptions.aspx
        public const int THIRD_PARTY_LEASING = 209; //Paymentoptions.aspx
        public const int ECCARD = 213;
        public const int DANKORT = 214;
        public const int CARTASI = 215;
        public const int CARTABLEUE = 219;
        public const int BVR = 216;
        public const int MANDAT_ADMINISTRATIF = 217;
        public const int FINANCE_LEASE = 276;
        public const int DIRECT_DEPOSIT = 277;
        public const int ELEC_FUND_TRANSFER = 278;
        public const int TELE_TRANSFER = 279;
        public const int CHEQUE_BANK_DRAFT = 280;
        public const int Check_via_Courier = 225;
        public const int COUPONS = 226; //Pagesetup.aspx
        public const int RADIO_BTN_SELECTION = 256;
        public const int SEARCH_BOX = 248;


        public const int GLOBAL_PORTAL = 227;
        public const int VIEW_PREFERENCES = 228;
        public const int DISPLAY_UPLIFT_IN_CONFIG = 234; //pagesetup.aspx
        public const int DISPLAY_UPLIFT_IN_CART = 229; //pagesetup.aspx
        public const int GPLP_DEFAULT_VIEW = 235; //pagesetup.aspx

        public const int CPF_APJ = 236; //checkoutoptions.aspx
        public const int CPF_EMEA_USD = 237; //checkoutoptions.aspx
        public const int CPF_EMEA_Euro = 238; //checkoutoptions.aspx
        public const int DellWorldTrade = 239; //checkoutoptions.aspx
        public const int NonUS_Purchase = 240; //checkoutoptions.aspx
        public const int CPF_EMEA_LOCAL = 439; //checkoutoptions.aspx
        public const int CPF_Migrate_2 = 446; //checkoutoptions.aspx


        //For CR 63773
        public const int DISABLE_APJ_LOCAL = 133; //checkoutoptions.aspx
        public const int DISABLE_EMEA_LOCAL = 134; //checkoutoptions.aspx


        public const int GLOBAL_STANDARD_SHIPPING = 241;

        public const int MASSUPDATE = 230;
        public const int EnableEcoFeeExempt = 427;

        public const int MY_PREMIER = 254;
        public const int PREMIER_CONNECT = 260;
        public const int ANAV = 250; //pagesetup.aspx
        public const int CHANNEL = 262; //checkoutoptions.aspx
        public const int PDF_UPLOAD = 263; //checkoutoptions.aspx

        public const int E_WAY = 266; //pagesetup.aspx
        public const int E_WAY_Reports = 273; //pagesetup.aspx
        public const int E_WAY_Price_List = 274; //pagesetup.aspx

        public const int RFQ_EXPIRE = 267; //checkoutoptions.aspx

        //Prestige Reports
        public const int PR_PurchaseHist = 268;
        public const int PR_Delivery = 269;
        public const int PR_ProdQuality = 270;
        public const int PR_Warranty = 271;
        public const int PR_SLADelivery = 272;



        //Content Constants -- really should use the ContentComponents section for these, but this is here
        // until the code using it can be changed.
        //----------------------------------------------

        public const int DIV_SHIPPING_INSTR = 12000;	//Pre Populate text
        public const int DIV_SHIPPING_HELP = 12001;  //Help Text

        //----------------------------------------------

        public const int BUY_PHONE = 999;	// dummy attribute
        public const int AFFINITY_MAPPED = 299;

        //Global Premier
        //---------------


        public const int CONUM = 998; //dummy attribute

        public const int MyPremier_Content_view = 258;
        public const int MyPremier_Dashboard_view = 259;
        public const int DIRECT_ACCESS_LINK = 429;

        public const int ALLOW_USD = 284;
        public const int ALLOW_EURO = 192;

        //TAA Compliance 
        public const int TAA_Compliant = 136;
        public const int Contract_Requires_Splitting = 137;
        public const int Secondary_Contract_Code = 124;
        public const int Secondary_Contract_Desc = 261;

        //Address wizard 
        public const int Billing_Address_Book_Mode = 997;
        public const int Shipping_Address_Book_Mode = 996;

        //Shipping
        public const int SHIPPING_ESTIMATOR = 995;
        public const int SHIPPING_INSTRUCTION_MODE = 994;

        // Tax
        public const int TAX_EXEMPTION_DEFAULT_STATUS = 993;
        public const int ENABLE_NONPROCUREMENT = 898;
        public const int PORTAL_TYPE = 899;
        public const int IS_OLR_CREATED_PAGE = 897;

        public const int CMI_MIGRATED = 430;

        public const int Display_ATS_Inventory_Quantity = 950;
        public const int Display_ATS_Inventory_Message = 951;
        public const int AffinityAccountName = 32202;
        public const int AffinityAccountID = 32201;

        //Stop at quote
        public const int EnableStopAtQuote = 32204;

        public const int IsGoalContract = 915; //Goal Contract
    }
}
