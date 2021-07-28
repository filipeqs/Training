using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTutorial.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Page { get; set; }
        [DisplayFormat( DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateAccess { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}