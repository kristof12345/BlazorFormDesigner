using BlazorFormDesigner.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFormDesigner.BusinessLogic.Interfaces
{
    public interface IAnswerRepository
    {
        Task<Response> GetByUserIdAndFormId(string userId, string formId);
        Task<Response> Create(Response response);
        Task<List<Response>> GetByFormId(string id);
    }
}
