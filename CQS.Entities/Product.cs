namespace CQS.Entities
{
    public class Product
    {
        public virtual int ProductId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Category { get; set; }
        public virtual string Tag { get; set; }
        public virtual string ImageUrl { get; set; }
    }
}