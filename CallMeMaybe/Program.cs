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
            try
            {
                LoginModelView loginModelView = new LoginModelView()
                {
                    Email = args[0],
                    Password = "string"
                };

                var authorizationResult = await HttpRestClient.LoginAsync(loginModelView);
                Connection connection = await ConnectionBuilder.Create(authorizationResult);
                await connection.Session.Initialization(connection.User);
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

                if (args[0] == "userxxx@gmai.com")
                {
                    await connection.Call("user1xxx@gmai.com");
                }
            
            
                Console.ReadKey();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}