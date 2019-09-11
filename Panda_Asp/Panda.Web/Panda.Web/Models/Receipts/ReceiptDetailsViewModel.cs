using System;

namespace Panda.Web.Models.Receipts
{
    public class ReceiptDetailsViewModel
    {
        public string Id { get; set; }
        public DateTime IssuedOn { get; set; }
        public string ShippingAddress { get; set; }
        public decimal PackageWeight { get; set; }
        public string Description { get; set; }
        public string RecipientName { get; set; }
        public decimal Fee { get; set; }
    }
}
