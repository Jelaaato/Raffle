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
            var events = db.Events.Where(e => e.delete_flag == null).OrderBy(e => e.event_name).ToList();
            if (events != null)
            {
                return View(events);
            }
            else
            {
                ViewBag.Message = "There are no events available";
                return View();
            }
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
                    return RedirectToAction("Prize", "Prize");
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