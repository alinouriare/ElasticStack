using BlogEngine.PostManagement.Models.Posts.Contracts;
using BlogEngine.PostManagement.Models.Posts.Entities;
using BlogEngine.Shared.Events.Posts;
using BlogEngine.Shared.Masstransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.PostManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IPostRepository _postRepository;

        public PostsController(IEventDispatcher eventDispatcher, IPostRepository postRepository)
        {
            _eventDispatcher = eventDispatcher;
            _postRepository = postRepository;
        }

        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            return _postRepository.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetPost")]
        public ActionResult<Post> Get(string id)
        {
            var book = _postRepository.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
        [HttpPost]
        public ActionResult<Post> Create(Post post)
        {
            _postRepository.Create(post);
            _eventDispatcher.Dispatch(new PostAdded
            {
                Author = post.Author,
                Body = post.Body,
                Category = post.Category,
                Id = post.Id,
                Keywords = post.Keywords,
                ShortDescription = post.ShortDescription,
                Title = post.Title
            });
            return CreatedAtRoute("GetPost", new { id = post.Id.ToString() }, post);
        
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Post post)
        {
            var curent = _postRepository.Get(id);

            if (curent == null)
            {
                return NotFound();
            }

            _postRepository.Update(id, post);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var curent = _postRepository.Get(id);

            if (curent == null)
            {
                return NotFound();
            }

            _postRepository.Remove(curent.Id);

            return NoContent();
        }
    }
}
