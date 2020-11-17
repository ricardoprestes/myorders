using MyOrders.Enums;
using MyOrders.Models;

namespace MyOrders.Helpers
{
    public sealed class GroupItem
    {
        public EGroupItemType Type { get; set; }
        public Sale Sale { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal Discount { get; set; }
    }
}
