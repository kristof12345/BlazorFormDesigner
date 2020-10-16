using BlazorFormDesigner.BusinessLogic.Exceptions;
using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFormDesigner.BusinessLogic.Services
{
    public class FormService
    {
        private readonly IFormRepository FormRepository;
        private readonly IUserRepository UserRepository;

        public FormService(IFormRepository formRepository, IUserRepository userRepository)
        {
            FormRepository = formRepository;
            UserRepository = userRepository;
        }

        public async Task<List<Form>> GetAll(User user)
        {
            var forms = await FormRepository.GetAll();
            for(int i=0; i < forms.Count; i++)
            {
                forms[i].Status = await getStatus(user.Username, forms[i].Id);
            }

            return forms;
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
