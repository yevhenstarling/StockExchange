namespace DTO.Models
{
    public class PurchaseDto
    {
        public int Shares { get; set; }
        public decimal CostPerShare { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
