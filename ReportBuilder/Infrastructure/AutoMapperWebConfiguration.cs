
using System.Linq;
using AutoMapper;
using ReportBuilder.Models;
using ReportBuilder.ViewModels;

namespace ReportBuilder.Infrastructure
{
    public class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            ConfigureOrderMapping();
        }

        private static void ConfigureOrderMapping()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderViewModel>()
                  .ForMember(
                      dest => dest.OrderId,
                      opt => opt.MapFrom(src => src.ID))
                  .ForMember(
                      dest => dest.ProductName,
                      opt => opt.MapFrom(src => src.OrderDetail.FirstOrDefault().Product.Name))
                  .ForMember(
                      dest => dest.Quantity,
                      opt => opt.MapFrom(src => src.OrderDetail.FirstOrDefault().Quantity))
                  .ForMember(
                      dest => dest.UnitPrice,
                      opt => opt.MapFrom(src => src.OrderDetail.FirstOrDefault().UnitPrice)));
        }
    }
}