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
                                where p.event_id == id && p.winner_flag == null && p.delete_flag == null
                                select p.display_name).ToList(),

                prizes = db.Prizes.OrderBy(a => a.prize_name).Where(a => a.event_id ==  id && a.raffle_flag == false).FirstOrDefault()


            };
            
            return View(ptcpnts);
        }

        public ActionResult LoadEventBanner()
        {
            var id = new Guid(Session["event"].ToString());
            byte[] eventBanner = db.Events.Where(e => e.event_id == id).Select(e => e.event_banner).First();
            return File(eventBanner, "image/jpeg");
        }

        public ActionResult UpdateFlag(Guid prize_id, string prize_name, string wname)
        {
            var getID = (from p in db.Participants where p.display_name == wname select p.participant_id).First();
            var getDept = (from p in db.Participants where p.display_name == wname select p.department_name).First();
            var id = new Guid(Session["event"].ToString());

            Winners winner = new Winners()
            {
                event_id = id,
                prize_id = prize_id,
                participant_id = getID,
                prize_name = prize_name,
                winner_department = getDept,
                winner_id = Guid.NewGuid(),
                winner_name = wname
            };

            db.Winners.Add(winner);
            db.SaveChanges();

            var winnerCount = (from w in db.Winners where w.event_id == id && w.prize_id == prize_id select w.winner_id).Count();

            if (winnerCount != 0)
            {
                Prizes prize = db.Prizes.First(a => a.prize_id == prize_id);
                prize.prizeout_qty = winnerCount;
                db.SaveChanges();
            }
        


            //Participants participant = db.Participants.First(a => a.participant_id == getID);
            //participant.winner_flag = true;
            //db.SaveChanges();


            return RedirectToAction("LoadParticipants");
        }

    }
}