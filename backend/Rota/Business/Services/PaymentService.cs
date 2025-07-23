using System;
using AutoMapper;
using Entities;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
	public class PaymentService : IPaymentService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
		public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
		{
            _unitOfWork = unitOfWork;
            _mapper = mapper;
		}

        public async Task CreateAsync(PaymentDto dto)
        {
            var entity = _mapper.Map<Payment>(dto);
            await _unitOfWork.Payments.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null)
                throw new Exception("Payment not found.");

            _unitOfWork.Payments.Delete(payment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetByIdAsync(int id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> GetByReservationIdAsync(int reservationId)
        {
            var payment = await _unitOfWork.CustomPayment.GetPaymentByReservationIdAsync(reservationId);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task UpdateAsync(PaymentDto dto)
        {
            var existing = await _unitOfWork.Payments.GetByIdAsync(dto.Id);
            if (existing == null)
                throw new Exception("Payment not found.");

            _mapper.Map(dto, existing);
            _unitOfWork.Payments.Update(existing);
            await _unitOfWork.SaveAsync();
        }
    }
}

