using StockExchange.ViewModels.Models;

namespace StockExchange.ViewModels
{
    public class StockViewModel
    {
        public List<Purchase> Purchases { get; set; }

        public OperationResult Result { get; set; }
    }
}
