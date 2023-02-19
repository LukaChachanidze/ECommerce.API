namespace Task_ECommerce.Services.Cart.DTO
{
    /// <summary>
    /// Data transfer object for a product in a cart
    /// </summary>
    public class CartProductDTO
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
