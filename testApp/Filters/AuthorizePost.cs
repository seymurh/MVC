using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using testApp.Models;
using testApp.Repositories;
using MongoDB.Bson;

namespace testApp.Filters
{
    public enum Method { See,Update,Delete,SeeComment,UpdateComment,DeleteComment }
    
    public class AuthorizePost:AuthorizeAttribute
    {
        PostRepository postRepository=new PostRepository ();
        public Method Method { get; set; }
        public String IdKey { get; set; }
        public bool HasRoleOrUserSetted
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Roles) || !string.IsNullOrWhiteSpace(Users);
            }
        }
        public AuthorizePost(Method method, String idKey="id")
        {
            
            IdKey = idKey;
            Method = method;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
          

            if (Method == Method.Update)
            {
                return IsPostBelongsToUser(httpContext);
            }
            else if (Method == Method.Delete)
            {
                return IsPostBelongsToUser(httpContext) ||( HasRoleOrUserSetted && base.AuthorizeCore(httpContext));
            }
            else if (Method == Method.UpdateComment)
            {
                return IsCommentBelongsToUser(httpContext);
            }
            else if (Method == Method.DeleteComment)
            {
                return IsCommentBelongsToUser(httpContext) || (HasRoleOrUserSetted && base.AuthorizeCore(httpContext));
            }
            else
            {
                return false;
            }
          
        }
        public bool IsPostBelongsToUser(HttpContextBase httpContext)
        {
            User user = httpContext.CurrentUser();
            string postId = httpContext.Request[IdKey] ?? httpContext.Request.RequestContext.RouteData.Values[IdKey].ToString();
            if (user != null && !String.IsNullOrEmpty(postId))
            {
                return postRepository.IsPostBelongsToUser(new ObjectId(postId), user.Name);
            }
            return false;
        }
        public bool IsCommentBelongsToUser(HttpContextBase httpContext)
        {
            
            User user = httpContext.CurrentUser();

            string commentId = httpContext.Request[IdKey] ?? httpContext.Request.RequestContext.RouteData.Values[IdKey].ToString();
            if (user != null && !String.IsNullOrEmpty(commentId))
            {
                return postRepository.IsCommentBelongsToUser(new ObjectId(commentId), user.Name);
            }
            return false;
        }

        

    }
}