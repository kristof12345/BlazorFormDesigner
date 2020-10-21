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
            for (int i = 0; i < forms.Count; i++)
            {
                forms[i].Status = await getStatus(user.Username, forms[i].Id);
                forms[i].MaxPoints = forms[i].Questions.Where(q => q.IsCorrected).Count() * 1.0;
            }

            foreach (var form in forms.Where(f => f.Status == FormStatus.Answered))
            {
                await getPoints(user, forms, form);

                await getSelectedAnswers(user, forms, form);
            }

            return forms;
        }

        private async Task getPoints(User user, List<Form> forms, Form form)
        {
            var u = await UserRepository.GetByUsername(user.Username);
            forms.GetById(form.Id).Points = u.AnsweredForms[form.Id];
        }

        private async Task getSelectedAnswers(User user, List<Form> forms, Form form)
        {
            var response = await AnswerRepository.Get(user.Username, form.Id);
            var questions = forms.GetById(form.Id).Questions;
            for (int i = 0; i < questions.Count; i++)
            {
                questions[i].SelectedOptions = response.Answers.GetByQuestion(questions[i].Id).SelectedOptions;
            }
        }

        private async Task<FormStatus> getStatus(string username, string formId)
        {
            var user = await UserRepository.GetByUsername(username);

            if (user.AnsweredForms.ContainsKey(formId)) return FormStatus.Answered;
            if (user.DismissedForms.Contains(formId)) return FormStatus.Dismissed;
            return FormStatus.Available;
        }

        public async Task<Form> GetById(string id)
        {
            return await FormRepository.GetById(id);
        }

        public async Task<Form> Create(Form form, User user)
        {
            form.CreationDate = DateTime.Now;
            form.CreatorId = user.Username;
            return await FormRepository.Create(form);
        }

        public async Task<Form> Delete(string id, User user)
        {
            var form = await FormRepository.GetById(id);
            if (form.CreatorId != user.Username) throw new AuthorizationException();

            return form;
        }
    }
}
