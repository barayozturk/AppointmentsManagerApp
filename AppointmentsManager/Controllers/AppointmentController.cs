using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentsManager.Data;
using AppointmentsManager.Data.Models;

namespace AppointmentsManager.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointment - default
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
          if (_context.Appointments == null)
          {
              return NotFound("No data found");
          }
            return await _context.Appointments.Where(e=> !e.Deleted && !e.Done).ToListAsync();
        }

        // GET: api/Appointment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
          if (_context.Appointments == null)
          {
              return NotFound("No data found");
          }
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound("No data found");
            }

            return appointment;
        }

        // PUT: api/Appointment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("You are trying to modify the wrong appointment.");
            }

           // _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                Appointment entry_ = await _context.Appointments.FindAsync(appointment.Id);

                if (entry_.Title != appointment.Title)
                {
                    entry_.Title = appointment.Title;
                }

                if (entry_.Description != appointment.Description)
                {
                    entry_.Description = appointment.Description;
                }

                if (entry_.Address != appointment.Address)
                {
                    entry_.Address = appointment.Address;
                }

                if (entry_.LevelOfImportance != appointment.LevelOfImportance)
                {
                    entry_.LevelOfImportance = appointment.LevelOfImportance;
                }

                if (entry_.Done != appointment.Done)
                {
                    entry_.Done = appointment.Done;
                }

                if (entry_.Deleted != appointment.Deleted)
                {
                    entry_.Deleted = appointment.Deleted;
                }

                if (entry_.Date != appointment.Date)
                {
                    entry_.Date = appointment.Date;
                }

                if (entry_.Time != appointment.Time)
                {
                    entry_.Time = appointment.Time;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound("The appointment with the Id" + " " + id + " does not exist");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Appointment uptaded successfully");
        }

        // POST: api/Appointment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
          if (_context.Appointments == null)
          {
              return Problem("Entity set 'Appointments'  is null.");
          }

            try
            {
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest("Could not create the new Appointment: " + e.Message);
            }

            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
        }

        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (_context.Appointments == null)
            {
                return NotFound("No data found");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound("No appointment with the ID " + id);
            }

            Appointment entry_ = await _context.Appointments.FindAsync(appointment.Id);
            entry_.ModifiedDate = DateTime.Now;
            entry_.Deleted = true;
            await _context.SaveChangesAsync();

            return Ok("Appointment deleted successfully");
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
