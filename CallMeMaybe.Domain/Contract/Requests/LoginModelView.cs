namespace CallMeMaybe.Domain.Contract.Requests
{
    public class LoginModelView
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}