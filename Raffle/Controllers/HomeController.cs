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
           return View (db.Events.OrderBy(e => e.event_name).ToList());
        }

        public ActionResult ValidatePasscode(string name)
        {
            Session["eventName"] = name;
            return View();
        }

        [HttpPost]
        public ActionResult ValidatePasscode(PasscodeModel model, Guid id)
        {
            Session["event"] = id;

            if (ModelState.IsValid)
            {
                int code = (from e in db.Events where e.event_id == id select e.passcode).FirstOrDefault();

                if (model.passcode == code)
                {
                    return RedirectToAction("Prizes", "Prize");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid passcode");
                }
            }
            return View();
        }
    }
}