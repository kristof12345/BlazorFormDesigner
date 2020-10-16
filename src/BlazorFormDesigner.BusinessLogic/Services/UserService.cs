using BlazorFormDesigner.BusinessLogic.Interfaces;
using System.Threading.Tasks;

namespace BlazorFormDesigner.BusinessLogic.Services
{
    public class UserService
    {
        private readonly IUserRepository UserRepository;

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<Models.User> Create(Models.User user, string password)
        {
            return await UserRepository.Create(user, password);
        }

        public Task<Models.User> Delete(string username)
        {
            return UserRepository.Delete(username);
        }

        public async Task<Models.User> Update(Models.User user)
        {
            return await UserRepository.Update(user);
        }

        public async Task<Models.User> ChangePassword(string username, string oldpassword, string newpassword)
        {
            await UserRepository.ValidatePassword(username, oldpassword);
            return await UserRepository.ChangePassword(username, newpassword);
        }

        public async Task<Models.User> ValidatePassword(string username, string password)
        {
            return await UserRepository.ValidatePassword(username, password);
        }
    }
}
