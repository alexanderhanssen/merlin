﻿using System.Web.Mvc;

namespace Merlin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Stasjon()
        {
            return View();
        }

    }
}
