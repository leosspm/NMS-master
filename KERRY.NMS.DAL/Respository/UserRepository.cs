using Dapper;
using KERRY.NMS.MODEL.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace KERRY.NMS.DAL.Respository
{
    public interface IUserRepository
    {
        Task<UserLogin> UserLogin(string userName, string password);
        Task<IEnumerable<string>> GetUserRoleByUserId(int userId);
    }

    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction) { }

        public async Task<UserLogin> UserLogin(string userName, string password)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@UserName", userName);
            pars.Add("@Password", password);
            return await QuerySingleOrDefaultAsync<UserLogin>(StoreProcedure.NMS_LOGIN, pars);
        }

        public async Task<IEnumerable<string>> GetUserRoleByUserId(int userId)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@UserId", userId);
            return await QueryAsync<string>(StoreProcedure.GET_USER_ROLE_BY_USER_ID, pars);
        }
    }
}
