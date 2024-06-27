namespace Backend.API.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public float Amount { get;set; }
        public DateTime CreatedAt { get;set; }
    }
}