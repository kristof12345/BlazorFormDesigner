﻿using BlazorFormDesigner.BusinessLogic.Exceptions;
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
                form.Status = await getStatus(user.Username, form);
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

        public async Task Start(string id, User user)
        {
            var form = await FormRepository.GetById(id);
            await UserRepository.RegisterStart(user.Username, id, DateTime.Now.AddHours(1).AddMinutes(form.AvailableMinutes));
        }

        private async Task<FormStatus> getStatus(string username, Form form)
        {
            if (form.EndDate < DateTime.Now) return FormStatus.Expired;
            if (form.StartDate > DateTime.Now) return FormStatus.Upcoming;

            var user = await UserRepository.GetByUsername(username);
            if (user.AnsweredForms.Contains(form.Id)) return FormStatus.Answered;
            if (user.DismissedForms.Contains(form.Id)) return FormStatus.Dismissed;

            return FormStatus.Available;
        }

        public async Task<Form> GetById(string id, User user)
        {
            var form = await FormRepository.GetById(id);
            var u = await UserRepository.GetByUsername(user.Username);
            form.RemainingTime = u.StartedForms[id];
            return form;
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

        public async Task<Form> Update(string id, Form form, User user)
        {
            var original = await FormRepository.GetById(id);
            if (original.CreatorId != user.Username) throw new AuthorizationException();

            form.CreationDate = DateTime.Now;
            form.CreatorId = user.Username;
            return await FormRepository.Update(id, form);
        }

        public async Task<Form> Delete(string id, User user)
        {
            var original = await FormRepository.GetById(id);
            if (original.CreatorId != user.Username) throw new AuthorizationException();

            return original;
        }

        public async Task Dismiss(string id, User user)
        {
            await UserRepository.RegisterDismiss(user.Username, id);
        }
    }
}
