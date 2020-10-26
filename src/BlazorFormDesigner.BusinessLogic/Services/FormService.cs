using BlazorFormDesigner.BusinessLogic.Exceptions;
using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Models;
using BlazorFormDesigner.BusinessLogic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormDesigner.BusinessLogic.Services
{
    public class FormService
    {
        private readonly IFormRepository FormRepository;
        private readonly IUserRepository UserRepository;
        private readonly IAnswerRepository AnswerRepository;

        public FormService(IFormRepository formRepository, IUserRepository userRepository, IAnswerRepository answerRepository)
        {
            FormRepository = formRepository;
            UserRepository = userRepository;
            AnswerRepository = answerRepository;
        }

        public async Task<List<Form>> GetAll(User user)
        {
            var forms = await FormRepository.GetAll();

            foreach (var form in forms)
            {
                form.Status = await getStatus(user.Username, form.Id);
                form.MaxPoints = form.Questions.Where(q => q.IsCorrected).Count() * 1.0;
            }

            foreach (var form in forms.Where(f => f.Status == FormStatus.Answered))
            {
                var response = await getSelectedAnswers(user, forms, form);
                form.Points = calculatePoints(form, response.Answers);
            }

            return forms;
        }

        private double calculatePoints(Form form, List<Answer> answers)
        {
            var points = 0.0;

            foreach (var question in form.Questions.Where(q => q.IsCorrected))
            {
                var myAnswer = answers.GetByQuestion(question.Id).SelectedOptions;
                var correctAnswer = question.Options.Where(o => o.IsCorrect).Select(o => o.Content).ToList();

                if (ListExtensions.Same(myAnswer, correctAnswer)) points += 1.0;
            }

            return points;
        }

        private async Task<Response> getSelectedAnswers(User user, List<Form> forms, Form form)
        {
            var response = await AnswerRepository.GetByUserIdAndFormId(user.Username, form.Id);
            var questions = forms.GetById(form.Id).Questions;
            for (int i = 0; i < questions.Count; i++)
            {
                questions[i].SelectedOptions = response.Answers.GetByQuestion(questions[i].Id).SelectedOptions;
            }

            return response;
        }

        private async Task<FormStatus> getStatus(string username, string formId)
        {
            var user = await UserRepository.GetByUsername(username);

            if (user.AnsweredForms.Contains(formId)) return FormStatus.Answered;
            if (user.DismissedForms.Contains(formId)) return FormStatus.Dismissed;
            return FormStatus.Available;
        }

        public async Task<Form> GetById(string id)
        {
            return await FormRepository.GetById(id);
        }

        public async Task<FormDetails> GetDetailsById(string id, string username)
        {
            var form = await FormRepository.GetById(id);
            if (form.CreatorId != username) return null;

            var answers = AnswerRepository.GetByFormId(id);

            var details = new FormDetails();
            return details;
        }

        public async Task<List<Form>> GetMy(User user)
        {
            return await FormRepository.GetByUser(user.Username);
        }

        public async Task<Form> Create(Form form, User user)
        {
            form.CreationDate = DateTime.Now;
            form.CreatorId = user.Username;
            await UserRepository.RegisterCreator(user.Username, form.Id);
            return await FormRepository.Create(form);
        }

        public async Task<Form> Delete(string id, User user)
        {
            var form = await FormRepository.GetById(id);
            if (form.CreatorId != user.Username) throw new AuthorizationException();

            return form;
        }

        public async Task Dismiss(string id, User user)
        {
            await UserRepository.RegisterDismiss(user.Username, id);
        }
    }
}
