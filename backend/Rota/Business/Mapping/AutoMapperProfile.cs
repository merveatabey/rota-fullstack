using System;
using AutoMapper;
using Entities;
using Rota.Entities.DTOs;

namespace Rota.Business.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tour, TourDto>().ReverseMap();
            CreateMap<Tour, PopularTourDto>().ReverseMap();
            CreateMap<Tour, TourDetailsDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<TourActivity, TourActivityDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<TourDay, TourDayDto>().ReverseMap();
            CreateMap<User, UserManagementDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();


            CreateMap<ReservationCreateDto, Reservation>();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.Tour, opt => opt.MapFrom(src => src.Tour))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));

            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();


            CreateMap<FavoriteTourAddDto, FavoriteTour>();

            CreateMap<FavoriteTour, FavoriteTourDto>()
                       .ForMember(dest => dest.TourName, opt => opt.MapFrom(src => src.Tour.Title))
                       .ForMember(dest => dest.TourDescription, opt => opt.MapFrom(src => src.Tour.Description))
                       .ForMember(dest => dest.TourImageUrl, opt => opt.MapFrom(src => src.Tour.ImageUrl))
                       .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Tour.Price))
                       .ReverseMap();
        }
    }
}

