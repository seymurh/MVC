using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace testApp.Models
{
    public class Employee
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}