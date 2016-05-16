using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using testApp.Models;
using testApp.Repositories;
using MongoDB.Bson;

using testApp.Models.ModelBinders;
using System.Web.Http.ModelBinding;
using testApp.Filters;



namespace testApp.Api
{
   //  [Route("api/post/{id}")]
   
    public class PostController : ApiController
    {
        protected override System.Web.Http.Results.ExceptionResult InternalServerError(Exception exception)
        {
            return base.InternalServerError(exception);
        }
       
        // GET api/<controller>
        PostRepository rep = new PostRepository();
         [Route("api/post/")]
        public IEnumerable<Post> Get()
        {
            var posts = rep.Find();
            return posts;
        }

        // GET api/<controller>/5
       // [ModelBinder(typeof(ObjectIdModelBinder))]
        [Route("api/post/{id}")]
        public Post Get([ModelBinder]ObjectId id)
        {
            return rep.FindById(id);
        }
        //Post insert etmək üçündür
        // POST api/<controller>
        public void Post([FromBody]Post value)
        {

        }
        //PUT update etmək üçündür
        // PUT api/<controller>/5
        public void Put([ModelBinder] ObjectId id, [FromBody]Post value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete([ModelBinder] ObjectId id)
        {
        }
    }
}