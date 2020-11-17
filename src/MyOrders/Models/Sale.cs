using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyOrders.Models
{
    public sealed class Sale
    {
        public string Name { get; set; }
        [JsonProperty("category_id")]
        public int CategoryId { get; set; }
        [JsonProperty("policies")]
        public List<Policy> Policies { get; set; }
    }
}
