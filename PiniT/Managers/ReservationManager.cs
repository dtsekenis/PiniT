using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PiniT.Managers
{
    public class ReservationManager
    {
        public ICollection<Reservation> GetReservations()
        {
            ICollection<Reservation> reservations;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservations = db.Reservations.ToList();
            }
            return reservations;
        }
        public ICollection<Reservation> GetReservationsFull()
        {
            ICollection<Reservation> reservations;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservations = db.Reservations.Include("Customer")
                                              .Include("Table")
                                              .ToList();
            }
            return reservations;
        }
        public ICollection<Reservation> GetRestaurantReservations(string restId)
        {
            ICollection<Reservation> reservations;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //Restaurant restaurant = db.Restaurants.Find(restId);
                reservations = db.Reservations.Where(x=>x.Table.RestaurantId == restId).Include("Customer")
                                              .Include("Table")
                                              .ToList();
                //reservations
            }
            return reservations;
        }

        public ICollection<Reservation> GetCustomerReservationsFull(string customerId)
        {
            ICollection<Reservation> reservations;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservations = db.Reservations.Where(x=>x.CustomerId == customerId)
                                              .Include("Customer")
                                              .Include("Table")
                                              .ToList();
            }
            return reservations;
        }
        public Reservation GetReservation(int id)
        {
            Reservation reservation;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservation = db.Reservations.Find(id);
            }
            return reservation;
        }
        public Reservation GetReservationFull(int id)
        {
            Reservation reservation;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservation = db.Reservations.Include("Customer")
                                .Include("Table")
                                .FirstOrDefault(x=>x.ReservationId == id);
            }
            return reservation;
        }
        public bool CreateReservation(Reservation reservation)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (db.Reservations.Find(reservation.ReservationId) == null)
                {
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        public void UpdateReservation(Reservation reservation)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Reservations.Attach(reservation);
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public bool DeleteReservation(int id)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Reservation reservation = db.Reservations.Find(id);
                if(reservation != null)
                {
                    db.Reservations.Remove(reservation);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
    }
}