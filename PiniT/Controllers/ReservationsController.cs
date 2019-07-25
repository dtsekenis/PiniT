using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PiniT.Managers;
using PiniT.Models;

namespace PiniT.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private TableManager tableDb = new TableManager();
        private ReservationManager db = new ReservationManager();
        private RestaurantManager restDb = new RestaurantManager();
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            ICollection<Reservation> reservations;

            if (User.IsInRole("Manager"))
            {
                reservations = db.GetRestaurantReservations(userId);
                return View(reservations);
            }
            if (User.IsInRole("Admin"))
            {
                reservations = db.GetReservationsFull();
                return View(reservations);
            }

            reservations = db.GetCustomerReservationsFull(userId);
            return View(reservations);
        }

        public ActionResult Create(int id)
        {
            TempData["TableId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            //check User account
            //check if table still available

            
            reservation.TableId = (int)TempData["TableId"];
            tableDb.ToggleIsBooked(reservation.TableId);
            reservation.CustomerId = User.Identity.GetUserId();
            db.CreateReservation(reservation);

            //send message to hub
            //var hub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            //hub.Clients.User(ManagerName).broadcast($"{userName} visited About!");

            return RedirectToAction("Index","Home");
        }
    }
}