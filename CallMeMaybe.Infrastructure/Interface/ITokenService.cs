using CallMeMaybe.Domain.Entities;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface ITokenService
    {
        public string Generate(ApplicationUser user);
    }
}