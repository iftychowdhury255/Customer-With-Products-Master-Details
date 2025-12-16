namespace CustomerCoreApi.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string PName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<ProductDetails>? ProductDetails { get; set; }
    }
}
