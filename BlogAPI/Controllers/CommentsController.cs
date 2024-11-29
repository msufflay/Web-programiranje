using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DataBase dataBase;
        public CommentsController(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        [HttpGet(Name = "GetComment")]
        public ActionResult<List<Comment>> GetComment()
        {
            return Ok(dataBase.ListOfComments);
        }

        [HttpGet("{id}")]
        public ActionResult GetCommentById(string id)
        {
            var comment = dataBase.ListOfComments.FirstOrDefault(i => i.Id == id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        [HttpPost(Name = "CreateComment")]
        public ActionResult<Comment> CreateComment(Comment comment)
        {
            if(dataBase.ListOfPosts.Any(post=> post.Id == comment.PostId))
            {
                dataBase.ListOfComments.Add(comment);
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
            }
            return NotFound();
                    
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Comment comment)
        {
            var existingComment = dataBase.ListOfComments.FirstOrDefault(i => i.Id == id);

            if (existingComment == null)
            {
                return NotFound(new { message = $"Element s istim Id-jem {id} već postoji." });
            }

            existingComment.Content = comment.Content;
            existingComment.PostId = comment.PostId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var comment = dataBase.ListOfComments.FirstOrDefault(i => i.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            dataBase.ListOfComments.Remove(comment);

            return NoContent();
        }

    }

    
}
