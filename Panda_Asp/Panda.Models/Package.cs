using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Models
{
    public class Package
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public string ShippingAddress { get; set; }
        public PackageStatus Status { get; set; } = PackageStatus.Pending;
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string RecipientId { get; set; }
        public PandaUser Recipient { get; set; }
    }
}
