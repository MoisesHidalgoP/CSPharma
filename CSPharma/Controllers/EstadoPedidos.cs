using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Modelo;
using Microsoft.CodeAnalysis.Differencing;
using Npgsql;
using Microsoft.Extensions.Logging;

namespace CSPharma.Controllers
{
    public class EstadoPedidos : Controller
    {
        private readonly CspharmaInformacionalContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<EstadoPedidos> _logger;

        
        public EstadoPedidos(CspharmaInformacionalContext context, ILogger<EstadoPedidos> logger, IConfiguration config)
        {
            _context = context;
            _logger = logger;
            _config = config;
        }

        //COn este metodo obtenemos todos los pedidos
        public async Task<IActionResult> Index()
        {
            var cspharmaInformacionalContext = _context.TdcTchEstadoPedidos.Include(t => t.CodEstadoDevolucionNavigation).Include(t => t.CodEstadoEnvioNavigation).Include(t => t.CodEstadoPagoNavigation).Include(t => t.CodLineaNavigation);
            return View(await cspharmaInformacionalContext.ToListAsync());
        }

        //Este método Details obtiene los detalles de un pedido de estado específico y los muestra.
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos
                .Include(t => t.CodEstadoDevolucionNavigation)
                .Include(t => t.CodEstadoEnvioNavigation)
                .Include(t => t.CodEstadoPagoNavigation)
                .Include(t => t.CodLineaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tdcTchEstadoPedido == null)
            {
                return NotFound();
            }

            return View(tdcTchEstadoPedido);
        }

        //El método Create muestra el formulario de creación de un nuevo pedido de estado
        //y luego agrega el nuevo pedido a la base de datos.
        public IActionResult Create()
        {
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion");
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio");
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago");
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdUuid,MdDate,Id,CodEstadoEnvio,CodEstadoPago,CodEstadoDevolucion,CodPedido,CodLinea")] TdcTchEstadoPedido tdcTchEstadoPedido)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(tdcTchEstadoPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion", tdcTchEstadoPedido.CodEstadoDevolucion);
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio", tdcTchEstadoPedido.CodEstadoEnvio);
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago", tdcTchEstadoPedido.CodEstadoPago);
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea", tdcTchEstadoPedido.CodLinea);
            return View(tdcTchEstadoPedido);
        }
        //El método Edit muestra el formulario de edición de un pedido de estado existente
        //y luego guarda los cambios en la base de datos.
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos.FindAsync(id);
            if (tdcTchEstadoPedido == null)
            {
                return NotFound();
            }
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion", tdcTchEstadoPedido.CodEstadoDevolucion);
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio", tdcTchEstadoPedido.CodEstadoEnvio);
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago", tdcTchEstadoPedido.CodEstadoPago);
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea", tdcTchEstadoPedido.CodLinea);
            return View(tdcTchEstadoPedido);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MdUuid,MdDate,Id,CodEstadoEnvio,CodEstadoPago,CodEstadoDevolucion,CodPedido,CodLinea")] TdcTchEstadoPedido tdcTchEstadoPedido)
        {
            if (id != tdcTchEstadoPedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tdcTchEstadoPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TdcTchEstadoPedidoExists(tdcTchEstadoPedido.Id))
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
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion", tdcTchEstadoPedido.CodEstadoDevolucion);
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio", tdcTchEstadoPedido.CodEstadoEnvio);
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago", tdcTchEstadoPedido.CodEstadoPago);
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea", tdcTchEstadoPedido.CodLinea);
            return View(tdcTchEstadoPedido);
        }

        //Recoge el pedido especifico y lo muestra para borrarlo
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos
                .Include(t => t.CodEstadoDevolucionNavigation)
                .Include(t => t.CodEstadoEnvioNavigation)
                .Include(t => t.CodEstadoPagoNavigation)
                .Include(t => t.CodLineaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tdcTchEstadoPedido == null)
            {
                return NotFound();
            }

            return View(tdcTchEstadoPedido);
        }

        //Confirmacion de borrado
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {

            if (_context.TdcTchEstadoPedidos == null)
            {
                return Problem("Entity set 'CspharmaInformacionalContext.TdcTchEstadoPedidos'  is null.");
            }
            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos.FindAsync(id);
            if (tdcTchEstadoPedido != null)
            {
                _context.TdcTchEstadoPedidos.Remove(tdcTchEstadoPedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TdcTchEstadoPedidoExists(long id)
        {
          return _context.TdcTchEstadoPedidos.Any(e => e.Id == id);
        }


        
    }
}
