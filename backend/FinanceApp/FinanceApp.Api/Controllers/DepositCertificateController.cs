using FinanceApp.Application.DTOs;
using FinanceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositCertificateController : ControllerBase
    {

        private readonly IDepositCertificateService _certificationService;
        public DepositCertificateController(IDepositCertificateService certificationService)
        {
            _certificationService = certificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _certificationService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetDetail()
        {
            var certifications = await _certificationService.GetDetailAsync();
            return certifications == null ? NotFound() : Ok(certifications);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _certificationService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepositCertificateDTO dto)
        {
            var created = await _certificationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CertificateId },
            created);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepositCertificateDTO dto)
        {
            await _certificationService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _certificationService.DeleteDepositCertificateAsync(id);
            return NoContent();
        }


    }
}
