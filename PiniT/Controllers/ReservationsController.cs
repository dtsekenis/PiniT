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
        private AccountWalletManager walletDb = new AccountWalletManager();
        private CustomerContext custDb = new CustomerContext();
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
                ViewBag.Restaurants = restDb.GetRestaurants();

                reservations = db.GetRestaurantReservations(userId);
                return View(reservations);
            }
            if (User.IsInRole("Admin"))
            {
                ViewBag.Restaurants = restDb.GetRestaurants();

                reservations = db.GetReservationsFull();
                return View(reservations);
            }

            reservations = db.GetCustomerReservationsFull(userId);
            ViewBag.Restaurants = restDb.GetRestaurants();
            return View(reservations);
        }

        [System.Web.Mvc.Authorize(Roles = "Customer")]
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
            Table table = tableDb.GetTable((int)TempData["TableId"]);
            PiniTCustomer customer = custDb.GetCustomer(User.Identity.GetUserId());
            var manager = manDb.GetManager(table.RestaurantId);
            var hub = GlobalHost.ConnectionManager.GetHubContext<PiniTHub>();

            if (!ModelState.IsValid)
            {
                return View(reservation);
            }

            if (customer.AccountWallet.Credits < reservation.BookingFee)
            {
                
                TempData["Message"] = "Not enough Credits. Reservation Cancelled";
                return RedirectToAction("CustomerIndex", "Tables", new { id = table.RestaurantId });
            }

            if (table.IsBooked)
            {
                TempData["Message"] = "Sorry! Someone else Booked that Table! You can Book another one :)";
                return RedirectToAction("CustomerIndex","Tables", new { id = table.RestaurantId });
            }

            reservation.TableId = table.TableId;
            tableDb.ToggleIsBooked(reservation.TableId);
            reservation.CustomerId = User.Identity.GetUserId();
            db.CreateReservation(reservation);
            hub.Clients.User(manager.UserName).getReservation(new { Customer = User.Identity.Name,
                                                                    Comment = reservation.Comment,
                                                                    Date = reservation.BookDate.ToString("dd/MM/yyyy HH:mm"),
                                                                    Table = table.Name});
            bool result = walletDb.PayFee(reservation.BookingFee, reservation.CustomerId, table.RestaurantId);

            if (result)
            {
                TempData["Message"] = "Reservation Accepted! :)";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Message"] = "Sorry Something went Wrong ! :'(";
                return RedirectToAction("CustomerIndex", "Tables", new { id = table.RestaurantId });
            }
        }
    }
}