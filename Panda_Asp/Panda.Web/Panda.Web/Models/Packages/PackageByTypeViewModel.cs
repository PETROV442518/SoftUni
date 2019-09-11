using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.Web.Models.Packages
{
    public class PackageByTypeViewModel
    {
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public string ShippingAddress { get; set; }
        public string Recipient { get; set; }

        public string Id { get; set; }
    }
}
