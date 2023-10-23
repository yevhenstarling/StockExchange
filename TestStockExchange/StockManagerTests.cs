using AutoMapper;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using Moq;
using StockExchange.Infrastructure;

namespace TestStockExchange
{
    [TestClass]
    public class StockManagerTests
    {
        [TestMethod]
        [DataRow(100, 30, "Success.")]
        [DataRow(500, 40, "Failed.")]
        public void SellShares_Should_CalculateResults(int sharesSold, double pricePerShare, string resultStatus)
        {
            //Arrange
            //Repository
            Mock<IPurchaseRepository> mockRepo = new Mock<IPurchaseRepository>();
            Queue<Purchase> purchaseQueue = new Queue<Purchase>();

            //Hardcoded starting values
            purchaseQueue.Enqueue(new Purchase { Shares = 100, CostPerShare = 20, PurchaseDate = new DateTime(2023, 1, 1) });
            purchaseQueue.Enqueue(new Purchase { Shares = 150, CostPerShare = 30, PurchaseDate = new DateTime(2023, 2, 1) });
            purchaseQueue.Enqueue(new Purchase { Shares = 120, CostPerShare = 10, PurchaseDate = new DateTime(2023, 3, 1) });

            mockRepo.Setup(m => m.GetPurchases()).Returns(purchaseQueue);

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PurchaseProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var stockManager = new StockManager(mockRepo.Object, mockMapper.CreateMapper());

            //Act
            var result = stockManager.SellShares(sharesSold, Convert.ToDecimal(pricePerShare));

            //Assert
            Assert.AreEqual(resultStatus, result.Status);
        }
    }
}