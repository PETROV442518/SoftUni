using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services.Dtos
{
   public  class ProblemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int? Count { get; set; }
    }
}
