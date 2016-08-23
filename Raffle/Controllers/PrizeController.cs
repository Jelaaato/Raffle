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

        public ActionResult Prize()
        {
            var id = new Guid(Session["event"].ToString());
            var model = new PrizeViewModel
            {
                prize = (from b in db.Prizes
                        where b.event_id == id
                        group b by b.prize_name
                            into grp
                            select new PrizeDTO
                                {
                                    distinct_prize_name = grp.Key,
                                    count = grp.Select(a => a.prize_name).Count()
                                })          
            };


            //Session["event"] = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Prize(PrizeViewModel model)
        {
            var id = new Guid(Session["event"].ToString());
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

        [HttpPost]
        public ActionResult Delete(Guid? id)
        {
            Prizes prizes = db.Prizes.Find(id);
            db.Prizes.Remove(prizes);
            db.SaveChanges();

            return RedirectToAction("Prize");
        }
            
    }
}