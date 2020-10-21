using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BlazorFormDesigner.Web.Models;

namespace BlazorFormDesigner.Web.Services
{
    public class UserService
    {
        public event Func<Task> LoginEvent;

        public User User { get; private set; }

        public async Task<ErrorResponse> Login(LoginRequest request)
        {
            try
            {
                var response = await AppService.Client.PutAsJsonAsync("user/login", request);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
                    User = loginResponse.ToUser();
                    AppService.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Token);
                    await LoginEvent?.Invoke();
                    return null;
                }

                return await response.Content.ReadAsAsync<ErrorResponse>();
            }
            catch (Exception)
            {
                return new ErrorResponse { Content = "Unable to connect to server." };
            }
        }

        public async Task Logout()
        {
            User = null;
            await LoginEvent?.Invoke();
        }

        public async Task<ErrorResponse> Register(UserRequest request)
        {
            try
            {
                var response = await AppService.Client.PostAsJsonAsync("user", request);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadAsAsync<User>();
                    return null;
                }

                return await response.Content.ReadAsAsync<ErrorResponse>();
            }
            catch (Exception)
            {
                return new ErrorResponse { Content = "Unable to connect to server." };
            }
        }
    }
}
