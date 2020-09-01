using System;
using System.ComponentModel.DataAnnotations;
using SoapStoreComIT.Models;

namespace SoapStoreComIT.Views.ViewModels
{
    public class OrderPaymentDetails
    {
        public Order Order { get; set; }

        [Display(Name = "Cardholder Name")]
        public string CardholderName { get; set; }

        [CreditCard]
        [Display(Name = "Creditcard Number")]
        public string CreditcardNumber { get; set; }

        [Display(Name = "Expiration Date")]
        public string ExpirationDate { get; set; }

        [Display(Name = "CVV")]
        public string CVV { get; set; }

        public OrderPaymentDetails()
        {
        }
    }
}
