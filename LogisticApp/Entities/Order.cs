namespace LogisticApp.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int TotalWeight { get; set; }
        public ICollection<Order> Items { get; set; } = new List<Order>();
    }
}
