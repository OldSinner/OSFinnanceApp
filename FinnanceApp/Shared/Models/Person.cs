namespace FinnanceApp.Shared.Models
{
    public class Person
    {
        public int id { get; set; }
        public User Owner { get; set; }
        public string name { get; set; }
    }
}
