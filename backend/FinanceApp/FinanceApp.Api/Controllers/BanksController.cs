using FinanceApp.Application.DTOs;
using FinanceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BanksController : ControllerBase
    {
        private readonly IBankService _bankService;
        public BanksController(IBankService bankService)
        {
            _bankService = bankService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _bankService.GetAllAsync();
            return Ok(list);
        }
        [HttpGet("{id:int}")]

        public async Task<ActionResult> GetById(int id)
        {
            var item = await _bankService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("options")]
        public async Task<IActionResult> GetBankLookup()
        {
            var list = await _bankService.GetBankLookupAsync();
            return Ok(list);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBankDTO dto)
        {
            var created = await _bankService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BankId },
            created);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateBankDTO dto)
        {
            await _bankService.UpdateAsync(id, dto);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bankService.DeleteAsync(id);
            return NoContent();
        }
    }
}
