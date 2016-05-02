using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using testApp;
using testApp.Models;
using testApp.Repositories;
using MongoDB.Bson;

namespace testApp.Tests.Repository
{
      [TestClass]
   public  class PostRepositoryTest
    {
          PostRepository rep;
          Post post;
          public PostRepositoryTest()
          {
              rep = new PostRepository();
             
          }

          [TestInitialize]
          public void Start()
          {
              User comment1User = new User { Name = "com1", Id = ObjectId.GenerateNewId() };
              User comment2User = new User { Name = "com2", Id = ObjectId.GenerateNewId() };
              post = new Post();
              post.Id = ObjectId.GenerateNewId();
              post.Author = new User { Name = "user", Id = ObjectId.GenerateNewId() };
              post.Content = "post content";
              post.Header = "post header";
              post.Comments = new List<Comment>();
              post.Comments.Add(new Comment { Id = ObjectId.GenerateNewId(), Author = comment1User, Content = "comment1" });
              post.Comments.Add(new Comment { Id = ObjectId.GenerateNewId(), Author = comment2User, Content = "comment2" });
             
          }
          [TestCleanup]
          public void Clean()
          {
              rep.Delete(post.Id);
          }
          [Priority(3)]
          [TestMethod]
          public void AddPost()
          {
             rep.Add(post);
             List< Post> foundPosts = rep.Find();
             Assert.IsNotNull(foundPosts);
             Assert.AreEqual(foundPosts.First().Content, post.Content);
          }
           [Priority(2)]
            [TestMethod]
          public void UpdatePost()
          {
              rep.Add(post);
              Post newPost = rep.FindById(post.Id);
              User comment3User = new User { Name = "com2", Id = ObjectId.GenerateNewId() };
              newPost.Content = "post content updated";
              var newComment=new Comment { Id = ObjectId.GenerateNewId(), Author = comment3User, Content = "comment3" };
              newPost.Comments.Add(newComment);
              rep.Update(newPost);
              Post updatedPost = rep.FindById(post.Id);
              Assert.AreEqual(updatedPost.Content, newPost.Content);
              Assert.IsTrue(updatedPost.Comments.Where(c=>c.Id==newComment.Id ).FirstOrDefault().Content=="comment3");
          }
           [Priority(2)]
            [TestMethod]
          public void AddComment()
          {
              rep.Add(post);
                User author=new User {Id=ObjectId.GenerateNewId(),Name="comment4user" };
                Comment newComment=new Comment{Id=ObjectId.GenerateNewId(), Author=author,Content="comment4" };
                rep.AddComment(post.Id,newComment);
                Post updatedPost = rep.FindById(post.Id);
                Assert.IsTrue(updatedPost.Comments.Where(c => c.Id == newComment.Id).FirstOrDefault().Content == "comment4");
          }
           [Priority(2)]
            [TestMethod]
          public void DeleteComment()
          {
              rep.Add(post);
              User author = new User { Id = ObjectId.GenerateNewId(), Name = "commentdeleteuser" };
              Comment newComment = new Comment { Id = ObjectId.GenerateNewId(), Author = author, Content = "delteComment" };
              rep.AddComment(post.Id, newComment);
               Post updatedPost = rep.FindById(post.Id);
               rep.DeleteComment(newComment.Id);
               Post postCommentDeleted = rep.FindById(post.Id);
               var com = postCommentDeleted.Comments.Where(c => c.Id.ToString() == newComment.Id.ToString()).Select(p=>p).ToList();
               Assert.AreEqual(com.Count(),0);
          }
            [TestMethod]
          public void UpdateComment()
          {
              rep.Add(post);
              Comment newComment = new Comment { Id = ObjectId.GenerateNewId(),  Content = "before update" };
              rep.AddComment(post.Id, newComment);
              newComment.Content="after update";
              rep.UpdateComment(newComment);
              Assert.AreEqual(rep.FindById(post.Id).Comments.Where(c=>c.Id==newComment.Id).ToList().Last().Content, newComment.Content);
          }
    }
}
