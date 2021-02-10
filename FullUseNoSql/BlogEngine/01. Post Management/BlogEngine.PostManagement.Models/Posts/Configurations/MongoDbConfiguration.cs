using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.PostManagement.Models.Posts.Configurations
{
    public class MongoDbConfiguration : IMongoDbConfiguration
    {
   

        public string PostCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    
    }
}
