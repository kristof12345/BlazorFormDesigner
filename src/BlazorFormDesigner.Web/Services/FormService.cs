using BlazorFormDesigner.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace BlazorFormDesigner.Web.Services
{
    public class FormService
    {
        public async Task<IEnumerable<Form>> GetAll()
        {
            var response = await AppService.Client.GetAsync("form");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Form>>();
            }

            return new List<Form>();
        }

        public async Task<IEnumerable<Form>> GetMy()
        {
            var response = await AppService.Client.GetAsync("form/my");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Form>>();
            }

            return new List<Form>();
        }

        public async Task<Form> GetById(string id)
        {
            var response = await AppService.Client.GetAsync("form/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Form>();
            }

            return null;
        }

        public async Task Create(FormRequest request)
        {
            await AppService.Client.PostAsJsonAsync("form", request);
        }

        public async Task Update(string id, FormRequest request)
        {
            await AppService.Client.PutAsJsonAsync("form/" + id, request);
        }

        public async Task Dismiss(string id)
        {
            await AppService.Client.PutAsJsonAsync("form/" + id + "/dismiss", "dismiss");
        }
    }
}
