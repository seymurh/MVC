using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using testApp.Models;


namespace testApp.Repositories
{
    public class PostRepository : MongoRepository<Post>
    {
        /// <summary>
        ///  id-lə posta comment yazmaq üçnü metod
        ///  comment.Post propertisi null olsa belə işləyəcək
        /// </summary>
        /// <param name="postId">Comment yazmaq istədiyiniz postun id-si</param>
        /// <param name="comment">id-sin göndərdiyiniz posta yazılacaq comment</param>
        public void AddComment(ObjectId postId, Comment comment)
        {
            Post post = base.FindById(postId);
            if (post != null)
            {
                if (post.Comments == null)
                {
                    post.Comments = new List<Comment>();
                }
                post.Comments.Add(comment);
                base.Update(post);
            }
        }
      
        public void DeleteComment(ObjectId commentId)
        {
            var filter = string.Format("{{'Comments._id':ObjectId('{0}') }}", commentId);
            col.UpdateMany(filter, Builders<Post>.Update.PullFilter("Comments", Builders<Comment>.Filter.Eq(c => c.Id, commentId)));
        }
       
        public void UpdateComment(Comment comment)
        {
            //col.Aggregate().Lookup("comments", "", "", "");
            var filter = string.Format("{{'Comments._id':ObjectId('{0}') }}", comment.Id);
            
            var update = Builders<Post>.Update.Set("Comments.$.Content", comment.Content).Set("Comments.$.Author", comment.Author).Set("Comments.$.Replies", comment.Replies);
            var result = col.UpdateOne(filter, update);
            
        }
        public bool IsPostBelongsToUser(ObjectId postId,string username) 
        {
            try
            {
                var filter = string.Format("{{_id:ObjectId('{1}'), 'Author.Name':'{0}' }}", username, postId);
                var posts = col.Find(filter).ToList();
                return posts != null && posts.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsCommentBelongsToUser(ObjectId commentId, string username)
        {
            try
            {
                var filter = string.Format("{{'Comments._id':ObjectId('{1}'), 'Comments.Author.Name':'{0}' }}", username, commentId);
                var posts = col.Find(filter).ToList();
                return posts != null && posts.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Comment FindCommentById(ObjectId commentId)
        {
            var filter = string.Format("{{'Comments._id':ObjectId('{0}') }}", commentId);
            List<Post> posts = col.Find(filter).ToList();
            if (posts != null)
            {
                List<Comment> comments = posts.FirstOrDefault().Comments.Where(c => c.Id == commentId).ToList();
                return comments.FirstOrDefault();
            }
            return null;
        }



    }
}