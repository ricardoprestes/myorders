using Newtonsoft.Json;

namespace MyOrders.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        [JsonProperty("category_id")]
        public int? CategoryId { get; set; }
    }
}
