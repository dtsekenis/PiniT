using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PiniT
{
    public class PiniTHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}