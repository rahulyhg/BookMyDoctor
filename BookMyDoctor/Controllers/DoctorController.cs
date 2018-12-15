using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMyDoctor.Models;
using BookMyDoctor.Repository;

namespace BookMyDoctor.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Doctor_Registration() {

            return View();
        }

        [HttpPost]
        public ActionResult Doctor_Registration(DoctorModel dmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DoctorDBHandle sdb = new DoctorDBHandle();
                    if (sdb.AddDoctor(dmodel))
                    {
                        ViewBag.Message = "Doctor Registration Completed Successfully";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

    }
}