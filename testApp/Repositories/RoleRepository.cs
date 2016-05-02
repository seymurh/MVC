using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using testApp.Models;

namespace testApp.Repositories
{
    public class RoleRepository:MongoRepository<Role>
    {
        IMongoCollection<Role> roles;
        IMongoCollection<User> users;
        public RoleRepository() 
        {
            roles = database.GetCollection<Role>("roles");
            users = database.GetCollection<User>("users");
        }
        public List<Role> GetRoles()
        {
           return roles.Find("{}").ToList() ;
        }
        public Role FindRoleById(ObjectId id)
        {
            return roles.Find(x => x.Id == id).ToList().FirstOrDefault();
        }
        public void AddRole(Role role)
        {
            roles.InsertOne(role);
        }
        public override void Update(Role role)
        {
            roles.ReplaceOne(r => r.Id == role.Id, role);
            string filter = string.Format("{{'Roles._id':ObjectId('{0}') }}", role.Id);
            users.UpdateMany(filter, Builders<User>.Update.Set("Roles.$.Name", role.Name));
        }
        public void DeleteRole(ObjectId id)
        {
            roles.DeleteOne(r => r.Id == id);
        }
    }
}