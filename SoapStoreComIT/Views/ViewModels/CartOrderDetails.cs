using System;
using System.Collections.Generic;
using SoapStoreComIT.Models;

namespace SoapStoreComIT.Views.ViewModels
{
    public class CartOrderDetails
    {
        public List<ShoppingCart> listCart { get; set; }

        public Order Order { get; set; }



        public CartOrderDetails()
        {
        }
    }
}
