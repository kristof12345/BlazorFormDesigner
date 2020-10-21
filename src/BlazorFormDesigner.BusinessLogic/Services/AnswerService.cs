using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFormDesigner.BusinessLogic.Services
{
    public class AnswerService
    {
        private readonly IFormRepository FormRepository;
        private readonly IAnswerRepository AnswerRepository;
        private readonly IUserRepository UserRepository;

        public AnswerService(IAnswerRepository answerRepository, IUserRepository userRepository, IFormRepository formRepository)
        {
            AnswerRepository = answerRepository;
            UserRepository = userRepository;
            FormRepository = formRepository;
        }

        public async Task<Response> Save(User user, string formId, List<Answer> answers)
        {
            var response = new Response
            {
                FormId = formId,
                Answers = answers,
                UserId = user.Username
            };

            await UserRepository.RegisterAnswer(user.Username, formId);

            return await AnswerRepository.Create(response);
        }
    }
}
