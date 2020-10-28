using BlazorFormDesigner.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorFormDesigner.Web.Services
{
    public class AnswerService
    {
        public async Task Save(string formId, List<Answer> answers)
        {
            var formAnswer = new Response { FormId = formId, Answers = answers };

            var response = await AppService.Client.PostAsJsonAsync("answer", formAnswer);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
        }

        public async Task<AnswerDetails> GetDetails(string formId, string questionId)
        {
            var response = await AppService.Client.GetAsync("answer/" + formId + "/" + questionId);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<AnswerDetails>();
            }

            return null;
        }
    }
}
