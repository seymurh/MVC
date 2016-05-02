using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace testApp.Models
{
    [BsonIgnoreExtraElements]
    public class Comment:EntityBase
    {
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
        public List<Comment> Replies { get; set; }
        public DateTime CommentDate { get; set; }
    }
}