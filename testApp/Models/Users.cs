using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace testApp.Models
{

     [BsonIgnoreExtraElements]
    public class User:EntityBase
    {
       
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string ProfileImageUrl { get; set; }
        public string Password { get; set; }

        public List<Role> Roles { get; set; }
    }
}