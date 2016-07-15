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

        //    public ActionResult LoadParticipants(Guid id)
        //    {
        //        var participants = (from e in db.Participants
        //                            select new
        //                            {
        //                                e.event_id,
        //                                Name = e.last_name + ", " + e.first_name + " " + e.middle_name
        //                            }).Where(a => a.event_id == id).FirstOrDefault();

        //        //var participants = db.Participants.Where(a => a.event_id == id).ToList();
        //        Session["serializedparticipants"] = participants.Name;
        //        return View();
        //    }

        //    public ActionResult LoadPrizes()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    public ActionResult LoadPrizes(PrizeViewModel model)
        //    {
        //        Prizes prizes = new Prizes()
        //        {
        //            prize_id = Guid.NewGuid(),
        //            event_id = Guid.NewGuid(),
        //            prize_name = model.PrizeName,
        //            raffle_flag = false
        //        };

        //        db.Prizes.Add(prizes);
        //        db.SaveChanges();
        //        return View();
        //    }
        //}
    }
}