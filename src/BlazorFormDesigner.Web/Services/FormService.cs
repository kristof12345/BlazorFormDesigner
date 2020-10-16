using BlazorFormDesigner.Web.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorFormDesigner.Web.Services
{
    public class FormService
    {
        public async Task<IEnumerable<FormResponse>> GetAll()
        {
            var response = await AppService.Client.GetAsync("form");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<FormResponse>>();
            }

            return new List<FormResponse>();
        }

        public async Task<FormResponse> GetById(string id)
        {
            var response = await AppService.Client.GetAsync("form/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<FormResponse>();
            }

            return null;
        }
    }
}
