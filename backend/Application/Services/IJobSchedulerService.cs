using System.Threading.Tasks;

namespace Application.Services
{
    public interface IJobSchedulerService
    {
        Task SyncData();
    }
}
