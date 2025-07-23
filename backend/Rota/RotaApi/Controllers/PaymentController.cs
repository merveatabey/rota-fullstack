using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Business.Services;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet("by-reservation/{reservationId}")]
        public async Task<IActionResult> GetByReservationId(int reservationId)
        {
            var payment = await _paymentService.GetByReservationIdAsync(reservationId);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentDto dto)
        {
            await _paymentService.CreateAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentDto dto)
        {
            await _paymentService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _paymentService.DeleteAsync(id);
            return Ok();
        }

    

    }
}

