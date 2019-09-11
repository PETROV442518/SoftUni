﻿using System.ComponentModel.DataAnnotations;

namespace Cinema.DataProcessor
{
    public class ImportMoviesDto
    {

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Duration { get; set; }
        [Required]
        [Range(1.00, 10.00)]
        
        public double Rating { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Director { get; set; }
    }
}