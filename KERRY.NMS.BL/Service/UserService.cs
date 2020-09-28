using KERRY.NMS.DAL;
using KERRY.NMS.MODEL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KERRY.NMS.BL
{
    public interface IUserService
    {
        Task<UserLogin> UserLogin(string userName, string password);
        Task<IEnumerable<string>> GetUserRoleByUserId(int userId);
    }

    internal class UserService : IUserService
    {
        private readonly IDalService dalService = null;

        public UserService(IDalService dalService)
        {
            this.dalService = dalService;
        }

        public async Task<UserLogin> UserLogin(string userName, string password)
        {
            using (IDBContext context = dalService.NewConnection())
            {
                try
                {
                    return await context.UnitOfWork.UserRepository.UserLogin(userName, password);
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<string>> GetUserRoleByUserId(int userId)
        {
#if !true
            var roles = new List<string>();
            roles.Add("admin");

            return await Task.FromResult<IEnumerable<string>>(roles);
#else
            using (IDBContext context = dalService.NewConnection())
            {
                return await context.UnitOfWork.UserRepository.GetUserRoleByUserId(userId);
            }
#endif
        }

    }
}
