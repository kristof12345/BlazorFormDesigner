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

        public FormService(IFormRepository formRepository)
        {
            FormRepository = formRepository;
        }

        public async Task<List<Form>> GetAll()
        {
            return await FormRepository.GetAll();
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
