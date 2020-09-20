using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quotes.Models;

namespace Quotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        public static List<QuoteModel> QuotesList = new List<QuoteModel>()
        {
            new QuoteModel()
            {
                Id = 1,
                Author = "John Lennon",
                Quote = "Life is what happens when you’re busy making other plans.",
                Category = "Life",
                CreationTime = DateTime.Now.AddMinutes(-50)
            },
            new QuoteModel()
            {
                Id = 2,
                Author = "Unknow",
                Quote = "Get busy living or get busy dying.",
                Category = "Life",
                CreationTime = DateTime.Now.AddHours(-1)

            },
            new QuoteModel()
            {
                Id = 3,
                Author = "Harold Abelson",
                Quote = "Programs must be written for people to read, and only incidentally for machines to execute.",
                Category = "Programmers",
                CreationTime = DateTime.Now
            }
        };

        // GET api/all quotes
        [HttpGet]
        [Route("GetAllQuotes")]
        public IActionResult GetAllQuotes()
        {
            return Ok(QuotesList);
        }

        // GET api/quotes by category
        [HttpGet]
        [Route("GetAllQuotesByCategory")]
        public IActionResult GetAllQuotesByCategory(string category)
        {
            return Ok(QuotesList.Where(p => p.Category.ToLower().Contains(category.ToLower())));
        }

        // GET api/random quote
        [HttpGet]
        [Route("GetRandomQuote")]
        public IActionResult GetRandomQuote()
        {
            var quote = QuotesList[new Random().Next(QuotesList.Count)];
            return Ok(quote);
        }

        // CRUD api/add quote
        [HttpPost]
        [Route("AddQuote")]        
        public IActionResult AddQuote([FromBody] QuoteModel quote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxId = QuotesList.Select(p => p.Id).Max();
            
            maxId++; 
            quote.Id = maxId;
            quote.CreationTime = DateTime.Now;
            QuotesList.Add(quote);
            
            return Ok(new { massage = "Successfully added" });
        }
        
        // CRUD api/delete quote by id
        [Route("DeleteQuote/{id}")]
        [HttpDelete]
        public IActionResult DeleteQuote(int id)
        {
            if (QuotesList.Remove(QuotesList.Find(p => p.Id == id)))
            {
                return Ok(new { massage = "Successfully removed!" });
            }
            else
            {
                return BadRequest(new { massage = "Quote not found!" });
            }
        }

        // CRUD api/edit quote by id
        [Route("EditQuote/{id}")]
        [HttpPut]
        public IActionResult EditQuote([FromRoute] int id, [FromBody] QuoteModel quote)
        {
            bool isModified = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            QuotesList.ForEach(p =>
            {
                if (p.Id == id)
                {
                    p.Quote = quote.Quote;
                    p.Author = quote.Author;
                    p.Category = quote.Category;
                    isModified = true;
                }
            });

            if (isModified)
            {
                return Ok(new { massage = "Successfully updated!" });
            }
            else
            {
                return BadRequest(new { massage = "Not found" });
            }
        }

        public static List<SubscriberModel> subscribersList = new List<SubscriberModel>();

        [Route("Subscribe")]
        [HttpPost]
        public IActionResult Subscribe([FromBody] SubscriberModel subscribers)
        {
            subscribers.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            subscribersList.Add(subscribers);
            return Ok(new { massage = "Успешно подписанны" });
        }
    }
}
