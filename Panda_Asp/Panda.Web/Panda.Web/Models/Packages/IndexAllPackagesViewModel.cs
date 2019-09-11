using Panda.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.Web.Models.Packages
{
    public class IndexAllPackagesViewModel
    {

        public IndexAllPackagesViewModel()
        {
            this.Pending = new List<PackageIndexInfo>();
            this.Shipped = new List<PackageIndexInfo>();
            this.Delivered = new List<PackageIndexInfo>();
        }
        public ICollection<PackageIndexInfo> Pending { get; set; }
        public ICollection<PackageIndexInfo> Shipped { get; set; }
        public ICollection<PackageIndexInfo> Delivered { get; set; }
    }
}
