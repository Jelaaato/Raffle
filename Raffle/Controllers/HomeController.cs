using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raffle.Models;
using Newtonsoft.Json;

namespace Raffle.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private EventsOrganizerEntities db = new EventsOrganizerEntities();
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }
    }
}