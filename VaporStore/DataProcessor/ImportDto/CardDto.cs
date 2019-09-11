using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor
{
    public class CardDto
    {
        [Required]
        [RegularExpression(@"^[0-9]{4}\s+[0-9]{4}\s+[0-9]{4}\s+[0-9]+$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{3}$")]
        public string CVC { get; set; }

       
        public string  Type { get; set; }
        
    }
}