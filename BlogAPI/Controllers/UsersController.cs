using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataBase dataBase;
        public UsersController(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }
        [HttpGet(Name = "GetUser")]
        public ActionResult<List<User>> GetUser()
        {
            return Ok(dataBase.ListOfUsers);
        }

        [HttpGet("{id}")]
        public ActionResult GetUserById(string id)
        {
            var user = dataBase.ListOfUsers.FirstOrDefault(i => i.Id == id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost(Name = "CreateUser")]
        public ActionResult<User> CreateUser(UserCreateRequests usercreaterequests)
        {
            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = usercreaterequests.Name,
                Email = usercreaterequests.Email
            };

            dataBase.ListOfUsers.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UserUpdateRequests userupdaterequests)
        {
            var existingUser = dataBase.ListOfUsers.FirstOrDefault(i => i.Id == id);

            if (existingUser == null)
            {
                return NotFound(new { message = $"User s istim Id-jem {id} već postoji." });
            }

            existingUser.Name = userupdaterequests.Name;
            existingUser.Email = userupdaterequests.Email;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var post = dataBase.ListOfPosts.FirstOrDefault(i => i.UserId == id);
            if (post != null)
                return Conflict(new { message = "Ne mozete brisati usera, postoji post kojeg je kreirao taj user." });

            var user = dataBase.ListOfUsers.FirstOrDefault(i => i.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            dataBase.ListOfUsers.Remove(user);

            return NoContent();
        }
    }
}
