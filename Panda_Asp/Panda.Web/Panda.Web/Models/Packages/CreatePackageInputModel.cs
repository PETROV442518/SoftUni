using System.Collections.Generic;

namespace Panda.Web.Models.Packages
{
    public class CreatePackageInputModel
    {
        public CreatePackageInputModel()
        {
            this.Names = new List<string>();
        }
        public List<string> Names { get; set; }
    }
}
