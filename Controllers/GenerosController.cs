﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ORT_PNT1_Proyecto_2022_2C_I_ReservaEspectaculo.Data;
using ORT_PNT1_Proyecto_2022_2C_I_ReservaEspectaculo.Helpers;
using ORT_PNT1_Proyecto_2022_2C_I_ReservaEspectaculo.Models;

namespace ORT_PNT1_Proyecto_2022_2C_I_ReservaEspectaculo.Controllers
{
    public class GenerosController : Controller
    {
        private readonly CineContext _context;

        public GenerosController(CineContext context)
        {
            _context = context;
        }

        // GET: Generos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Generos.ToListAsync());
        }

        // GET: Generos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Generos == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genero == null)
            {
                return NotFound();
            }

            ViewBag.listaDePeliculas = _context.Peliculas.Where(p => p.GeneroId == id ).ToList();

            return View(genero);
        }

        // GET: Generos/Create
        [Authorize(Roles = Configs.Empleado)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Configs.Empleado)]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                _context.Generos.Add(genero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        // GET: Generos/Edit/5
        [Authorize(Roles = Configs.Empleado)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Generos == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        // POST: Generos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Configs.Empleado)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Genero genero)
        {
            if (id != genero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Generos.Update(genero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroExists(genero.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        // GET: Generos/Delete/5
        [Authorize(Roles = Configs.Empleado)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Generos == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // POST: Generos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Configs.Empleado)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Generos == null)
            {
                return Problem("Entity set 'CineContext.Generos'  is null.");
            }
            var genero = await _context.Generos.FindAsync(id);
            if (genero != null)
            {
                _context.Generos.Remove(genero);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneroExists(int id)
        {
          return _context.Generos.Any(e => e.Id == id);
        }
    }
}