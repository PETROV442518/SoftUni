using System;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        
        public PurchaseType Type { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Z0-9]+-[A-Z0-9]+-[A-Z0-9]+$")]
        public string ProductKey { get; set; }
        
        public DateTime Date { get; set; }

        
        public int CardId { get; set; }
    
        public Card Card { get; set; }

        public int GameId { get; set; }

        
        public Game Game { get; set; }
    }
}