using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiftSimulator.Controllers
{
    public class HomeController : Controller
    {
        [Route("", Name="Home")]
        public ActionResult Home()
        {
            return View();
        }
    }
}