namespace Logistics.Core.Domain
{
    public class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
