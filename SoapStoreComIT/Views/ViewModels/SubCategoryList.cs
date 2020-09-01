using System.Collections.Generic;
using SoapStoreComIT.Models;

namespace SoapStoreComIT.Views.ViewModels
{
    public class SubCategoryList
    {
        public IEnumerable<Category> CategoryList { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<string> SubCategoryLists { get; set; }

        public string StatusMessage { get; set; }


        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
