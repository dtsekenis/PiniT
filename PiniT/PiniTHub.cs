using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PiniT
{
    public class PiniTHub : Hub
    {
        public void SendMessage(string to, string message)
        {
            Clients.User(to).gotMessage(Context.User.Identity.Name, message);
        }
    }
}