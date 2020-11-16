using System.Collections.Generic;

namespace MyOrders.Models
{
    public sealed class Sale
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public List<Policy> Policies { get; set; }
    }
}
