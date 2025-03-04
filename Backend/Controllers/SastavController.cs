﻿using AutoMapper;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    
        [ApiController]
        [Route("api/v1/[controller]")]
        public class SastavController(Backendcontext context, IMapper mapper) : GlavniController(context, mapper)
        {


            // RUTE
            [HttpGet]
            public ActionResult<List<SastavDTORead>> Get()
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { poruka = ModelState });
                }
                try
                {
                    return Ok(_mapper.Map<List<SastavDTORead>>(_context.Sastavi));
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }

            }


            [HttpGet]
            [Route("{sifra:int}")]
            public ActionResult<SastavDTOInsertUpdate> GetBySifra(int sifra)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { poruka = ModelState });
                }
                Sastav? e;
                try
                {
                    e = _context.Sastav.Find(sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound(new { poruka = "Sastav ne postoji u bazi" });
                }

                return Ok(_mapper.Map<SastavDTOInsertUpdate>(e));
            }

            [HttpPost]
            public IActionResult Post(SastavDTOInsertUpdate dto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { poruka = ModelState });
                }
                try
                {
                    var e = _mapper.Map<Sastav>(dto);
                    _context.Sastavi.Add(e);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created, _mapper.Map<SastavDTORead>(e));
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }



            }

            [HttpPut]
            [Route("{sifra:int}")]
            [Produces("application/json")]
            public IActionResult Put(int sifra, SastavDTOInsertUpdate dto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { poruka = ModelState });
                }
                try
                {
                    Sastav? e;
                    try
                    {
                        e = _context.Sastavi.Find(sifra);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { poruka = ex.Message });
                    }
                    if (e == null)
                    {
                        return NotFound(new { poruka = "Sastav ne postoji u bazi" });
                    }
                    e = _mapper.Map(dto, e);

                    _context.Sastavi.Update(e);
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
                    Sastav? e;
                    try
                    {
                        e = _context.Sastavi.Find(sifra);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { poruka = ex.Message });
                    }
                    if (e == null)
                    {
                        return NotFound("Sastav ne postoji u bazi");
                    }
                    _context.Sastavi.Remove(e);
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

