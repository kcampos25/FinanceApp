using FinanceApp.Application.DTOs;
using FinanceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _CurrencyService;
        public CurrencyController(ICurrencyService CurrencyService)
        {
            _CurrencyService = CurrencyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _CurrencyService.GetAllAsync();
            return Ok(list);
        }
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetById(int id)
        {
            var item = await _CurrencyService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("options")]
        public async Task<IActionResult> GetCurrencyLookup()
        {
            var list = await _CurrencyService.GetCurrencyLookupAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyDTO dto)
        {
            var created = await _CurrencyService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CurrencyId },
            created);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCurrencyDTO dto)
        {
            await _CurrencyService.UpdateAsync(id, dto);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _CurrencyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
