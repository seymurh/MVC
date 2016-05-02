using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace testApp.Models
{
    public class Log
    {
        public ObjectId Id { get; set; }

        public string LogMethod { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

    }
}