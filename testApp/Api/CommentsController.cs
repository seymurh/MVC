using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MongoDB.Bson;
using testApp.Filters;
using testApp.Models;
using testApp.Repositories;

namespace testApp.Api
{
    [CustomExceptionLogger]
    [Route("api/posts/{postId}/comments/")]
    public class CommentsController : ApiController
    {
        // GET api/<controller>

        PostRepository rep = new PostRepository();
         
        public IEnumerable<Comment> Get([ModelBinder]ObjectId postId)
        {
           // var id = new ObjectId(postId);
            return rep.FindById(postId).Comments;
        }

        // GET api/<controller>/5
        [Route("api/posts/{postId}/comments/{id}")]
        public Comment Get([ModelBinder]ObjectId postId, [ModelBinder]ObjectId id)
        {
            return rep.FindCommentById(id);
        }

        // POST api/<controller>
        public void Post([ModelBinder]ObjectId postId, [FromBody]Comment value)
        {
          
            rep.AddComment(postId, value);
        }
         [Route("api/posts/{postId}/comments/{id}")]
        // PUT api/<controller>/5
        public void Put([ModelBinder]ObjectId id, [FromBody]Comment value)
        {
            
            rep.UpdateComment(value);
        }
         [Route("api/posts/{postId}/comments/{id}")]
        // DELETE api/<controller>/5
        public void Delete([ModelBinder]ObjectId id)
        {

           rep.DeleteComment(id);
        }
    }
}