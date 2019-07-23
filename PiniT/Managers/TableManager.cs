using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PiniT.Models;

namespace PiniT.Managers
{
    public class TableManager
    {
        public ICollection<Table> GetAllTables()
        {
            ICollection<Table> tables;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                tables = db.Tables.Include("Restaurant").ToList();
            }

            return tables;
        }
        public ICollection<Table> GetTables(string restaurantId)
        {
            ICollection<Table> tables;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                tables = db.Tables.Where(x=>x.RestaurantId == restaurantId).Include("Restaurant").ToList();
            }

            return tables;
        }
        public Table GetTable(int id)
        {
            Table table;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                table = db.Tables.Include("Restaurant").FirstOrDefault(x => x.TableId == id);
            }
            return table;
        }
        public bool CreateTable(Table table)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                
                if (db.Tables.Find(table.TableId) == null)
                {
                    db.Tables.Add(table);
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
        public void UpdateTable(Table table)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Tables.Attach(table);
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();

            }
        }
        public bool DeleteTable(int id)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Table table = db.Tables.Find(id);
                if(table != null)
                {
                    db.Tables.Remove(table);
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

        public bool ToggleIsBooked(int id)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Table table = db.Tables.Find(id);
                if (table != null && table.IsBooked==false)
                {
                    table.IsBooked = true;
                    db.SaveChanges();
                    result = true;
                }
                else if (table != null && table.IsBooked == true)
                {
                    table.IsBooked = false;
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