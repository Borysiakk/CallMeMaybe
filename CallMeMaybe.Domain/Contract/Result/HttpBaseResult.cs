using System.Collections.Generic;
using System.Net;

namespace CallMeMaybe.Domain.Contract.Result
{
    public class HttpBaseResult
    {
        public HttpStatusCode Code { get; set; }
        public IEnumerable<string> Errors { get; set; } 
    }
}