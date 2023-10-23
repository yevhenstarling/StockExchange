using DTO.Models;

namespace BLL.Services
{
    public interface IStockManager
    {
        List<PurchaseDto> GetPurchases();

        OperationResultDto SellShares(int sharesToSell, decimal pricePerShare);
    }
}
