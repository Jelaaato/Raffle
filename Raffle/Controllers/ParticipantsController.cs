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

            var ptcpnts = new ParticipantsViewModel
            {
                participants = (from p in db.Participants
                                where p.event_id == id
                                select p.display_name).ToList()
            };
            
            return View(ptcpnts);
        }
    }
}