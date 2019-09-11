using System.ComponentModel.DataAnnotations;

namespace Cinema.DataProcessor
{
    public class ImportHallSeats
    {
        [Required]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public bool Is4Dx { get; set; }

        [Required]
        public bool Is3D { get; set; }


        [Range(1, int.MaxValue)]
        public int Seats { get; set; }
        
    }
}