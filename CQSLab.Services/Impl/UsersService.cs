using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using CQSLab.Business;

namespace CQSLab.Services.Impl
{
    public class UsersService : ServiceBase, IUsersService
    {
        public UsersService(ModelContext context)
            : base(context)
        {
        }

        public async Task<bool> IsFilledPersonalData(string userId)
        {
            var user = await Context.Users.SingleOrDefaultAsync(p => p.Id == userId);

            if (user == null) return false;
            if (string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Surname) ||
                string.IsNullOrWhiteSpace(user.Email))
            {
                return false;
            }


            return true;
        }

        public async Task<bool> IsFilledStudiesData(string userId)
        {
            var user = await Context.Users.SingleOrDefaultAsync(p => p.Id == userId);
            if (user == null) return false;

            return user.UserStudies.Any();
        }
    }
}