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

            await UserRepository.RegisterAnswer(user.Username, formId);

            return await AnswerRepository.Create(response);
        }

        public async Task<AnswerDetails> GetDetails(string formid, string questionid)
        {
            var responses = await AnswerRepository.GetByFormId(formid);

            var answers = new Dictionary<string, int>();
            var correct = 0;

            foreach (var response in responses)
            {
                var a = response.Answers.Where(a => a.QuestionId == questionid).First();
                correct += await calculatePoints(formid, questionid, a);
                foreach (var o in a.SelectedOptions)
                {
                    if (!answers.ContainsKey(o)) answers.Add(o, 1);
                    else answers[o]++;
                }
            }

            var details = new AnswerDetails
            {
                Answers = answers,
                IncorrectAnswers = responses.Count - correct,
                CorrectAnswers = correct
            };

            return details;
        }

        private async Task<int> calculatePoints(string formid, string questionid, Answer answer)
        {
            var form = await FormRepository.GetById(formid);
            var correctAnswer = form.Questions.Where(q => q.Id == questionid).First().Options.Where(o => o.IsCorrect).Select(o => o.Content).ToList();

            if (ListExtensions.Same(answer.SelectedOptions, correctAnswer)) return 1;

            return 0;
        }
    }
}
