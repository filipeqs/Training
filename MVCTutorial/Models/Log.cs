using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTutorial.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Page { get; set; }
        public DateTime DateAccess { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}