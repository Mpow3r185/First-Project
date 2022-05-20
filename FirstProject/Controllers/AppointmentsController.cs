using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;
using Microsoft.AspNetCore.Http;
using MimeKit;
using MailKit.Net.Smtp;

namespace FirstProject.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ModelContext _context;

        public AppointmentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Appointments.Include(a => a.Emp).Include(a => a.Patient);

            return View(await modelContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Emp)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Appointmentid == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["Empid"] = new SelectList(_context.Employees, "Empid", "Empid");
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Appointmentid,Healthcase,Status,Appointmenttime,Empid,Patientid")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Empid"] = new SelectList(_context.Employees, "Empid", "Empid", appointment.Empid);
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", appointment.Patientid);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            Bill bill = new Bill();
            bill.Cost = 20;
            bill.Billtime = appointment.Appointmenttime;
            bill.Status = "Not paid";
            bill.Patientid = ViewBag.PatientID;
            bill.Appointmentid = appointment.Appointmentid;
            _context.Add(bill);
            await _context.SaveChangesAsync();


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("FirstProject","mahmoud7hamarsheh@gmail.com"));
            message.To.Add(new MailboxAddress("Test", "hamarsheh9111@gmail.com"));
            message.Subject = "Appointment";
            message.Body = new TextPart("Palin")
            {
                Text = "Approve"
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com",587,false);
                //client.Authenticate("mahmoud7hamarsheh@gmail.com", "Ki@cerato!185");
               // client.Send(message);
                client.Disconnect(true);
               
            }



                ViewData["Empid"] = new SelectList(_context.Employees, "Empid", "Empid", appointment.Empid);
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", appointment.Patientid);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Appointmentid,Healthcase,Status,Appointmenttime,Empid,Patientid")] Appointment appointment)
        {
            if (id != appointment.Appointmentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Appointmentid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("AppointmentPending", "Home");
            }
            ViewData["Empid"] = new SelectList(_context.Employees, "Empid", "Empid", appointment.Empid);
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", appointment.Patientid);
            return View(appointment);
        }


        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Emp)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Appointmentid == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("AppointmentPending", "Home");
        }

        private bool AppointmentExists(decimal id)
        {
            return _context.Appointments.Any(e => e.Appointmentid == id);
        }
    }
}
