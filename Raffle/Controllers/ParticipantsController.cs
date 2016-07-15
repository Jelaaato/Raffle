using Raffle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raffle.Controllers
{
    public class ParticipantsController : Controller
    {
        private EventsOrganizerEntities db = new EventsOrganizerEntities();

        // GET: Participants
        public ActionResult LoadParticipants()
        {
            var id = new Guid(Session["event"].ToString());
            List<Participants> participant = new List<Participants>().Where(a => a.event_id == id).Select(b => b.last_name).ToList();

            //participant = (from e in db.Participants
            //                    select new
            //                    {
            //                        e.event_id,
            //                        Name = e.last_name + ", " + e.first_name + " " + e.middle_name
            //                    }).Where(a => a.event_id == id).ToList();

            //var participants = db.Participants.Where(a => a.event_id == id).ToList();
            Session["participants"] = participant.AsEnumerable();
            
            return View();
        }
    }
}