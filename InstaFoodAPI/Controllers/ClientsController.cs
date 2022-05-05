using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InstaFoodAPI.Models;

namespace InstaFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly InstaFoodDBContext _context;

        private Tools.Crypto MyCrypto { get; set; }

        public ClientsController(InstaFoodDBContext context)
        {
            _context = context;
            MyCrypto = new Tools.Crypto();
        }

        // Controller para funcion del login del cliente

        [HttpGet("ValidateClientLogin")]
        public async Task<ActionResult<Client>> ValidateClientLogin(string pEmail, string pPassword)
        {
            string ApiLevelEncriptedPassword = MyCrypto.EncriptarEnUnSentido(pPassword);
            var user = await _context.Clients.SingleOrDefaultAsync(e => e.Email == pEmail &&
                                                                 e.Password == ApiLevelEncriptedPassword);

            //si no hay respuesta en la consulta se devuelte el mensaje http Not Found
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }


        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.IdClient)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostClientEncriptado")]
        public async Task<ActionResult<Client>> PostClientEncriptado(Client client)
        {

            //El password ya viene encriptado desde la app, por un asunto de seguridad (si alguien intercepta el request
            //no va poder entender que password digito el usuario.)
            //ademas de esa encriptacion acá se volvera a encriptar con otra llave para que aunque se pueda copiar el
            //password (a nivel del app) no se pueda usar contra la base de datos.

            string ApiLevelEncriptedPassword = MyCrypto.EncriptarEnUnSentido(client.Password);

            client.Password = ApiLevelEncriptedPassword;


            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
        }


        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.IdClient == id);
        }
    }
}
