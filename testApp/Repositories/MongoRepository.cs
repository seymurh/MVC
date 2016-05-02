using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using testApp.Models;
using System.Threading.Tasks;

namespace testApp.Repositories
{
    public class MongoRepository<T> where T : EntityBase
    {
         //col = collection
         public  IMongoCollection<T> col;
         public  IMongoDatabase database;

         public string CollectionName
         {
             get
             {
                 return typeof(T).Name.ToLower();
             }
         }
         public  MongoRepository()
        {
            IMongoClient client = new MongoClient();
            database = client.GetDatabase("blog");
            col = database.GetCollection<T>(CollectionName);
        }
        public virtual List<T> Find()
        {
            return col.Find("{}").ToList();
        }
        public virtual async Task<List<T>> FindAsync()
        {
            var result=await col.FindAsync("{}");
            return await result.ToListAsync(); 
        }
        public virtual List<T> Find(BsonDocument doc)
        {
            return col.Find(doc).ToList();
        }
        public virtual async Task<List<T>> FindAsync(BsonDocument doc)
        {
            var result = await col.FindAsync(doc);
            return await result.ToListAsync();
        }
        public virtual T FindById(ObjectId id)
        {
            return col.Find(x => x.Id == id).ToList().FirstOrDefault();
        }
        public virtual async Task<T> FindByIdAsync(ObjectId id)
        {
            var result = await col.FindAsync(x => x.Id == id);
            List<T> tasks= await result.ToListAsync();
            if (tasks != null)
            {
                return tasks.FirstOrDefault();
            }
            return null;
        }
        public virtual void Add(T model)
        {
            col.InsertOne(model);
        }
        public virtual async void AddAsync(T model)
        {
          await  col.InsertOneAsync(model);
        }
        public virtual void Update(T model)
        {
            col.ReplaceOne(x => x.Id == model.Id, model);
        }
        public virtual async void UpdateAsync(T model)
        {
            await col.ReplaceOneAsync(x => x.Id == model.Id, model);
        }
        public virtual void Delete(ObjectId id)
        {
            col.DeleteOne(x => x.Id == id);
        }
        public virtual async void DeleteAsync(ObjectId id)
        {
            await col.DeleteOneAsync(x => x.Id == id);
        } 
    }
}