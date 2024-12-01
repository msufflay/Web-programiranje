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
                return NotFound(new { message = $"Komentar s Id-jem {id} ne postoji." });

            return Ok(comment);
        }

        [HttpPost(Name = "CreateComment")]
        public ActionResult<Comment> CreateComment(CommentCreateRequests commentcreaterequests)
        {
            if(dataBase.ListOfPosts.Any(post=> post.Id == commentcreaterequests.PostId))
            {
                Comment comment = new Comment
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = commentcreaterequests.Content,
                    PostId = commentcreaterequests.PostId
                };
                dataBase.ListOfComments.Add(comment);
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
            }
            return NotFound();
                    
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, CommentUpdateRequests commentupdaterequests)
        {
            var existingComment = dataBase.ListOfComments.FirstOrDefault(i => i.Id == id);

            if (existingComment == null)
            {
                return NotFound(new { message = $"Komentar s Id-jem {id} ne postoji." });
            }

            if (dataBase.ListOfPosts.Any(post => post.Id == commentupdaterequests.PostId))
            {
                existingComment.Content = commentupdaterequests.Content;
                existingComment.PostId = commentupdaterequests.PostId;
            }
            else
                return NotFound(new { message = $"Post s Id-jem {commentupdaterequests.PostId} ne postoji." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var comment = dataBase.ListOfComments.FirstOrDefault(i => i.Id == id);
            // ako komentar ne postoji
            if (comment == null)
            {
                return NotFound(new { message = $"Komentar s Id-jem {id} ne postoji." });
            }

            dataBase.ListOfComments.Remove(comment);

            return NoContent();
        }

    }

    
}
