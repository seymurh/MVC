using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using testApp.Models;

namespace testApp.Repositories
{
    public class LogRepository
    {
        IMongoCollection<Log> logs;
        public LogRepository()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("test");
            logs = db.GetCollection<Log>("logs");
        }
        public List<Log> Get()
        {
            return logs.Find("{}").ToList();
        }
        public void AddLog(Log log)
        {
            logs.InsertOne(log);
        }
    }
}