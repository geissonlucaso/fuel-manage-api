﻿using FuelManageAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuelManageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Veiculos.ToListAsync();

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Veiculo model)
        {
            // Quando números inteiros não são informados no model será atribuído o valor 0.
            // Para evitar isso, retornar BadRequest.
            if (model.AnoFabricacao <= 0 || model.AnoModelo <= 0)
            {
                // Erro 400.
                //return BadRequest(ModelState);
                return BadRequest(new { message = "Ano de Fabricação e Ano do Modelo são obrigatórios e devem ser maiores que zero." });
            }

            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id}, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Veiculos
                .FirstOrDefaultAsync(v => v.Id == id);

            if (model == null) NotFound();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model)
        {
            if (id != model.Id) return BadRequest();

            var modelDb = await _context.Veiculos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (modelDb == null) return NotFound();

            _context.Veiculos.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Veiculos
                .FindAsync(id);

            if (model == null) NotFound();

            _context.Veiculos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
