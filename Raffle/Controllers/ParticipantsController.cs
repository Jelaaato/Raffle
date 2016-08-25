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

                prizes = db.Prizes.OrderBy(a => a.prize_name).Where(a => a.event_id ==  id && a.raffle_flag == false).FirstOrDefault()

            };
            
            return View(ptcpnts);
        }

        public ActionResult UpdateFlag(Guid prize_id, string prize_name, string wname)
        {
            Prizes prize = db.Prizes.First(a => a.prize_id == prize_id);
            prize.raffle_flag = true;
            db.SaveChanges();


            var getID = (from p in db.Participants where p.display_name == "Venzon, Nino" select p.participant_id).First();

            Participants participant = db.Participants.First(a => a.participant_id == getID);
            participant.winner_flag = true;
            db.SaveChanges();


            Winners winner = new Winners()
            {
                event_id = new Guid(Session["event"].ToString()),
                prize_id = prize_id,
                participant_id = getID,
                prize_name = prize_name,
                winner_department = "Medical Informatics",
                winner_id = Guid.NewGuid(),
                winner_name = "Venzon, Nino"
            };

            db.Winners.Add(winner);
            db.SaveChanges();

            return RedirectToAction("LoadParticipants");
        }

        public ActionResult GetWinner(string getwinner)
        {
            return View();
        }
    }
}