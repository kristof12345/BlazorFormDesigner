using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Models;
using BlazorFormDesigner.Database.Converters;
using BlazorFormDesigner.Database.Settings;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BlazorFormDesigner.Database.Repositories
{
    public class AnswerRepository : AppRepository, IAnswerRepository
    {
        private readonly IMongoCollection<Entities.Response> responses;

        public AnswerRepository(DatabaseSettings settings, IMapper mapper) : base(settings, mapper)
        {
            responses = database.GetCollection<Entities.Response>(settings.AnswersCollectionName);
        }

        public async Task<Response> Create(Response response)
        {
            var entity = response.ToEntity(mapper);
            await responses.InsertOneAsync(entity);
            return entity.ToModel(mapper);
        }

        public async Task<Response> GetByUserIdAndFormId(string userId, string formId)
        {
            var result = await responses.Find(r => r.UserId == userId && r.FormId == formId).FirstOrDefaultAsync();
            return result.ToModel(mapper);
        }
    }
}
