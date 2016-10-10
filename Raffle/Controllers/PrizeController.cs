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

        public ActionResult Prizes()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var id = new Guid(Session["event"].ToString());

                //retrieve prize details
                var model = new PrizeViewModel
                {
                    prizes = (from b in db.Prizes
                             where b.event_id == id && b.raffle_flag == false && b.delete_flag == null
                             select new PrizeDTO
                             {
                                 prize_name = b.prize_name,
                                 prize_count = b.prize_qty,
                                 prizeout_count = b.prizeout_qty,
                                 prize_id = b.prize_id
                             }).OrderBy(b => b.prize_name)
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
                //insert prize to dbo.Prizes
                Prizes prizes = new Prizes()
                {
                    prize_id = Guid.NewGuid(),
                    event_id = id,
                    prize_name = model.prize_name,
                    raffle_flag = false,
                    prize_qty = model.quantity,
                    prizeout_qty = 0,
                    includeAll_flag = model.include_all,
                    datetime_added = DateTime.Now
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
            // delete prize
            Prizes prizes = db.Prizes.Find(id);
            prizes.delete_flag = true;
            prizes.datetime_deleted = DateTime.Now;
            db.SaveChanges();

            return RedirectToAction("Prizes");
        }    
    }
}