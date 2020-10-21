using BlazorFormDesigner.BusinessLogic.Extensions;
using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;
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

            var points = 0.0;

            var form = await FormRepository.GetById(formId);

            foreach (var question in form.Questions.Where(q => q.IsCorrected))
            {
                var myAnswer = answers.GetByQuestion(question.Id).SelectedOptions;
                var correctAnswer = question.Options.Where(o => o.IsCorrect).Select(o => o.Content).ToList();

                if (ListExtensions.Same(myAnswer, correctAnswer)) points += 1.0;
            }

            await UserRepository.RegisterAnswer(user.Username, formId, points);

            return await AnswerRepository.Create(response);
        }
    }
}
