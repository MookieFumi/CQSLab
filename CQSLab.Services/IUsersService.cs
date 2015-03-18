using System.Threading.Tasks;

namespace CQSLab.Services
{
    public interface IUsersService
    {
        Task<bool> IsFilledPersonalData(string userId);
        Task<bool> IsFilledStudiesData(string userId);
    }
}