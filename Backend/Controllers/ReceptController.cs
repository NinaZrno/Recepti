using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ReceptController : ControllerBase
    {
        private readonly BackendContext _context;

        public ReceptController (BackendContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Recepti);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{sifra:int}")]
        public IActionResult Get(int sifra)
        {
            if (sifra <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { poruka = "Šifra mora biti pozitivan broj" });

            }
            try
            {
                var recept = _context.Recepti.Find(sifra);
                if (recept == null)
                {
                    return NotFound(new { poruka = $"Recept s šifrom {sifra} ne postoji" });
                }
                return Ok(recept);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{sifra:int}")]
        public IActionResult Put (int sifra, Recept recept)
        {
            try
            {
                var receptBaza = _context.Recepti.Find(sifra);
                if (receptBaza == null)
                {
                    return NotFound(new { poruka = $"Recept s šifrom {sifra} ne postoji" });
                }

                receptBaza.Naziv = recept.Naziv;
                receptBaza.Vrsta = recept.Vrsta;
                receptBaza.Uputa = recept.Uputa;
                receptBaza.Trajanje = recept.Trajanje;

                _context.Recepti.Update(receptBaza);
                _context.SaveChanges();
                return Ok(receptBaza);
            }

            catch (Exception e)
            {


                return BadRequest(e);

            }

        }

        [HttpPost]
        public IActionResult Post (Recept recept)
        {
            try
            {
                _context.Recepti.Add(recept);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, recept);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            if (sifra <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { poruka = "Šifra mora biti pozitivan broj" });
            }
            try
            {
                var recept= _context.Recepti.Find(sifra);
                if (recept == null)
                {
                    return NotFound(new { poruka = $"Recept s šifrom {sifra} ne postoji" });
                }
                _context.Recepti.Remove(recept);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }






    }
}
