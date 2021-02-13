using System;

namespace CallMeMaybe.Domain.Contract.Result
{
    public class HttpAuthorizationResult :HttpBaseResult
    {
        public string Id { get; set; }
        public string User { get; set; }
        public string Token { get; set; }
        public DateTime DateIssue { get; set; }
    }
}