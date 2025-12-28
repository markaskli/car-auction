namespace CarAuction.Api.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string? ModifiedBy { get; set; } 
        public DateTime? ModifiedAt { get; set; }
    }
}
