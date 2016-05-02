using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using testApp.Models;

namespace testApp.Repositories
{
   
    public class UserRepository:MongoRepository<User>
    {
        
       
        public void AddUser (User user)
        {
            col.InsertOne(user);
        }
        public bool ValidateUser(User user)
        {
            var users= col.Find(u=>u.Name==user.Name && u.Password==user.Password).ToList();
            return users!=null ?users.FirstOrDefault()!=null:false;
        }
        public List<User> GetUsers()
        {
            return col.Find("{}").ToList();
        }
        public User FindUser(string name)
        {
            var users=col.Find(u => u.Name == name).ToList();
            return users!=null? users.FirstOrDefault():null;
        }
        public User FindById(ObjectId id)
        { 
            var users=col.Find(u => u.Id == id).ToList();
            return users != null ? users.FirstOrDefault() : null;
        }
        public void Update(User user)
        {
             col.FindOneAndReplace(x => x.Id == user.Id, user);
        }
        public void Delete(ObjectId id)
        {
            col.DeleteOne(u => u.Id == id);
        }
    }
}