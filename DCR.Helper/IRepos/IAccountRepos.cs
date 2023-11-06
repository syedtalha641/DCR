

namespace DCR.ViewModel.IRepos
{
    public interface IAccountRepos
    {
        Task<object> GetUsers();
        Task<object> GetUser(string UserLoginId);
        Task<object> AddUser(string UserLoginId, string UserName, string UserEmail, string UserPassword, string ConfirmPassword);
        Task<object> LoginUser(string UserLoginId, string UserPassword);
        Task<object> UpdateUserPassword(string UserLoginId, string UserPassword);
        Task<string> GetUserEmail(string UserLoginId);
        Task<string> GetUserPhoneNumber(string UserLoginId);
        Task<object> DeleteUser(string UserLoginId);
    }
}
