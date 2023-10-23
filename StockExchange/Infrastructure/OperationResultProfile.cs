using AutoMapper;
using DTO.Models;
using StockExchange.ViewModels.Models;

namespace StockExchange.Infrastructure
{
    public class OperationResultProfile : Profile
    {
        public OperationResultProfile()
        {
            CreateMap<OperationResult, OperationResultDto>();
            CreateMap<OperationResultDto, OperationResult>();
        }
    }
}
