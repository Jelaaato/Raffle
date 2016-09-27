using Raffle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raffle.Controllers
{
    public class PrizeController : Controller
    {
        private EventsOrganizerEntities db = new EventsOrganizerEntities();
        // GET: Prize

        public ActionResult Prizes()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var id = new Guid(Session["event"].ToString());
                var model = new PrizeViewModel
                {
                    prize = (from b in db.Prizes
                             where b.event_id == id && b.raffle_flag == false
                             select new PrizeDTO
                             {
                                 distinct_prize_name = b.prize_name,
                                 count = b.prize_qty,
                                 prizeout_count = b.prizeout_qty,
                                 prize_id = b.prize_id
                             }).OrderBy(b => b.distinct_prize_name)
                };
                return View(model);
            }


        }

        public ActionResult AddPrizes()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult AddPrizes(PrizeViewModel model)
        {
            var id = new Guid(Session["event"].ToString());
            int prizecount = model.quantity;

            if (ModelState.IsValid)
            {
                Prizes prizes = new Prizes()
                {
                    prize_id = Guid.NewGuid(),
                    event_id = id,
                    prize_name = model.prize_name,
                    raffle_flag = false,
                    prize_qty = model.quantity,
                    prizeout_qty = 0,
                    includeAll_flag = model.includeAll
                };

                db.Prizes.Add(prizes);
                db.SaveChanges();

                ModelState.Clear();
                return RedirectToAction("Prizes");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult Delete(Guid? id)
        {
            Prizes prizes = db.Prizes.Find(id);
            db.Prizes.Remove(prizes);
            db.SaveChanges();

            return RedirectToAction("Prizes");
        }
            
    }
}