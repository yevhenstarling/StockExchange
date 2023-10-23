namespace DAL.Models
{
    public class Purchase
    {
        public int Shares { get; set; }
        public decimal CostPerShare { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
