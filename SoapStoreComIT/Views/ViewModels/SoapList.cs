using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoapStoreComIT.Models;

namespace SoapStoreComIT.Views.ViewModels
{
    public class SoapList
    {
        public IEnumerable<Soap> Soaps { get; set; }
    }
}
