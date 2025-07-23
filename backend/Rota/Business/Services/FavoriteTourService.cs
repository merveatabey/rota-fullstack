using System;
using AutoMapper;
using Entities;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
    public class FavoriteTourService : IFavoriteTourService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavoriteTourService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddFavoriteAsync(Guid userId, FavoriteTourAddDto dto)
        {
            // Favori zaten var mı kontrolü - CustomFavorite kullanıyoruz
            var exists = await _unitOfWork.CustomFavorite.GetByUserIdAndTourIdAsync(userId, dto.TourId);
            if (exists != null) return false;

            var favorite = _mapper.Map<FavoriteTour>(dto);
            favorite.UserId = userId;

            await _unitOfWork.FavoriteTours.AddAsync(favorite); // Generic repository kullanımı
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task CreateAsync(FavoriteTourDto dto)
        {
            var entity = _mapper.Map<FavoriteTour>(dto);
            await _unitOfWork.FavoriteTours.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.FavoriteTours.GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.FavoriteTours.Delete(entity);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<FavoriteTourDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.FavoriteTours.GetAllAsync();
            return _mapper.Map<IEnumerable<FavoriteTourDto>>(entities);
        }

        public async Task<FavoriteTourDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.FavoriteTours.GetByIdAsync(id);
            return _mapper.Map<FavoriteTourDto>(entity);
        }

        public async Task<IEnumerable<FavoriteTourDto>> GetFavoritesByUserIdAsync(Guid userId)
        {
            var favorites = await _unitOfWork.CustomFavorite.GetFavoritesByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<FavoriteTourDto>>(favorites);
        }

        public async Task<bool> RemoveFavoriteAsync(Guid userId, int tourId)
        {
            var favorite = await _unitOfWork.CustomFavorite.GetByUserIdAndTourIdAsync(userId, tourId);
            if (favorite == null) return false;

            _unitOfWork.FavoriteTours.Delete(favorite); // Generic repository kullanımı
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task UpdateAsync(FavoriteTourAddDto dto)
        {
            var entity = _mapper.Map<FavoriteTour>(dto);
            _unitOfWork.FavoriteTours.Update(entity);
            await _unitOfWork.SaveAsync(); throw new NotImplementedException();
        }

        public async Task UpdateAsync(FavoriteTourDto dto)
        {
            var entity = _mapper.Map<FavoriteTour>(dto);
            _unitOfWork.FavoriteTours.Update(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}

