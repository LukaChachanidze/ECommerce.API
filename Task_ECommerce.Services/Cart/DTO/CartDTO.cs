namespace Task_ECommerce.Services.Cart.DTO
{
    /// <summary>
    /// Data transfer object for cart
    /// </summary>
    public class CartDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product Price
        /// </summary>
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ItemId { get; set; }
    }   
}
