using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using testApp.Models;

namespace testApp.Repositories
{
    public class EmployeeRepository
    {
        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<Employee> col;

        public EmployeeRepository()
        {
            client = new MongoClient();
            db = client.GetDatabase("test");
            col = db.GetCollection<Employee>("employees");
        }
        public List<Employee> Find() 
        {
            return col.Find("{}").ToList();
        }
        public Employee FindById(ObjectId empId)
        {
           return col.Find(x => x.Id == empId).ToList().FirstOrDefault();
        }
        public void AddEmployee(Employee emp) 
        {
            col.InsertOne(emp);
        }
        public void UpdateEmployee(Employee emp)
        {
           col.ReplaceOne(x=>x.Id==emp.Id,emp);
        }
        public void DeleteEmployee(ObjectId empId)
        {
            col.DeleteOne(x => x.Id == empId);
        }
    }
}