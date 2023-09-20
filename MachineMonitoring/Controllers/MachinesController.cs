using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MachineMonitoring.Data;
using MachineMonitoring.Data.Models;
using MassTransit;
using EventContracts.Enums;
using EventContracts.Models;

namespace MachineMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly MachineMonitoringDbContext _context;
        private IBus _bus;

        public MachinesController(MachineMonitoringDbContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        // GET: api/Machines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Machine>>> GetMachines()
        {
          if (_context.Machines == null)
          {
              return NotFound();
          }
            return await _context.Machines.ToListAsync();
        }

        // GET: api/Machines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Machine>> GetMachine(int id)
        {
          if (_context.Machines == null)
          {
              return NotFound();
          }
            var machine = await _context.Machines.FindAsync(id);

            if (machine == null)
            {
                return NotFound();
            }

            return machine;
        }

        // PUT: api/Machines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMachine(int id, Machine machine)
        {
            if (id != machine.MachineId)
            {
                return BadRequest();
            }

            _context.Entry(machine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Machines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Machine>> PostMachine(Machine machine)
        {
          if (_context.Machines == null)
          {
              return Problem("Entity set 'MachineMonitoringDbContext.Machines'  is null.");
          }
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMachine", new { id = machine.MachineId }, machine);
        }

        // DELETE: api/Machines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            if (_context.Machines == null)
            {
                return NotFound();
            }
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{workcenter}/starting")]
        public async Task<IActionResult> SetMachineStateSetup(string workcenter)
        {
            if (_context.Machines == null)
            {
                return NotFound();
            }
            var machine = _context.Machines.FirstOrDefault(m => m.WorkcenterId.Equals(workcenter));
            if (machine == null)
            {
                return NotFound();
            }

            if (machine.CurrentMachineState != DeviceState.Starting)
            {
                var newState = new StateChangeRequest() { RequestedDeviceState = DeviceState.Starting.ToString(), WorkcenterId = workcenter };

                _bus.Publish(newState);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{workcenter}/running")]
        public async Task<IActionResult> SetMachineStateRunning(string workcenter)
        {
            if (_context.Machines == null)
            {
                return NotFound();
            }
            var machine = _context.Machines.FirstOrDefault(m => m.WorkcenterId.Equals(workcenter));
            if (machine == null)
            {
                return NotFound();
            }

            if (machine.CurrentMachineState != DeviceState.Running)
            {
                var newState = new StateChangeRequest() { RequestedDeviceState = DeviceState.Starting.ToString(), WorkcenterId = workcenter };

                _bus.Publish(newState);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{workcenter}/history")]
        public async Task<IActionResult> GetHistoricalDeviceStates(string workcenter)
        {
            if (_context.Machines == null)
            {
                return NotFound();
            }
            var machine = _context.Machines.Include(m => m.HistoricalDeviceStates).FirstOrDefault(e => e.WorkcenterId.Equals(workcenter));
            
            if (machine == null)
            {
                return NotFound();
            }

            return Ok(machine);
        }

        [HttpGet("{workcenter}/orderbacklog")]
        public async Task<IActionResult> GetOrderBacklog(string workcenter)
        {
            if (_context.Machines == null)
            {
                return NotFound();
            }
            var machine = _context.Machines.Include(m => m.OrderBacklog).FirstOrDefault(e => e.WorkcenterId.Equals(workcenter));

            if (machine == null)
            {
                return NotFound();
            }

            return Ok(machine);
        }


        private bool MachineExists(int id)
        {
            return (_context.Machines?.Any(e => e.MachineId == id)).GetValueOrDefault();
        }
    }
}
