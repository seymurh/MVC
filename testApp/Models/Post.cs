﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace testApp.Models
{
    public class Post : EntityBase
    {
        
        public string Header { get; set; }
        
        public string Content { get; set; }
       
        public User Author { get; set; }
        public List<Comment> Comments {get;set;}
        public string ImageUrl { get; set; }
        public DateTime PostDate { get; set; }
        public Category Category { get; set; }
    }
}