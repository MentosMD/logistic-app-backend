namespace LogisticApp.Entities
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public Point Location {  get; set; }
    }
}
