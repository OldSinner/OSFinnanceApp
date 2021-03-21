using System.ComponentModel.DataAnnotations;

namespace FinnanceApp.Shared.Models
{
    public class MontlyBills
    {
        public int id { get; set; }
        [Range(1, 31, ErrorMessage = "Dzień musi być pomiędzy 1 a 31")]
        public int dayOfMonth { get; set; } = 1;
        [Required(ErrorMessage = "Musisz nadać nazwę rachunkowi")]
        public string name { get; set; }
        [Range(0.01, 10000, ErrorMessage = "Rachunek musi mieć wartość")]
        public double value { get; set; }
        public string description { get; set; }
        public int shopid { get; set; }
        public Shops shop { get; set; }
        public int personid { get; set; }
        public Person person { get; set; }
        public User user { get; set; }
        public int categoryId { get; set; }
        public Category category { get; set; }
    }
}