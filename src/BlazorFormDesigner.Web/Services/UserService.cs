using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorFormDesigner.Web.Models;

namespace BlazorFormDesigner.Web.Services
{
    public class UserService
    {
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
