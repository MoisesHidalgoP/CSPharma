using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Modelo;
using Npgsql;

namespace CSPharma.Controllers
{
    public class Usuarios : Controller
    {
        private readonly CspharmaInformacionalContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<EstadoPedidos> _logger;

        public Usuarios(CspharmaInformacionalContext context, ILogger<EstadoPedidos> logger, IConfiguration config)
        {
            _context = context;
            _logger = logger;
            _config = config;
        }

        // Devuelve una lista de usuarios usando el contexto de la base de datos y lo envía a la vista correspondiente.
        public async Task<IActionResult> Index()
        {
            var cspharmaInformacionalContext = _context.DlkCatAccEmpleados.Include(d => d.NivelAccesoEmpleadoNavigation);
            return View(await cspharmaInformacionalContext.ToListAsync());
        }

        // Busca en la base de datos la información detallada del usuario, según su identificación proporcionada.
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DlkCatAccEmpleados == null)
            {
                return NotFound();
            }

            var dlkCatAccEmpleado = await _context.DlkCatAccEmpleados
                .Include(d => d.NivelAccesoEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.CodEmpleado == id);
            if (dlkCatAccEmpleado == null)
            {
                return NotFound();
            }

            return View(dlkCatAccEmpleado);
        }

        // Muestra un formulario para que los usuarios puedan agregar un nuevo usuario.
        // El [HttpPost] se encarga de manejar la solicitud de envío y procesar la información del usuario.
        public IActionResult Create()
        {
            ViewData["NivelAccesoEmpleado"] = new SelectList(_context.DlkCatRoles, "NivelAccesoEmpleado", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdUuid,CodEmpleado,ClaveEmpleado,MdDate,NivelAccesoEmpleado")] DlkCatAccEmpleado dlkCatAccEmpleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dlkCatAccEmpleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NivelAccesoEmpleado"] = new SelectList(_context.DlkCatRoles, "NivelAccesoEmpleado", "Descripcion", dlkCatAccEmpleado.NivelAccesoEmpleado);
            return View(dlkCatAccEmpleado);
        }

        // //El método Edit muestra el formulario de edición de un usuario existente
        //y luego guarda los cambios en la base de datos.
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DlkCatAccEmpleados == null)
            {
                return NotFound();
            }

            var dlkCatAccEmpleado = await _context.DlkCatAccEmpleados.FindAsync(id);
            if (dlkCatAccEmpleado == null)
            {
                return NotFound();
            }
            ViewData["NivelAccesoEmpleado"] = new SelectList(_context.DlkCatRoles, "NivelAccesoEmpleado", "Descripcion", dlkCatAccEmpleado.NivelAccesoEmpleado);
            return View(dlkCatAccEmpleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MdUuid,CodEmpleado,ClaveEmpleado,MdDate,NivelAccesoEmpleado")] DlkCatAccEmpleado dlkCatAccEmpleado)
        {
            if (id != dlkCatAccEmpleado.CodEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dlkCatAccEmpleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DlkCatAccEmpleadoExists(dlkCatAccEmpleado.CodEmpleado))
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
            ViewData["NivelAccesoEmpleado"] = new SelectList(_context.DlkCatRoles, "NivelAccesoEmpleado", "Descripcion", dlkCatAccEmpleado.NivelAccesoEmpleado);
            return View(dlkCatAccEmpleado);
        }

        //Recoge el usuario especifico y lo muestra para borrarlo
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DlkCatAccEmpleados == null)
            {
                return NotFound();
            }

            var dlkCatAccEmpleado = await _context.DlkCatAccEmpleados
                .Include(d => d.NivelAccesoEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.CodEmpleado == id);
            if (dlkCatAccEmpleado == null)
            {
                return NotFound();
            }

            return View(dlkCatAccEmpleado);
        }

        // Confirmacion de borrado de usuario.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DlkCatAccEmpleados == null)
            {
                return Problem("Entity set 'CspharmaInformacionalContext.DlkCatAccEmpleados'  is null.");
            }
            var dlkCatAccEmpleado = await _context.DlkCatAccEmpleados.FindAsync(id);
            if (dlkCatAccEmpleado != null)
            {
                _context.DlkCatAccEmpleados.Remove(dlkCatAccEmpleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DlkCatAccEmpleadoExists(string id)
        {
          return _context.DlkCatAccEmpleados.Any(e => e.CodEmpleado == id);
        }



    }
}
