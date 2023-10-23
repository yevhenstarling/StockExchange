namespace StockExchange.ViewModels.Models
{
    public class OperationResult
    {
        public int RemainingShares { get; set; }
        public decimal CostBasisPerSoldShare { get; set; }
        public decimal CostBasisPerRemainingShare { get; set; }
        public decimal TotalProfitOrLoss { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
