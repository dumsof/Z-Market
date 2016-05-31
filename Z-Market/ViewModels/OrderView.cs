using System.Collections.Generic;
using Z_Market.Models;

namespace Z_Market.ViewModels
{
    public class OrderView
    {
        public Customer Customer { get; set; }
        public ProductOrder ProductOrder { get; set; }
        public List<ProductOrder> Products { get; set; }
    }
}
