using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PiniT.Models;
using System.Data.Entity;

namespace PiniT.Managers
{
    public class CustomerContext
    {
        public ICollection<PiniTCustomer> GetCustomers()
        {
            ICollection<PiniTCustomer> customers;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                customers = db.Users.OfType<PiniTCustomer>()
                                    .Include("Reservations")    
                                    .ToList();
            }
            return customers;
        }
        public PiniTCustomer GetCustomer(string id)
        {
            PiniTCustomer customer;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                customer = db.Users.OfType<PiniTCustomer>()
                                   .Include("Reservations")
                                   .FirstOrDefault(x => x.Id == id);
            }
            return customer;
        }
        public void UpdateCustomer(PiniTCustomer customer)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Users.Attach(customer);
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public bool DeleteCustomer(string id)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                PiniTCustomer customer = db.Users.OfType<PiniTCustomer>().FirstOrDefault(x => x.Id == id);
                if (customer != null)
                {
                    var customerReservations = db.Reservations.Where(x => x.CustomerId == customer.Id);
                    foreach (Reservation reservation in customerReservations)
                    {
                        db.Reservations.Remove(reservation);
                    }
                    db.Users.Remove(customer);
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