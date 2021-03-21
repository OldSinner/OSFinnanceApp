using System;
using System.ComponentModel.DataAnnotations;

namespace FinnanceApp.Shared.Models
{
    public class Bills
    {
        public int id { get; set; }
        [Range(1, 5000, ErrorMessage = "Kwota musi być wyższa niż 1zł")]
        public double money { get; set; }
        public User Owner { get; set; }
        public int OwnerId { get; set; }
        public Shops Shop { get; set; }
        public int ShopId { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime BuyDate { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [StringLength(maximumLength: 200, ErrorMessage = "Komentarz za długi!")]
        public string comment { get; set; }
    }
}
