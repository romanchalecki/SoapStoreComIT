using System.Collections.Generic;
using SoapStoreComIT.Models;

namespace SoapStoreComIT.Views.ViewModels
{
    public class StoreItemList
    {
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<SubCategory> SubCategoryList { get; set; }
        public StoreItem StoreItem { get; set; }
        public List<string> StoreItemLists { get; set; }

        public string StatusMessage { get; set; }


        public IEnumerable<StoreItem> StoreItems { get; set; }
    }
}