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
        public ActionResult AddPrizes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPrizes(PrizeViewModel model, Guid id)
        {
            try
            {
                int prizecount = model.Quantity;
                for (int i = 1; i <= prizecount; i++ )
                {
                    Prizes prizes = new Prizes()
                    {
                        prize_id = Guid.NewGuid(),
                        event_id = id,
                        prize_name = model.PrizeName,
                        raffle_flag = false
                    };

                    db.Prizes.Add(prizes);
                    db.SaveChanges();
                }
                ModelState.Clear();
                Session["event"] = id;
                return View();
            }
            catch (DbEntityValidationException dbex)
            {
                Exception ex = dbex;
                foreach (var error in dbex.EntityValidationErrors)
                {
                    foreach (var err in error.ValidationErrors)
                    {
                        string msg = string.Format("{0}:{1}", error.Entry.Entity.ToString(), err.ErrorMessage);
                        ex = new InvalidOperationException(msg, ex);
                    }
                }
                throw ex;
            }
            }
    }
}