﻿using BlazorFormDesigner.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFormDesigner.BusinessLogic.Interfaces
{
    public interface IFormRepository
    {
        Task<List<Form>> GetAll();
        Task<List<Form>> GetByUser(string username);
        Task<Form> GetById(string id);
        Task<Form> Create(Form form);
    }
}
