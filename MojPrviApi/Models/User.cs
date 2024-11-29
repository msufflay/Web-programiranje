namespace MojPrviApi.Models
{
    public class User
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Surname {get; set;}

        public string getFullName(){
            return this.Name + this.Surname;
        }


    }
}