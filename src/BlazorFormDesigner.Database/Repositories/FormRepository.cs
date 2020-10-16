using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Models;
using BlazorFormDesigner.Database.Converters;
using BlazorFormDesigner.Database.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFormDesigner.Database.Repositories
{
    public class FormRepository : AppRepository, IFormRepository
    {
        private readonly IMongoCollection<Entities.Form> forms;

        public FormRepository(DatabaseSettings settings, IMapper mapper) : base(settings, mapper)
        {
            forms = database.GetCollection<Entities.Form>(settings.FormsCollectionName);
        }
        public async Task<List<Form>> GetAll()
        {
            var result = await forms.FindAll().ToListAsync();
            return result.ToModel(mapper);
        }

        public async Task<Form> GetById(string id)
        {
            var result = await forms.Find(form => form.Id == id).FirstOrDefaultAsync();
            return result.ToModel(mapper);
        }

        public async Task<Form> Create(Form form)
        {
            var entity = form.ToEntity(mapper);
            await forms.InsertOneAsync(entity);
            return entity.ToModel(mapper);
        }
    }
}
