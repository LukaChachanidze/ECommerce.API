namespace Task_ECommerce.Services.Cart.DTO
{
    /// <summary>
    /// Data transfer object for cart
    /// </summary>
    public class CartDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }   
}
