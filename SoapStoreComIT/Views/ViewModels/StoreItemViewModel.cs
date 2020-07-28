using System;
using System.Collections.Generic;
using SoapStoreComIT.Models;

namespace SoapStoreComIT.Views.ViewModels
{
    public class StoreItemViewModel
    {
        public StoreItem StoreItem { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<SubCategory> SubCategory { get; set; }

        public IEnumerable<Category> CategoryList { get; set; }

        public IEnumerable<StoreItem> StoreItems { get; set; }

        public List<string> StoreItemList { get; set; }


    }
}
