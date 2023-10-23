namespace DTO.Models
{
    public class OperationResultDto
    {
        public int RemainingShares { get; set; }
        public decimal CostBasisPerSoldShare { get; set; }
        public decimal CostBasisPerRemainingShare { get; set; }
        public decimal TotalProfitOrLoss { get; set; }
        public string Status { get; set; } //TODO: Status shold be enum
        public string Message { get; set; }
    }
}
