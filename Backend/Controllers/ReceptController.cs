using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Controllers;
using Backend.Data;
using Backend.Models.DTO;
using Backend.Models;

[ApiController]
[Route("api/v1/[controller]")]
public class ReceptController : GlavniController
{
    private readonly BackendContext _context;
    private readonly IMapper _mapper;

    public ReceptController(BackendContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReceptDTORead>>> Get()
    {
        try
        {
            var recepti = await _context.Recepti.ToListAsync();
            return Ok(_mapper.Map<List<ReceptDTORead>>(recepti));
        }
        catch (Exception e)
        {
            return BadRequest(new { poruka = e.Message });
        }
    }

    [HttpGet("{sifra:int}")]
    public async Task<ActionResult<ReceptDTOInsertUpdate>> GetBySifra(int sifra)
    {
        try
        {
            var recept = await _context.Recepti.FindAsync(sifra);
            if (recept == null)
                return NotFound(new { poruka = "Recept ne postoji u bazi" });

            return Ok(_mapper.Map<ReceptDTOInsertUpdate>(recept));
        }
        catch (Exception e)
        {
            return BadRequest(new { poruka = e.Message });
        }
    }

    [HttpPut("{sifra:int}")]
    public async Task<IActionResult> Put(int sifra, [FromBody] ReceptDTOInsertUpdate dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { poruka = ModelState });

        try
        {
            var recept = await _context.Recepti.FindAsync(sifra);
            if (recept == null)
                return NotFound(new { poruka = "Recept ne postoji u bazi" });

            _mapper.Map(dto, recept);
            _context.Recepti.Update(recept);
            await _context.SaveChangesAsync();

            return Ok(new { poruka = "Uspješno promijenjeno" });
        }
        catch (Exception e)
        {
            return BadRequest(new { poruka = e.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReceptDTOInsertUpdate dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { poruka = ModelState });

        try
        {
            var recept = _mapper.Map<Recept>(dto);
            await _context.Recepti.AddAsync(recept);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBySifra), new { sifra = recept.Sifra }, _mapper.Map<ReceptDTORead>(recept));
        }
        catch (Exception e)
        {
            return BadRequest(new { poruka = e.Message });
        }
    }

    [HttpDelete("{sifra:int}")]
    public async Task<IActionResult> Delete(int sifra)
    {
        try
        {
            var recept = await _context.Recepti.FindAsync(sifra);
            if (recept == null)
                return NotFound(new { poruka = "Recept ne postoji u bazi" });

            _context.Recepti.Remove(recept);
            await _context.SaveChangesAsync();

            return Ok(new { poruka = "Uspješno obrisano" });
        }
        catch (Exception e)
        {
            return BadRequest(new { poruka = e.Message });
        }
    }
}
