using System.Collections.Generic;
using System.Linq;

namespace MyOrders.Models
{
    public class Cart
    {
        public List<CartEntry> Entries { get; set; }

        public decimal Total
        {
            get
            {
                return Entries.Sum(e => e.Subtotal);
            }
        }

        public int Count
        {
            get
            {
                if (Entries != null)
                    return Entries.Sum(e => e.Amount);
                else
                    return 0;
            }
        }

        public Cart()
        {
            Entries = new List<CartEntry>();
        }
    }
}
