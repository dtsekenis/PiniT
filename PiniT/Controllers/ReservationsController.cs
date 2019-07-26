using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PiniT.Managers;
using PiniT.Models;

namespace PiniT.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ReservationsController : Controller
    {
        private ManagerContext manDb = new ManagerContext();
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
            ViewBag.Restaurants = restDb.GetRestaurants();
            return View(reservations);
        }

        public ActionResult Create(int id)
        {
            Table table = tableDb.GetTable(id);
            TempData["TableId"] = id;
            ViewBag.Restaurant = restDb.GetRestaurant(table.RestaurantId);
            ViewBag.Table = table;
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
            Table table = tableDb.GetTable((int)TempData["TableId"]);
            if (table.IsBooked)
            {
                //Send message that this table was booked during reservation by another user
                //Need to fix the path
                return RedirectToAction("CustomerIndex","Tables", new { id = table.RestaurantId });
            }


            reservation.TableId = table.TableId;
            tableDb.ToggleIsBooked(reservation.TableId);
            reservation.CustomerId = User.Identity.GetUserId();
            db.CreateReservation(reservation);

            //send message to hub
            var hub = GlobalHost.ConnectionManager.GetHubContext<PiniTHub>();

            var manager = manDb.GetManager(table.RestaurantId);
            
            hub.Clients.User(manager.UserName).getReservation(new { Customer = User.Identity.Name,
                                                                    Comment = reservation.Comment,
                                                                    Date = reservation.BookDate.ToString("dd/MM/yyyy HH:mm"),
                                                                    Table = table.Name});

            return RedirectToAction("Index","Home");
        }
    }
}