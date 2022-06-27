using Microsoft.AspNetCore.Mvc;
using WebApplication1.data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;
        public CardsController(CardsDbContext cardsDbContext)
        {
            this.cardsDbContext = cardsDbContext;
        }
        // get all cards 
        [HttpGet]

        public async Task <IActionResult> getAllCards()
        {
            var cards = await cardsDbContext.cards.ToListAsync();
            return Ok(cards);
        }
        //get a single card with the id
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("getCard")]
        public async Task<IActionResult> getCard([FromRoute] Guid id)
        {
            var card = await cardsDbContext.cards.FirstOrDefaultAsync(x => x.id == id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("card not found"); 
        }
        //adding a card 
        [HttpPost]
        public async Task<IActionResult> addCard([FromBody] Card card)
        {
            card.id = Guid.NewGuid(); 
            await cardsDbContext.cards
                .AddAsync(card);
            await cardsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(getCard), new { id = card.id }, card);
        }
        // update a card 
        [HttpPut]
        [Route("{id:guid}")]
        public async 
            Task<IActionResult> updateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await cardsDbContext.cards.FirstOrDefaultAsync(x => x.id == id);
            if (existingCard != null)
            {
                existingCard.carHolderName = card.carHolderName;
                existingCard.cardNumber = card.cardNumber;
                existingCard.expiryDate = card.expiryDate;
                existingCard.expiryYear = card.expiryYear;
                existingCard.CVC = card.CVC;
                await cardsDbContext.SaveChangesAsync();
                return
                     Ok(existingCard);
            }
            return NotFound("card not found");
        }
        //delete a card with an id
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> deleteCard([FromRoute] Guid id )
        {
            var existingCard = await cardsDbContext.cards.FirstOrDefaultAsync(x => x.id == id);
            if (existingCard !=null)
            {
                cardsDbContext.Remove(existingCard);
                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("card not found");
        }
    }
}
