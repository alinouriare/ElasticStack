using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Output.WebUI.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public List<string> Keywords { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string Category { get; set; }
    }
}
