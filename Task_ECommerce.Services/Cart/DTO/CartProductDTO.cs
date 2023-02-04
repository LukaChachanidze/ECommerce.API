namespace Task_ECommerce.Services.Cart.DTO
{
    /// <summary>
    /// Data transfer object for a product in a cart
    /// </summary>
    public class CartProductDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
