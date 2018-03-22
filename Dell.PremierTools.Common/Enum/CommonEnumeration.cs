namespace Dell.PremierTools.Common.Enum
{
    public class CommonEnumeration
    {
        public enum WEBAPIResponseMessage
        {
            Valid = 1,
            Invalid = 2,
            StoreIDEmpty = 3,
            StoreIDInvalidForOLROrNotExists = 4,
            AccessGroupNameEmpty = 5,
            AccessGroupInvalidSepecialCharacter = 6,
            AccessGroupDescriptionEmpty = 7,
            AccessGroupTypeEmpty = 8,
            CreatorUserNameEmpty = 9,
            AccessGroupAlreadyExists = 10,
            InvalidRequestObject = 11,
            InvalidGuid = 12,
            Created = 201,
            Updated = 200,
            Delete = 202,
            NotModified = 304,
            ExpectationFailed = 417,
            UserNameEmpty = 900,
            InvalidAttributeValue = 901,
            InvalidGroupId = 902,
			EmptyUsername = 102,
			EmptyOrderCodes = 103
		}
		public enum AccessGroupStatusCode
		{
			Success = 100,
			Failure = 101,
		}

		//public enum Regions
		//{
		//    US = 1,
		//    EMEA = 2,
		//    CA = 3,
		//    AP = 4,
		//    JP = 5,
		//    GEP = 6,
		//    GLOBAL = 7,
		//    LA = 8,
		//    CPF_EMEA = 9,
		//    CPF_APJ = 10
		//}
		public enum MagGroups
        {
            MainMenuTabsSelection = 1,
            MyPremier = 2,
            CatalogSelection = 3,
            StandardConfigurations = 4,
            CheckOutOptions = 5,
            ShippingOptions = 6,
            Reports = 7,
            PaymentOptions = 8,
            MarketingContentSelection = 9,
            CustomLinks = 10,
            ContentManagement = 11,
            DocumentSharing = 12,
            Support = 13,
            B2BLandingPageDefault = 14
        }
    }
}
