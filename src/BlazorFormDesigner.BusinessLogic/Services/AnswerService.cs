using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFormDesigner.BusinessLogic.Services
{
    public class AnswerService
    {
        private readonly IAnswerRepository AnswerRepository;

        public AnswerService(IAnswerRepository answerRepository)
        {
            AnswerRepository = answerRepository;
        }

        public Task<Response> Save(User user, string formId, List<Answer> answers)
        {
            var response = new Response
            {
                FormId = formId,
                Answers = answers,
                UserId = user.Username
            };

            return AnswerRepository.Create(response);
        }
    }
}
