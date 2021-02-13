using System;
using System.Threading.Tasks;
using CallMeMaybe.Args;
using CallMeMaybe.Builder;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Http;
using Microsoft.AspNetCore.SignalR.Client;

namespace CallMeMaybe
{
    class Program
    {
        static void ReceiveMessage(object sender,MessageDelegateArgs args)
        {
            Console.WriteLine(args.UserName + ": " + args.Message);
        }
        
        static async Task Main(string[] args)
        {
            LoginModelView loginModelView = new LoginModelView()
            {
                Email = args[0],
                Password = "string"
            };

            var authorizationResult = await HttpRestClient.LoginAsync(loginModelView);
            Connection connection = await ConnectionBuilder.Create(authorizationResult);
            
            connection.IncomingCall += async (sender, delegateArgs) =>
            {
                Console.WriteLine("Połączenie przychodzące od {0}",delegateArgs.User);
                Console.WriteLine("Czy zgadzasz sie na połączenie T/N");
                var answer = Console.ReadLine();
                if (answer == "T") await connection.CallAcceptedIncoming(delegateArgs.User);
                else if (answer == "N") await connection.CallDeclinedIncoming(delegateArgs.User);
            };
            
            foreach (var friend in connection.Friends)
            {
                Console.WriteLine(friend.Key + ": " + friend.Value);
            }

            if (args[0] == "szymaborys@gmail.com")
            {
                await connection.Call("borys59@onet.eu");
            }
            
            
            Console.ReadKey();
        }
    }
}