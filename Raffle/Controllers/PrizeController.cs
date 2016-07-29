﻿using Raffle.Models;
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

        public ActionResult Prizes(Guid? id)
        {
            var model = new PrizeViewModel()
            {
                prizes = db.Prizes.Where(a => a.event_id == id).ToList()
            };

            Session["event"] = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Prizes(PrizeViewModel model, Guid id)
        {
               
                int prizecount = model.quantity;

                    for (int i = 1; i <= prizecount; i++ )
                    {
                        Prizes prizes = new Prizes()
                        {
                            prize_id = Guid.NewGuid(),
                            event_id = id,
                            prize_name = model.prize_name,
                            raffle_flag = false
                        };

                        db.Prizes.Add(prizes);
                        db.SaveChanges();
                    }
               
            ModelState.Clear();
                
            return RedirectToAction("Prizes");
        }
            
    }
}