using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.ViewModels
{
    public class SaleDataViewModel
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public DateTime DateStamp { get; set; }
        public int Quantity { get; set; }
    }
}