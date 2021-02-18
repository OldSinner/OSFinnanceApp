namespace FinnanceApp.Shared.Models
{
    public class MontlyBills
    {
        public int id {get;set;}
        public int dayOfMonth { get; set; }
        public string name { get; set; }
        public double value { get; set; }
        public string description { get; set; }
        public Shops shop { get; set; }
        public Person person { get; set; }
        public User user { get; set; }

        public Category category { get; set; }
    }
}