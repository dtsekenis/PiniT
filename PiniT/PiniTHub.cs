using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PiniT.Models;

namespace PiniT
{
    public class PiniTHub : Hub
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void SendToUser(string to, string message)
        {
            var user = db.Users.Find(to);
            if (user == null)
            {
                user = db.Users.FirstOrDefault(x => x.UserName == to);
            }
            Clients.User(user.UserName).gotMessage(Context.User.Identity.Name, message);
        }

    }
}