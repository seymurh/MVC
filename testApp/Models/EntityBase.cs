using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace testApp.Models
{
    public class EntityBase
    {
        public ObjectId Id { get; set;}
    }
}