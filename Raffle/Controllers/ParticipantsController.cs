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

            ParticipantsViewModel ptcpnts = new ParticipantsViewModel
            {
                participants = (from p in db.Participants
                                where p.event_id == id && p.winner_flag == false
                                select p.display_name).ToList(),

                prizes = db.Prizes.Where(a => a.event_id ==  id && a.raffle_flag == false).FirstOrDefault()
            };
            
            return View(ptcpnts);
        }

        public ActionResult DisplayPrize()
        {
            var id = new Guid(Session["event"].ToString());

            return View();
        }
    }
}