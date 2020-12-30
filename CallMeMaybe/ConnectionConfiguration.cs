using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallMeMaybe
{
    public class ConnectionConfiguration
    {
        public HubConnection HubConnection { get; set; }
    }
}
