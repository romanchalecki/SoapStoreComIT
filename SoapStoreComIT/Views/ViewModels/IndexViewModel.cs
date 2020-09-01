 using System;
using System.Collections.Generic;
using SoapStoreComIT.Models;

namespace SoapStoreComIT.Views.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<StoreItem> StoreItem { get; set; }
        public IEnumerable<Category> Category { get; set; }

    }
}
