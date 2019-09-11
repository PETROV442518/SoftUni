using System.ComponentModel.DataAnnotations;

namespace SULS.Web.Models.Problem
{
    public class ProblemCreationBindingModel
    {
        [MaxLength(20)]
        [MinLength(5)]
        [Required]
        public string Name { get; set; }


        [Required]
        public int Points { get; set; }
    }
}
