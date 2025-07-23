using System;
using AutoMapper;
using Entities;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
    public class TourDayService : ITourDayService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        
		public TourDayService(IUnitOfWork unitOfWork, IMapper mapper)
		{
            _unitOfWork = unitOfWork;
            _mapper = mapper;
		}

        public async Task CreateAsync(TourDayDto dto)
        {
            var tourDay = _mapper.Map<TourDay>(dto);
            await _unitOfWork.TourDays.AddAsync(tourDay);
            await _unitOfWork.SaveAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var tourDay = await _unitOfWork.TourDays.GetByIdAsync(id);
            if (tourDay == null)
            {
                throw new Exception("Tour not found");
            }

            _unitOfWork.TourDays.Delete(tourDay);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TourDayDto>> GetAllAsync()
        {
            var tourDay = await _unitOfWork.TourDays.GetAllAsync();
            return _mapper.Map<IEnumerable<TourDayDto>>(tourDay);
        }

        public async Task<TourDayDto> GetByIdAsync(int id)
        {
            var tourDay = await _unitOfWork.TourDays.GetByIdAsync(id);
            if (tourDay == null)
                throw new Exception("TourDay not found");
            return _mapper.Map<TourDayDto>(tourDay);
        }

        public async Task UpdateAsync(TourDayDto dto)
        {
            var tourDay = await _unitOfWork.TourDays.GetByIdAsync(dto.Id);
            if (tourDay == null)
                throw new Exception("TourDay not found");
            _mapper.Map(dto, tourDay);
            _unitOfWork.TourDays.Update(tourDay);
            await _unitOfWork.SaveAsync();

        }
    }
}

