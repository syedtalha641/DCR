using DAL.EntityModels;

namespace Repository.IRepos
{
    public interface IAccountRepos
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string UserLoginId);
        Task<User> AddUser(string UserLoginId, string UserName, string UserEmail, string UserPassword, string ConfirmPassword);
        Task<User> LoginUser(string UserLoginId, string UserPassword);
        Task<User> UpdateUserPassword(string UserLoginId, string UserPassword);
        Task<string> GetUserEmail(string UserLoginId);
        Task<string> GetUserPhoneNumber(string UserLoginId);
        Task<User> DeleteUser(string UserLoginId);
    }
}
