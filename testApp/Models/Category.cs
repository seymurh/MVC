﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace testApp.Models
{
    public class Category:EntityBase
    {
        public string Name { get; set;}
    }
}