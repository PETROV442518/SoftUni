using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.Web.Models.Packages
{
    public class AllPackagesByType
    {
        public AllPackagesByType()
        {
            this.Packages = new List<PackageByTypeViewModel>();
        }

        public List<PackageByTypeViewModel> Packages { get; set; }
    }
}
