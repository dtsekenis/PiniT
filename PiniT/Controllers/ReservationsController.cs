using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PiniT.Managers;
using PiniT.Models;
using PiniT.ViewModels;

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
                Restaurant restaurant = restDb.GetRestaurantFull(User.Identity.GetUserId());
                if (restaurant == null)
                {
                    return RedirectToAction("Create", "Restaurants");
                }
                ViewBag.Restaurants = restDb.GetRestaurants();

                reservations = db.GetRestaurantReservations(userId);
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
            CreateReservationsVM vm = new CreateReservationsVM
            {
                Table = table,
                Restaurant = restDb.GetRestaurant(table.RestaurantId),

            };
            return View(vm);
        }

        [System.Web.Mvc.Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateReservationsVM vm)
        {
            Table table = tableDb.GetTable((int)TempData["TableId"]);
            PiniTCustomer customer = custDb.GetCustomer(User.Identity.GetUserId());
            var manager = manDb.GetManager(table.RestaurantId);
            var hub = GlobalHost.ConnectionManager.GetHubContext<PiniTHub>();

            if (!ModelState.IsValid || !vm.HasAcceptedTerms)
            {
                TempData["TableId"] = table.TableId;
                vm.Table = table;
                vm.Restaurant = restDb.GetRestaurant(table.RestaurantId);
                return View(vm);
            }

            if (customer.AccountWallet.Credits < vm.Reservation.BookingFee)
            {
                
                TempData["Message"] = "Not enough Credits. Reservation Cancelled";
                return RedirectToAction("CustomerIndex", "Tables", new { id = table.RestaurantId });
            }

            if (table.IsBooked)
            {
                TempData["Message"] = "Sorry! Someone else Booked that Table! You can Book another one :)";
                return RedirectToAction("CustomerIndex","Tables", new { id = table.RestaurantId });
            }

            vm.Reservation.TableId = table.TableId;
            tableDb.ToggleIsBooked(vm.Reservation.TableId);
            vm.Reservation.CustomerId = User.Identity.GetUserId();
            if (vm.Reservation.Comment == null || vm.Reservation.Comment =="")
            {
                vm.Reservation.Comment = "No Comment";
            }
            db.CreateReservation(vm.Reservation);
            hub.Clients.User(manager.UserName).getReservation(new
            {
                Customer = User.Identity.Name,
                Comment = vm.Reservation.Comment,
                Date = vm.Reservation.BookDate.ToString("dd/MM/yyyy HH:mm"),
                Table = table.Name,
                EstimatedTime = vm.Reservation.BookDate.AddMinutes(vm.EstimatedTime).ToString("HH:mm")
            });
            bool result = walletDb.PayFee(vm.Reservation.BookingFee, vm.Reservation.CustomerId, table.RestaurantId);

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