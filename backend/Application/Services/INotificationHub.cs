using System.Threading.Tasks;

namespace Application.Services
{
    public interface INotificationHub
    {
        Task SendData(string ticker, string source);
    }
}
