using System.Collections.Generic;

namespace Panda.Web.Models.Receipts
{
    public class ReceiptsAllViewModel
    {
        public ReceiptsAllViewModel()
        {
            this.Receipts = new List<ReceiptDetailsViewModel>();
        }
        public ICollection<ReceiptDetailsViewModel> Receipts { get; set; }
    }
}
