using BlogEngine.PostManagement.Models.Posts.Configurations;
using BlogEngine.PostManagement.Models.Posts.Contracts;
using BlogEngine.PostManagement.Models.Posts.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.PostManagement.Models.Posts.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _posts;
      
        public PostRepository(IMongoDbConfiguration setting)
        {
           // var credentials = MongoCredential.CreateMongoCRCredential(databaseName: setting.DatabaseName, username: setting.User, password: setting.Pass);
           // var server = new MongoServerAddress(host: setting.ConnectionString, port: setting.Port);

           // var mongoClientSettings = new MongoClientSettings
           // {
           //     Credential = credentials,
           //     Server = server,
           //     ConnectionMode = ConnectionMode.Standalone,
           //     ServerSelectionTimeout = TimeSpan.FromSeconds(3)
           // };

           //var client = new MongoClient(mongoClientSettings);


            var client = new MongoClient(setting.ConnectionString);
            var database = client.GetDatabase(setting.DatabaseName);

            _posts = database.GetCollection<Post>(setting.PostCollectionName);
        }

        public Post Create(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }

        public Post Get(string id)
        {
          return  _posts.Find(c => c.Id == id).FirstOrDefault();
        }

        public List<Post> Get()
        {
            return _posts.Find(p => true).ToList();
        }

        public void Remove(Post post)
        {
            _posts.DeleteOne(c => c.Id == post.Id);
        }

        public void Remove(string id)
        {
            _posts.DeleteOne(p => p.Id == id);
        }

        public void Update(string id, Post post)
        {
            _posts.ReplaceOne(c => c.Id == id, post);
        }
    }
}
