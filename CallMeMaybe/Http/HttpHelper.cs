using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace CallMeMaybe.Http
{
    public class HttpHelper
    {
        public static async Task<T> Post<T,G>(string url,G model)
        {
            RestClient restClient = new RestClient(Routes.Root);
            
            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(model);
            
            IRestResponse restResponse = await restClient.ExecuteAsync<T>(request);
            return JsonConvert.DeserializeObject<T>(restResponse.Content);
        }
        
        public static async Task<T> GetStringAsync<T>(string url,string name, string value)
        {
            RestClient restClient = new RestClient(Routes.Root);
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Content-type", "application/json");
            request.AddParameter(name, value,ParameterType.UrlSegment);
            IRestResponse restResponse = await restClient.ExecuteGetAsync<T>(request);
            return JsonConvert.DeserializeObject<T>(restResponse.Content);
        }
    }
    
    
}