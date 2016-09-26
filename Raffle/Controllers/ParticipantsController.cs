﻿using Raffle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Raffle.Controllers
{
    public class ParticipantsController : Controller
    {

        private EventsOrganizerEntities db = new EventsOrganizerEntities();

        // GET: Participants
        public ActionResult LoadParticipants(PrizeViewModel model)
        {
            var id = new Guid(Session["event"].ToString());
            var prize_id = new Guid(Session["prizeId"].ToString());
            var includeAll = db.Prizes.Where(p => p.prize_id == prize_id).Select(p => p.includeAll_flag).FirstOrDefault();

            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if ((Session["event"] != null) && (!(bool)(includeAll)))
            {
                ParticipantsViewModel ptcpnts = new ParticipantsViewModel
                {
                    participants = (from p in db.Participants
                                    where p.event_id == id && p.winner_flag == null && p.delete_flag == null
                                    select p.display_name).ToList(),

                    prizes = db.Prizes.OrderBy(a => a.prize_name).Where(a => a.event_id == id && a.prize_id == prize_id && a.raffle_flag == false).FirstOrDefault()
                };

                if (ptcpnts.prizes == null)
                {

                    TempData["showModal"] = "true";
                    return RedirectToAction("Prizes", "Prize");
                }
                else
                {
                    return View(ptcpnts);
                }
            }
            else
            { 
                ParticipantsViewModel ptcpnts = new ParticipantsViewModel
                {
                    participants = (from p in db.Participants
                                    where p.event_id == id && p.winnerAgain_flag == null && p.delete_flag == null
                                    select p.display_name).ToList(),

                    prizes = db.Prizes.OrderBy(a => a.prize_name).Where(a => a.event_id == id && a.prize_id == prize_id && a.raffle_flag == false).FirstOrDefault()
                };

                if (ptcpnts.prizes == null)
                {
                    var parti = db.Participants.Where(p => p.winnerAgain_flag == true).ToList();
                    foreach (var p in parti)
                    {
                        if (p.winner_flag == null)
                        {
                            p.winner_flag = true;
                            p.winnerAgain_flag = null;
                            db.SaveChanges();
                        }
                    }
                    
                    TempData["showModal"] = "true";
                    return RedirectToAction("Prizes", "Prize");
                }
                else
                {
                    return View(ptcpnts);
                }
            }
        }

        public ActionResult LoadEventBanner()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var id = new Guid(Session["event"].ToString());
                byte[] eventBanner = db.Events.Where(e => e.event_id == id).Select(e => e.event_banner).First();
                return File(eventBanner, "image/jpeg");
            }
        }

        public ActionResult UpdateFlag(Guid prize_id, string prize_name, string wname, PrizeViewModel model)
        {
            try
            {
                var id = new Guid(Session["event"].ToString());
                var includeAll = db.Prizes.Where(p => p.prize_id == prize_id).Select(p => p.includeAll_flag).FirstOrDefault();
                var getID = (from p in db.Participants where p.display_name == wname && p.event_id == id select p.participant_id).First();
                var getDept = (from p in db.Participants where p.display_name == wname && p.event_id == id select p.department_name).First();

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

                //change code here
                if (!(bool)includeAll)
                {
                    Participants participant = db.Participants.First(a => a.participant_id == getID);
                    participant.winner_flag = true;
                    db.SaveChanges();
                }
                else
                {
                    Participants participant = db.Participants.First(a => a.participant_id == getID);
                    participant.winnerAgain_flag = true;
                    db.SaveChanges();
                }
                
                var winnerCount = (from w in db.Winners where w.event_id == id && w.prize_id == prize_id select w.winner_id).Count();

                if (winnerCount != 0)
                {
                    Prizes prize = db.Prizes.First(a => a.prize_id == prize_id);
                    prize.prizeout_qty = winnerCount;
                    db.SaveChanges();

                    if (prize.prize_qty == prize.prizeout_qty)
                    {
                        prize = db.Prizes.First(a => a.prize_id == prize_id);
                        prize.raffle_flag = true;
                        db.SaveChanges();
                    }
                }
            }

            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction("LoadParticipants");
        }

        public ActionResult CapturePrizeId(Guid? prize_id)
        {
            Session["prizeId"] = prize_id;
            return RedirectToAction("LoadParticipants");
        }

        public ActionResult DisplayWinner()
        {
            var id = new Guid(Session["event"].ToString());
            var winners = db.Winners.Where(w => w.event_id == id).OrderBy(w => w.winner_name).ToList();

            return PartialView("_DisplayWinner", winners);
        }
    }

}