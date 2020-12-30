using System.Threading.Tasks;

namespace CallMeMaybe
{
    public interface IConnector
    {
        Task Send(string userName, string message);
    }
}