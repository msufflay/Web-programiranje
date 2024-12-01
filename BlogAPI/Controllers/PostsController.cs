using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DataBase dataBase;
        public PostsController(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        [HttpGet(Name = "GetPost")]
        public ActionResult<List<Post>> GetPost()
        {
            return Ok(dataBase.ListOfPosts);
        }

        [HttpGet("{id}")]
        public ActionResult GetPostById(string id)
        {
            var post = dataBase.ListOfPosts.FirstOrDefault(i => i.Id == id);
            if (post == null)
                return NotFound(new { message = $"Post s Id-jem {id} ne postoji." });

            return Ok(post);
        }

        [HttpPost(Name = "CreatePost")]
        public ActionResult<Post> CreatePost(PostCreateRequests postcreaterequests)
        {
            // mislim da ovdje nešto nije dobro :)
            if (dataBase.ListOfUsers.Any(user => user.Id == postcreaterequests.UserId))
            {
                Post post = new Post
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = postcreaterequests.Title,
                    UserId = postcreaterequests.UserId
                };

                dataBase.ListOfPosts.Add(post);
                return CreatedAtAction(nameof(GetPostById), new { id = post.Id },post);
            }
            return NotFound(new { message = $"Korisnik s Id-jem {postcreaterequests.UserId} ne postoji." });

        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, PostUpdateRequests postupdaterequests)
        {
            var existingPost = dataBase.ListOfPosts.FirstOrDefault(i => i.Id == id);

            if (existingPost == null)
            {
                return NotFound(new { message = $"Post s Id-jem {id} ne postoji." });
            }

            if (dataBase.ListOfUsers.Any(user => user.Id == postupdaterequests.UserId))
            {
                existingPost.Title = postupdaterequests.Title;
                existingPost.UserId = postupdaterequests.UserId;
            }
            else
                return NotFound(new { message = $"Korisnik s Id-jem {postupdaterequests.UserId} ne postoji." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var comment = dataBase.ListOfComments.FirstOrDefault(i => i.PostId == id);
            if (comment != null)
                return Conflict(new { message = "Ne mozete brisati objavu, postoje komentari na njoj." });

            var post = dataBase.ListOfPosts.FirstOrDefault(i => i.Id == id);

            if (post == null)
            {
                return NotFound(new { message = $"Post s Id-jem {id} ne postoji." });
            }

            dataBase.ListOfPosts.Remove(post);

            return NoContent();
        }
    }
}
