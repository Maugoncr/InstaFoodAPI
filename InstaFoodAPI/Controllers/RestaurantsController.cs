using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InstaFoodAPI.Models;
using InstaFoodAPI.Attributes;

namespace InstaFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class RestaurantsController : ControllerBase
    {
        private readonly InstaFoodDBContext _context;

        private Tools.Crypto MyCrypto { get; set; }

        public RestaurantsController(InstaFoodDBContext context)
        {
            _context = context;

            MyCrypto = new Tools.Crypto();
        }


        // Controller para funcion del login del cliente

        [HttpGet("ValidateRestLogin")]
        public async Task<ActionResult<Restaurant>> ValidateRestLogin(string pEmail, string pPassword)
        {
            string ApiLevelEncriptedPassword = MyCrypto.EncriptarEnUnSentido(pPassword);
            var rest = await _context.Restaurants.SingleOrDefaultAsync(e => e.Email == pEmail &&
                                                                 e.Password == ApiLevelEncriptedPassword);

            //si no hay respuesta en la consulta se devuelte el mensaje http Not Found
            if (rest == null)
            {
                return NotFound();
            }
            return rest;
        }


        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return await _context.Restaurants.ToListAsync();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.IdRest)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST PARA RESTAURANTES
        // POST: api/Restaurants/PostRestEncriptado

        [HttpPost("PostRestEncriptado")]
        public async Task<ActionResult<Restaurant>> PostRestEncriptado(Restaurant restaurant)
        {
          
            string ApiLevelEncriptedPassword = MyCrypto.EncriptarEnUnSentido(restaurant.Password);
            restaurant.Password = ApiLevelEncriptedPassword;
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.IdRest }, restaurant);
        }


        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.IdRest }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.IdRest == id);
        }
    }
}
