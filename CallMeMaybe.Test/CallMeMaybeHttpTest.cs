using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using NUnit.Framework;

namespace CallMeMaybe.Test
{
    public class Tests
    {
        [Test]
        public async Task LoginAsyncIsLoginCodeOk()
        {
            LoginModelView loginModelView = new LoginModelView()
            {
                Email = "user@example.com",
                Password = "string",
            };

            var result = await HttpRestClient.LoginAsync(loginModelView);
            
            Assert.AreEqual(result.Code,HttpStatusCode.OK);
        }

        [Test]
        public async Task GetFriendsWithStatusAsyncGetFriendToDictionary()
        {
            string userId = "bfebbc35-c07c-497d-83ff-27eb7eda94da";
            
            var result = await HttpRestClient.GetFriendsWithStatus(userId);
            
            Assert.That(result,Contains.Key("8aa230d5-a2b5-4276-b750-2768b1dc46e0"));
        }
    }
}