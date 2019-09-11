using Panda.Models;

namespace Panda.Services.Dtos
{
    public class PackageCreateDto
    {
        public string ShippingAddress { get;  set; }
        public string Description { get;  set; }
        public decimal Weight { get;  set; }
        public PandaUser Recipient { get;  set; }
    }
}
