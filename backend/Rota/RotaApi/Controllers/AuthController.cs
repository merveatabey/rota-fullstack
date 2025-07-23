using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {

                var token = await _authService.RegisterAsync(dto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _authService.GetByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized(new { message = "Kayıtlı kullanıcı bulunamadı" });

            if (user.Role != "User")
                return Unauthorized(new { message = "Bu alandan sadece kullanıcı girişi yapılabilir." });

            var token = await _authService.LoginAsync(dto);

            // ID burada dönülüyor
            var userDto = new
            {
                id = user.Id.ToString(),  // Guid olduğu için string'e çevir
                email = user.Email,
                role = user.Role,
                name = user.FullName
            };

            return Ok(new { token, user = userDto });  // frontend'e ID geliyor
        }


        [HttpPost("adminlogin")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginDto dto)
        {
            try
            {
                // Kullanıcı bilgilerini al
                var user = await _authService.GetByEmailAsync(dto.Email);

                if (user == null)
                    return Unauthorized(new { message = "Kullanıcı bulunamadı." });

                if (user.Role != "Admin")
                    return Unauthorized(new { message = "Bu alana sadece admin girişi yapılabilir." });


                // Giriş başarılı, token oluştur
                var token = await _authService.LoginAsync(dto);

                // ID burada dönülüyor
                var userDto = new
                {
                    id = user.Id.ToString(),  // Guid olduğu için string'e çevir
                    email = user.Email,
                    role = user.Role,
                    name = user.FullName
                };

                return Ok(new { token,user = userDto});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        //Kullanıcıya şifre sıfırlama linki veya geçici token içeren mail gönderir.
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            try
            {
                await _authService.ForgotPasswordAsync(dto);
                return Ok(new { message = "If the email is correct, a reset link has been sent." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //kullanıcı yeni şifre oluşturur
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            try
            {
                await _authService.ResetPasswordAsync(dto);
                return Ok(new { message = "Password has been succesfully reset." });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

