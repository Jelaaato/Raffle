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
            var events = db.Events.Where(e => e.delete_flag != true && e.hasRaffle == 1).OrderBy(e => e.event_name).ToList();

            if (events.Count() != 0)
            {
                return View(events);
            }
            else
            {
                ViewBag.Message = "There are no events available.";
                return View(events);
            }
        }

        public ActionResult ValidatePasscode(string name)
        {
            Session["eventName"] = name;

            if (Session["eventName"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var participantCount = db.Events.Where(e => e.event_name == name).Select(e => e.participant_count).First();
                if (participantCount == null)
                {
                    TempData["showModal"] = "true";
                    return RedirectToAction("Index");
                }
            } 
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

        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}