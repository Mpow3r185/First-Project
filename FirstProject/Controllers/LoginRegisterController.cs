using FirstProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public LoginRegisterController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Register()
        {

            return View();
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Patientid,Fname,Lname,Age,Gender,Phonenumber,Email,Imgpath,ImgFile")] Patient patient, string email, string pass)
        {
            if (ModelState.IsValid)
            {
                if (patient.ImgFile != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + patient.ImgFile.FileName;
                    string path = Path.Combine(w3path + "/images/", img);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await patient.ImgFile.CopyToAsync(fileStream);
                    }
                    patient.Imgpath = img;

                    _context.Add(patient);
                    await _context.SaveChangesAsync();
                }
                Login userLogin = new Login();

                userLogin.Email = email;
                userLogin.Passwordd = pass;
                userLogin.Roleid = 24;
      

                _context.Add(userLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "LoginRegister");

            }
            return View(patient);
        }
        public IActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email", "Passwordd")] Login userLogin)
        {

            var auth = _context.Logins.Where(x => x.Email == userLogin.Email && x.Passwordd == userLogin.Passwordd).SingleOrDefault();
            var authe = _context.Employees.Where(x => x.Email == userLogin.Email).SingleOrDefault();
            var authp = _context.Patients.Where(x => x.Email == userLogin.Email).SingleOrDefault();

            if (auth != null)
            {

                switch (auth.Roleid)
                {
                    case 23:
                        //HttpContext.Session.SetString("PatientEmail", authe.Email);
                        HttpContext.Session.SetInt32("EmployeeID", (int)authe.Empid);
                        HttpContext.Session.SetString("Name", "mh");
                        return RedirectToAction("Doctor", "Home");
                    case 22:
                        //HttpContext.Session.SetString("Name", "mh");
                        HttpContext.Session.SetInt32("EmployeeID", (int)authe.Empid);
                        return RedirectToAction("Admin", "Home");
                    case 24:
                        HttpContext.Session.SetInt32("PatientID", (int)authp.Patientid);
                        HttpContext.Session.SetString("Name", "mh");
                        return RedirectToAction("Index", "PatientHome");

                }

            }
            //else if(authp != null)
            //{
            //   // HttpContext.Session.SetString("PatientEmail", authe.Email);
               
            //}

                return View();
        }

    }
}
       
       