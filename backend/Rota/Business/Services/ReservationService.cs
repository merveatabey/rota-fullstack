using System;
using AutoMapper;
using Entities;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;


namespace Rota.Business.Services
{
	public class ReservationService : IReservationService
	{
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
		{
            _unitOfwork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var reservations = await _unitOfwork.CustomReservation.GetReservationsWithTourAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> GetByIdAsync(int id)
        {
            var reservation = await _unitOfwork.CustomReservation.GetReservationWithDetailsAsync(id);
            if (reservation == null)
                throw new Exception("Reservation not found");

            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task CreateAsync(ReservationDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);
            await _unitOfwork.Reservations.AddAsync(reservation);
            await _unitOfwork.SaveAsync();
        }

        public async Task CreateAsync(ReservationCreateDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);

            reservation.ReservationDate = DateTime.Now;
            reservation.Status = "Rezerve";
            reservation.ExpirationDate = DateTime.Now.AddDays(10);


            // Tur fiyatı üzerinden toplam tutar hesapla
            var tour = await _unitOfwork.Tours.GetByIdAsync(dto.TourId);
            if (tour == null)
                throw new Exception("Tur bulunamadı.");

            reservation.TotalPrice = dto.GuestCount * tour.Price;

            await _unitOfwork.Reservations.AddAsync(reservation);
            await _unitOfwork.SaveAsync();

        }

        public async Task UpdateAsync(ReservationDto dto)
        {
            var reservation = await _unitOfwork.Reservations.GetByIdAsync(dto.Id);
            if(reservation == null)
            {
                throw new Exception("reservation not found");
            }
            _mapper.Map(dto, reservation);

            _unitOfwork.Reservations.Update(reservation);
            await _unitOfwork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await _unitOfwork.Reservations.GetByIdAsync(id);
            if (reservation == null)
            {
                throw new Exception("reservation not found");
            }
            _unitOfwork.Reservations.Delete(reservation);
            await _unitOfwork.SaveAsync();

        }

        public async Task<List<ReservationDto>> GetByUserIdAsync(Guid userId)
        {
            var reservations = await _unitOfwork.CustomReservation.GetReservationsByUserAsync(userId);
            return _mapper.Map<List<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> CreateWithDetailsAsync(ReservationCreateDto dto)
        {
            var reservation = new Reservation
            {
                UserId = dto.UserId,
                TourId = dto.TourId,
                AdultCount = dto.AdultCount,
                ChildCount = dto.ChildCount,
                Note = dto.Note,
                ReservationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(10),
                Status = "Bekliyor"
            };

            // Tur bilgisi üzerinden fiyat hesapla
            var tour = await _unitOfwork.Tours.GetByIdAsync(dto.TourId);
            if (tour == null)
                throw new Exception("Tur bulunamadı.");

            reservation.TotalPrice = dto.AdultCount * tour.Price + dto.ChildCount * (tour.Price * 0.5m); // örnek

            await _unitOfwork.Reservations.AddAsync(reservation);
            await _unitOfwork.SaveAsync();

            return _mapper.Map<ReservationDto>(reservation);
        }

      
    }
}

