using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWebList.Models
{
    public class Response
    {
        public static object Cookies { get; internal set; }
        public string ErrorMessage { get; set; }
        public string ResponseMessage { get; set; }
    }
}