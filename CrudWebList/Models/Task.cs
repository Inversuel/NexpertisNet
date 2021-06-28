using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CrudWebList.Models
{
    public class Task
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("User")]
        public string User { get; set; }
        [JsonProperty("AssignedUser")]
        public string AssignedUser { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Describe")]
        public string Describe { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JsonProperty("StartDate")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JsonProperty("EndDate")]
        public DateTime EndDate { get; set; }
        [JsonProperty("__v")]
        public int __v { get; set; }
    }
}