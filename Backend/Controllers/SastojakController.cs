using AutoMapper;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Backend.Controllers
{
   
    
        [ApiController]
        [Route("api/v1/[controller]")]
        public class SastojakController(BackendContext context, IMapper mapper) : GlavniController(context, mapper)
        {


            // RUTE
            [HttpGet]
            public ActionResult<List<SastojakDTORead>> Get()
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { poruka = ModelState });
                }
                try
                {
                    return Ok(_mapper.Map<List<SastojakDTORead>>(_context.Grupe.Include(g => g.Recept)));
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }

            }


            [HttpGet]
            [Route("{sifra:int}")]
            public ActionResult<SastojakDTOInsertUpdate> GetBySifra(int sifra)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { poruka = ModelState });
                }
                Sastojak? e;
                try
                {
                    e = _context.Recepti.Include(g => g.Smjer).FirstOrDefault(g => g.Sifra == sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound(new { poruka = "Sastojak ne postoji u bazi" });
                }

                return Ok(_mapper.Map<SastojakDTOInsertUpdate>(e));
            }

            [HttpPost]
            public IActionResult Post(SastojakDTOInsertUpdate dto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { poruka = ModelState });
                }

                Recept? es;
                try
                {
                    es = _context.Recepti.Find(dto.ReceptSifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (es == null)
                {
                    return NotFound(new { poruka = "Smjer na grupi ne postoji u bazi" });
                }

                try
                {
                    var e = _mapper.Map<Sastojak>(dto);
                    e.Recept= es;
                    _context.Sastojci.Add(e);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created, _mapper.Map<SastojakDTORead>(e));
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }



            }

            [HttpPut]
            [Route("{sifra:int}")]
            [Produces("application/json")]
            public IActionResult Put(int sifra, SastojakDTOInsertUpdate dto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { poruka = ModelState });
                }
                try
                {
                Sastojak? e;
                    try
                    {
                        e = _context.Sastojci.Include(g => g.Recept).FirstOrDefault(x => x.Sifra == sifra);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { poruka = ex.Message });
                    }
                    if (e == null)
                    {
                        return NotFound(new { poruka = "Sastojak ne postoji u bazi" });
                    }

                    Recept? es;
                    try
                    {
                        es = _context.Sastav.Find(dto.Receptsifra);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { poruka = ex.Message });
                    }
                    if (es == null)
                    {
                        return NotFound(new { poruka = "Recept na grupi ne postoji u bazi" });
                    }

                    e = _mapper.Map(dto, e);
                    e.Recept = es;
                    _context.Grupe.Update(e);
                    _context.SaveChanges();

                    return Ok(new { poruka = "Uspješno promjenjeno" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }

            }

            [HttpDelete]
            [Route("{sifra:int}")]
            [Produces("application/json")]
            public IActionResult Delete(int sifra)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { poruka = ModelState });
                }
                try
                {
                Sastojak? e;
                    try
                    {
                        e = _context.Sastojci.Find(sifra);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { poruka = ex.Message });
                    }
                    if (e == null)
                    {
                        return NotFound("Grupa ne postoji u bazi");
                    }
                    _context.Sastojci.Remove(e);
                    _context.SaveChanges();
                    return Ok(new { poruka = "Uspješno obrisano" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
            }


            [HttpGet]
            [Route("Polaznici/{sifraGrupe:int}")]
            public ActionResult<List<SastavDTORead>> GetSastavi(int sifraSastojka)
            {
                if (!ModelState.IsValid || sifraSastojka <= 0)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var p = _context.Grupe
                        .Include(i => i.Sastavi).FirstOrDefault(x => x.Sifra == sifraSastojka);
                    if (p == null)
                    {
                        return BadRequest("Ne postoji sastav s šifrom " + sifraSastojka + " u bazi");
                    }

                    return Ok(_mapper.Map<List<SastavDTORead>>(p.Sastavi));
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
            }



            [HttpPost]
            [Route("{sifra:int}/dodaj/{polaznikSifra:int}")]
            public IActionResult DodajSastav(int sifra, int sastojak)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (sifra <= 0 || sastojak <= 0)
                {
                    return BadRequest("Šifra sastava ili sastojka nije dobra");
                }
                try
                {
                    var sastav = _context.sastojak
                        .Include(g => g.sastavi)
                        .FirstOrDefault(g => g.Sifra == sifra);
                    if (sastav == null)
                    {
                        return BadRequest("Ne postoji sastav s šifrom " + sifra + " u bazi");
                    }
                    var podrijetlo = _context.Sastavi.Find(sastojak);
                    if (sastav == null)
                    {
                        return BadRequest("Ne postoji sastojak s šifrom " + sastojak + " u bazi");
                    }
                    sastav.Sastavi.Add(sastav);
                    _context.Grupe.Update(sastav);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        poruka = "Recept " + sastav.Kolicina + " " + sastav.Napomena + " dodan na grupu "
                     + sastav.Naziv
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(
                           StatusCodes.Status503ServiceUnavailable,
                           ex.Message);
                }
            }


            [HttpDelete]
            [Route("{sifra:int}/obrisi/{polaznikSifra:int}")]
            public IActionResult ObrisiPolaznika(int sifra, int sastojakSifra)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (sifra <= 0 || sastojakSifra <= 0)
                {
                    return BadRequest("Šifra recepta ili sastojka nije dobra");
                }
                try
                {
                    var grupa = _context.Grupe
                        .Include(g => g.Polaznici)
                        .FirstOrDefault(g => g.Sifra == sifra);
                    if (grupa == null)
                    {
                        return BadRequest("Ne postoji grupa s šifrom " + sifra + " u bazi");
                    }
                    var Sastojak = _context.Polaznici.Find(sastojakSifra);
                    if (Sastojak == null)
                    {
                        return BadRequest("Ne postoji sastojak s šifrom " + sastojakSifra + " u bazi");
                    }
                    grupa.Polaznici.Remove(Sastojak);
                    _context.Grupe.Update(grupa);
                    _context.SaveChanges();

                    return Ok(new
                    {
                        poruka = "Sastojak " + Sastojak.Podrijetlo + " " + Sastojak.Naziv + " obrisan iz grupe "
                     + grupa.Naziv
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });

                }
            }


        }
    
}
