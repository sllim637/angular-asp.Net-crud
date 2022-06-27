using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Card
    {
        [Key]
        public Guid id { get; set; }
        public  string carHolderName { get; set; }
        public string cardNumber { get; set; }
        public int expiryDate   { get; set; }
        public int expiryYear { get; set; }
        public int CVC { get; set; }

    }
}
