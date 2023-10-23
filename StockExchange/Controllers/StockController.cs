using AutoMapper;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StockExchange.ViewModels;
using StockExchange.ViewModels.Models;

namespace StockExchange.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockManager _stockManager;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public StockController(IStockManager stockManager, IMapper mapper, ILogger<StockController> logger)
        {
            this._stockManager = stockManager;
            this._mapper = mapper;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            var stockViewModel = new StockViewModel();
            stockViewModel.Purchases = _mapper.Map<List<Purchase>>(_stockManager.GetPurchases());
            return View(stockViewModel);
        }

        [HttpPost]
        public IActionResult Index(SharesSellInfo sharesSellInfo)
        {
            if (ModelState.IsValid)
            {
                var sellResult = _mapper.Map<OperationResult>(_stockManager.SellShares(sharesSellInfo.SharesToSell, sharesSellInfo.PricePerShare));
                _logger.LogInformation("Shares sold at {DT} with message: { }", DateTime.UtcNow.ToLongTimeString(), sellResult.Message);

                var purchases = _mapper.Map<List<Purchase>>(_stockManager.GetPurchases());

                return View("Index", new StockViewModel { Purchases = purchases, Result = sellResult });
            }
            else
            {
                string errorMessages = string.Join("\n", ModelState.Where(item => item.Value.ValidationState == ModelValidationState.Invalid)
                    .Select(item => $"Wrong input for {item.Key}:\n {string.Join("\n", item.Value.Errors.Select(error => error.ErrorMessage))}"));
                _logger.LogWarning("Shares selling failed at {DT} with message: { }", DateTime.UtcNow.ToLongTimeString(), errorMessages);

                var purchases = _mapper.Map<List<Purchase>>(_stockManager.GetPurchases());

                return View("Index", new StockViewModel { Purchases = purchases, Result = new OperationResult() { Status = "Failed.", Message = errorMessages } });
            }
        }
    }
}
