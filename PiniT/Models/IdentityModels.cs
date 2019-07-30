using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PiniT.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class PiniTManager : ApplicationUser
    {
        [DisplayName("Authorized")]
        public bool RestaurantAuthorized { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
    public class PiniTCustomer : ApplicationUser
    {
        public PiniTCustomer()
        {
            Reservations = new HashSet<Reservation>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }


    public class ApplicationUser : IdentityUser
    {

        public virtual AccountWallet AccountWallet { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<RestaurantType> RestaurantTypes { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<AccountWallet> AccountWallets { get; set; }
        public virtual DbSet<Image> Images { get; set; }
    }
}