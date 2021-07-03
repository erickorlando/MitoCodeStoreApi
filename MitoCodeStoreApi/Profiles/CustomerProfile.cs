using AutoMapper;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;

namespace MitoCodeStoreApi.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDtoSingleResponse>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CustomerBirth, opt => opt.MapFrom(src => src.BirthDate.ToString(Constants.DateFormat)))
                .ForMember(dest => dest.CustomerNumberId, opt => opt.MapFrom(src => src.NumberId))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();


            CreateMap<CustomerDtoRequest, Customer>();
        }
    }
}