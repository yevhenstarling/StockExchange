using AutoMapper;
using DAL.Models;
using DAL.Repositories;
using DTO.Models;

namespace BLL.Services
{
    public class StockManager : IStockManager
    {
        private readonly IPurchaseRepository _purchaseRepository;

        private readonly IMapper _mapper;

        public StockManager(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            this._purchaseRepository = purchaseRepository;
            this._mapper = mapper;
        }

        public List<PurchaseDto> GetPurchases()
        {
            return _mapper.Map<List<PurchaseDto>>(_purchaseRepository.GetPurchases());
        }

        //TODO: here can be added some more variated processing, (other from FIFO method, calculation of the potential revenue in case of future dividents/taxes ...)
        public OperationResultDto SellShares(int sharesToSell, decimal pricePerShare)
        {
            var purchaseQueue = _purchaseRepository.GetPurchases();
            var existingSharesAmount = purchaseQueue.Sum(p => p.Shares);

            //validation max sharesToSell
            if (sharesToSell > existingSharesAmount)
            {
                return new OperationResultDto
                {
                    RemainingShares = existingSharesAmount,
                    CostBasisPerSoldShare = 0,
                    CostBasisPerRemainingShare = 0,
                    TotalProfitOrLoss = 0,
                    Status = $"Failed.",
                    Message = $"Not enought shares to buy."
                };
            }

            int remainingShares = sharesToSell;
            decimal costBasisSold = 0;
            decimal costBasisRemaining = 0;
            decimal totalProfitOrLoss = 0;

            while (purchaseQueue.Count > 0 && remainingShares > 0)
            {
                Purchase purchase = purchaseQueue.Peek();

                if (purchase.Shares >= remainingShares)
                {
                    // This purchase can fulfill all or some of the remaining shares to be sold
                    costBasisSold += remainingShares * purchase.CostPerShare;
                    totalProfitOrLoss += (pricePerShare - purchase.CostPerShare) * remainingShares;

                    if (purchase.Shares == remainingShares)
                    {
                        // Remove the purchase since all shares in this lot are sold
                        purchaseQueue.Dequeue();
                    }
                    else
                    {
                        // Update the purchase with remaining shares
                        purchase.Shares -= remainingShares;
                    }

                    remainingShares = 0;
                }
                else
                {
                    // This purchase is fully sold, but not enough to meet all remaining shares to be sold
                    costBasisSold += purchase.Shares * purchase.CostPerShare;
                    totalProfitOrLoss += (pricePerShare - purchase.CostPerShare) * purchase.Shares;
                    remainingShares -= purchase.Shares;

                    // Remove this purchase since it's fully sold
                    purchaseQueue.Dequeue();
                }
            }

            var remainingSharesAmount = existingSharesAmount - sharesToSell;
            costBasisRemaining = purchaseQueue.Sum(p => p.Shares * p.CostPerShare);

            return new OperationResultDto
            {
                RemainingShares = purchaseQueue.Sum(p => p.Shares),
                CostBasisPerSoldShare = costBasisSold / sharesToSell,
                CostBasisPerRemainingShare = (remainingSharesAmount != 0) ? costBasisRemaining / remainingSharesAmount : 0,
                TotalProfitOrLoss = totalProfitOrLoss,
                Status = $"Success.",
                Message = $"Shares successfully sold."
            };
        }
    }
}
