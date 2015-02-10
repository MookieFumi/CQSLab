namespace CQS.Entities
{
    public class OrderLine
    {
        public virtual int OrderLineId { get; set; }
        public virtual Order Order { get; set; }
        public virtual int OrderId { get; set; }
        public virtual Product Product { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}