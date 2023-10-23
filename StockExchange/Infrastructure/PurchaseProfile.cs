using AutoMapper;
using DTO.Models;
using DALPurchase = DAL.Models.Purchase;
using ViewModelsPurchase = StockExchange.ViewModels.Models.Purchase;

namespace StockExchange.Infrastructure
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<DALPurchase, PurchaseDto>();
            CreateMap<PurchaseDto, DALPurchase>();
            CreateMap<ViewModelsPurchase, PurchaseDto>();
            CreateMap<PurchaseDto, ViewModelsPurchase>();
        }
    }
}
