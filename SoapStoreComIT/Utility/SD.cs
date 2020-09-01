using System;
namespace SoapStoreComIT.Utility
{
    public static class SD
    {
        public const string DeafultItemImage = "soap.png";


        public const string ManagerUser = "Manager";
        public const string WarehouseUser = "Warehouse";
        public const string FrontDeskUser = "FrontDesk";
        public const string CustomerAndUser = "Customer";

        public const string ssShoppingCartCount = "ssCartCount";


        internal class UserAttribute : Attribute
        {
        }
    }
}
