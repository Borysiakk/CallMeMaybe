using CallMeMaybe.Domain.Contract.Results;
using Newtonsoft.Json;
using RestSharp;

namespace CallMeMaybe.RestApi
{
    public class HttpHelper
    {
        private static string _connection = "https://localhost:5001/";
        public static T Post<T,G>(string url,G model)
        {
            RestClient restClient = new RestClient(_connection);
            
            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(model);
            
            IRestResponse restResponse = restClient.Execute(request);
            return JsonConvert.DeserializeObject<T>(restResponse.Content);
        }

        // public static T Get<T, G>(string url, G model)
        // {
        //     RestClient restClient = new RestClient(_connection);
        //     var request = new RestRequest(url, Method.GET);
        //     request.AddHeader("Content-type", "application/json");
        //     request.AddParameter("userId", "7af252a7-3e2f-45c8-a9f7-6c52c0b74ea5");
        //     IRestResponse restResponse = restClient.Execute(request);
        //     
        //     return JsonConvert.DeserializeObject<T>(restResponse.Content);
        // }
        
        public static T GetString<T, G>(string url, G model)
        {
            RestClient restClient = new RestClient(_connection);
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Content-type", "application/json");
            request.AddParameter("userId", "7af252a7-3e2f-45c8-a9f7-6c52c0b74ea5");
            IRestResponse restResponse = restClient.Execute(request);
            
            return JsonConvert.DeserializeObject<T>(restResponse.Content);
        }
    }
}