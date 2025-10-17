using FinanceApp.Application.DTOs;
using FinanceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CardController:ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cardDetail = await _cardService.GetAllAsync();
            return cardDetail == null ? NotFound() : Ok(cardDetail);
        }

        [HttpGet("getDetail")]
        public async Task<IActionResult> GetDetail()
        {
            var cardDetail = await _cardService.GetDetailAsync();
            return cardDetail == null ? NotFound() : Ok(cardDetail);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var card = await _cardService.GetByIdAsync(id);
            return card == null ? NotFound() : Ok(card);
        }


        [HttpGet("getCardTypeLookup")]
        public async Task<IActionResult> GetCardTypeLookup()
        {
            var list = await _cardService.GetCardTypeLookupAsync();
            return Ok(list);
        }

        [HttpPost]
       public async Task<IActionResult> AddCard([FromBody] CreateCardDTO cardDto)
        {
            var newCard = await _cardService.CreateAsync(cardDto);
            return CreatedAtAction(nameof(GetById), new{ id = newCard.CardId },newCard);
        }

        [HttpPut ("{id:int}")]
        public async Task<IActionResult> UpdateCard(int id,[FromBody] UpdateCardDTO cardDto)
        {
             await _cardService.UpdateAsync(id,cardDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            await _cardService.DeleteAsync(id);
            return NoContent();
        }
    }
}
