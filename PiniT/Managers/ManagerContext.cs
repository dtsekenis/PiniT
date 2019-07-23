using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiniT.Managers
{
    public class ManagerContext
    {
        //Test USERS?? Controller for Admin?? Ask Vyron
        public PiniTManager GetManager(string id)
        {
            PiniTManager manager;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                                                
                manager = (PiniTManager)db.Users.Where(x => x.Id == id)
                                                .OfType<PiniTManager>()
                                                .FirstOrDefault();
            }
            return manager;
        }
    }
}