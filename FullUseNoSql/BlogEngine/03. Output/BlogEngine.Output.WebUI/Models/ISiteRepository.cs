using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogEngine.Output.WebUI.Models
{
   public interface ISiteRepository
    {
        List<Post> GetAll();
        List<Post> Search(string q);
        Post Find(string id);
    }

    public class FakeSiteRepository : ISiteRepository
    {
        public Post Find(string id)
        {
            return

                new Post
                {
                    Title = "Titr",
                    ShortDescription = "Description",
                    Id = "123"
                };
        }

        public List<Post> GetAll()
        {
            return new List<Post>()
            {
                new Post
                {
                    Title = "Titr",
                    ShortDescription = "Description",
                    Id = "123"
                },
                    new Post
                {
                    Title = "Titr2",
                    ShortDescription = "Description2",
                    Id = "2"
                },
                new Post
                {
                    Title = "Titr3",
                    ShortDescription = "Description3",
                    Id = "3"
                }
            };
        }

        public List<Post> Search(string q)
        {
            return new List<Post>()
            {
                new Post
                {
                    Title = "Titr" + q,
                    ShortDescription = "Description",
                    Id = "1234"
                }
            };
        }
    }



    public class CachedRepository : ISiteRepository
    {
        private readonly FakeSiteRepository fakeSiteRepository;
        private readonly IDistributedCache distributedCache;

        public CachedRepository(FakeSiteRepository fakeSiteRepository, IDistributedCache distributedCache)
        {
            this.fakeSiteRepository = fakeSiteRepository;
            this.distributedCache = distributedCache;
        }
        public Post Find(string id)
        {
            var result = distributedCache.GetString(id);
            if (string.IsNullOrEmpty(result))
            {
                var fromDb = fakeSiteRepository.Find(id);
                string serialized = JsonSerializer.Serialize(fromDb);
                distributedCache.SetString(id, serialized);
                return fromDb;
            }
            var obj = JsonSerializer.Deserialize<Post>(result);
            return obj;
        }

        public List<Post> GetAll()
        {
            var result = distributedCache.GetString("ListOfAllItem");
            if (string.IsNullOrEmpty(result))
            {
                var fromDb = fakeSiteRepository.GetAll();
                string serialized = JsonSerializer.Serialize(fromDb);
                distributedCache.SetString("ListOfAllItem", serialized, new DistributedCacheEntryOptions
                {
                });
                return fromDb;
            }
            var obj = JsonSerializer.Deserialize<List<Post>>(result);
            return obj;
        }

        public List<Post> Search(string q)
        {
            throw new System.NotImplementedException();
        }
    }


}
