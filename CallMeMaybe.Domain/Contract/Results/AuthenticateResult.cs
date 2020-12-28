using System.Collections.Generic;

namespace CallMeMaybe.Domain.Contract.Results
{
    public class AuthenticateResult
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}