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
                    return Ok(_mapper.Map<List<SastojakDTORead>>(_context.Sastojci));
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
                    e = _context.Sastojci.FirstOrDefault(g => g.Sifra == sifra);
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

                

                try
                {
                    var e = _mapper.Map<Sastojak>(dto);
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
                        e = _context.Sastojci.FirstOrDefault(x => x.Sifra == sifra);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { poruka = ex.Message });
                    }
                    if (e == null)
                    {
                        return NotFound(new { poruka = "Sastojak ne postoji u bazi" });
                    }

                   

                    e = _mapper.Map(dto, e);
                    _context.Sastojci.Update(e);
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




        }
    
}
