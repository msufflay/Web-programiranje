namespace BlogAPI.Models
{
    public class DataBase
    {
        public List<User> ListOfUsers { get; set; } = new List<User>();  
        public List<Post> ListOfPosts { get; set; } = new List<Post>();
        public List<Comment> ListOfComments { get; set; } = new List<Comment>();

    }
}
