using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourActivityController : ControllerBase
    {
        private readonly ITourActivityService _tourActivityService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TourActivityController(ITourActivityService tourActivityService, IWebHostEnvironment hostEnvironment)
        {
            _tourActivityService = tourActivityService;
            _hostEnvironment = hostEnvironment;
        }

        // GET api/TourActivity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourActivityDto>>> GetAll()
        {
            var activities = await _tourActivityService.GetAllAsync();
            return Ok(activities);
        }

        // GET api/TourActivity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TourActivityDto>> GetById(int id)
        {
            var activity = await _tourActivityService.GetByIdAsync(id);
            if (activity == null)
                return NotFound($"TourActivity with id {id} not found.");

            return Ok(activity);
        }

        // POST api/TourActivity
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Create([FromForm] TourActivityDto dto, IFormFile formFile )
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { message = "Geçersiz model", errors });
            }

            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwRootPath))
                return BadRequest("Web root path is null. 'wwwroot' klasörü eksik olabilir");

            if(formFile != null && formFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(formFile.FileName);
                string uploadRoot = Path.Combine(wwwRootPath, "img", "activity");
                Directory.CreateDirectory(uploadRoot);

                var newFilePath = Path.Combine(uploadRoot, fileName + extension);
                using var stream = new FileStream(newFilePath, FileMode.Create);
                await formFile.CopyToAsync(stream);

                dto.ActivityImage = $"img/activity/{fileName + extension}";

            }
            else
            {
                dto.ActivityImage ??= "img/activity/default.png";
            }

            await _tourActivityService.CreateAsync(dto);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Update(int id, [FromForm] TourActivityDto dto, [FromForm] IFormFile formFile)
        {
            //formFile = görsel dosyası

            dto.Id = id;

            var existing = await _tourActivityService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"TourActivity with id {id} not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string wwwRootPath = _hostEnvironment.WebRootPath;

            if (formFile != null && formFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString();            //yeni benzersiz bir dosya adı oluştur
                string extension = Path.GetExtension(formFile.FileName);        //kullanıcının yüklediği görselin uzantısını al
                string uploadRoot = Path.Combine(wwwRootPath, "img", "activity");       //görselin yükelenceği klasör yolu

                Directory.CreateDirectory(uploadRoot);

                // Önceki dosya yolunu al, dosya sisteminde bu dosya varsa eski görseli sil --sunucuda gereksiz resim birikmesini önler  
                if (!string.IsNullOrEmpty(dto.ActivityImage))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, dto.ActivityImage.TrimStart('\\', '/'));       
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                //yeni dosya yolu oluştur.
                //Görsel içeriği CopyToAsync ile asenkron şekilde diske kaydedilir.
                string newFilePath = Path.Combine(uploadRoot, fileName + extension);
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                dto.ActivityImage = Path.Combine("img", "activity", fileName + extension).Replace("\\", "/");
            }

            await _tourActivityService.UpdateAsync(dto);
            return Ok(dto);
        }

        // DELETE api/TourActivity/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _tourActivityService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"TourActivity with id {id} not found.");

            await _tourActivityService.DeleteAsync(id);
            return NoContent();
        }


      
       

    }
}
