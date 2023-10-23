using System.ComponentModel.DataAnnotations;

namespace StockExchange.ViewModels
{
    public class SharesSellInfo
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value equal or bigger than {1}")]
        public int SharesToSell { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value equal or bigger than {1}")]
        public decimal PricePerShare { get; set; }
    }
}
