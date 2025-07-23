using System;
using AutoMapper;
using Entities;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
	public class HotelService : IHotelService
	{

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
      

        public async Task CreateAsync(HotelDto dto)
        {

            var hotels = _mapper.Map<Hotel>(dto);

            await _unitOfWork.Hotels.AddAsync(hotels);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hotels = await _unitOfWork.Hotels.GetByIdAsync(id);
            if(hotels == null)
            {
                throw new Exception("Hotel not found");
            }

            _unitOfWork.Hotels.Delete(hotels);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<HotelDto>> GetAllAsync()
        {
            var hotels = await _unitOfWork.Hotels.GetAllAsync();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);

        }

        public async Task<HotelDto> GetByIdAsync(int id)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAsync(id);
            if(hotel == null)
            {
                throw new Exception("hotel not found");
            }

            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task UpdateAsync(HotelDto dto)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAsync(dto.Id);
            if (hotel == null)
            {
                throw new Exception("Hotel not found");
            }

            _mapper.Map(dto, hotel);

            // TourId'yi manuel set etmek gerekebilir:
            hotel.TourId = dto.TourId;


            _unitOfWork.Hotels.Update(hotel);
            await _unitOfWork.SaveAsync();
        }
    }
}

