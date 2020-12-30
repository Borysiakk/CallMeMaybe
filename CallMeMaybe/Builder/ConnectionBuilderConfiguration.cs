using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallMeMaybe.Builder
{
    public class ConnectionBuilderConfiguration
    {
        public AuthorizationManager AuthorizationManager { get; set; }

        public Connection.SendMessageDelegate SendMessageDelegate;
        public Connection.ReceiveMessageDelegate ReceiveMessageDelegate;
        public Connection.UpdateFriendsStatusDelegate UpdateFriendsStatusDelegate;
    }
}
