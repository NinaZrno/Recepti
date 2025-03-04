using AutoMapper;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReceptController : ControllerBase
    {
        private readonly BackendContext _context;

        public ReceptController (BackendContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ReceptDTORead>> Get()
        {
            try
            {   
                return Ok(_mapper.Map<List<ReceptDTORead>>(_context.Recepti));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{sifra:int}")]
        public ActionResult<ReceptDTOInsertUpdate> GetBySifra(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            Recept? e;
            try
            {
                e = _context.Recepti.Find(sifra);
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
            if (e == null)
            {
                return NotFound(new { poruka = "Recept ne postoji u bazi" });
            }

            return Ok(_mapper.Map<ReceptDTOInsertUpdate>(e));
        }

        [HttpPut]
        [Route("{sifra:int}")]
        [Produces("application/json")]

        public IActionResult Put (int sifra, Recept recept, ReceptDTOInsertUpdate dto
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                Recept? e;
                try
                {
                    e = _context.Recepti.Find(sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound(new { poruka = "Recept ne postoji u bazi" });
                }

                e = _mapper.Map(dto, e);

                _context.Recepti.Update(e);
                _context.SaveChanges();

                return Ok(new { poruka = "Uspješno promjenjeno" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }

        }

        [HttpPost]
        public IActionResult Post (ReceptDTOInsertUpdate dto
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                var e = _mapper.Map<Recept>(dto);
                _context.Recepti.Add(e);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<ReceptDTORead>(e));
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
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
