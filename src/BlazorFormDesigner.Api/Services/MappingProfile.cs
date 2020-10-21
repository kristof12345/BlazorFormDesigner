using AutoMapper;

namespace BlazorFormDesigner.Api.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Database - Models
            CreateMap<Database.Entities.User, BusinessLogic.Models.User>();
            CreateMap<BusinessLogic.Models.User, Database.Entities.User>();

            CreateMap<Database.Entities.Form, BusinessLogic.Models.Form>();
            CreateMap<BusinessLogic.Models.Form, Database.Entities.Form>();

            CreateMap<Database.Entities.Question, BusinessLogic.Models.Question>();
            CreateMap<BusinessLogic.Models.Question, Database.Entities.Question>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Database.Entities.Option, BusinessLogic.Models.Option>();
            CreateMap<BusinessLogic.Models.Option, Database.Entities.Option>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Database.Entities.Response, BusinessLogic.Models.Response>();
            CreateMap<BusinessLogic.Models.Response, Database.Entities.Response>();

            CreateMap<Database.Entities.Answer, BusinessLogic.Models.Answer>();
            CreateMap<BusinessLogic.Models.Answer, Database.Entities.Answer>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //Requests - Models
            CreateMap<Web.Models.UserRequest, BusinessLogic.Models.User>();
            CreateMap<Web.Models.FormRequest, BusinessLogic.Models.Form>();
            CreateMap<Web.Models.QuestionRequest, BusinessLogic.Models.Question>();
            CreateMap<Web.Models.Option, BusinessLogic.Models.Option>();
            CreateMap<Web.Models.Response, BusinessLogic.Models.Response>();
            CreateMap<Web.Models.Answer, BusinessLogic.Models.Answer>();

            //Models - Responses
            CreateMap<BusinessLogic.Models.User, Web.Models.User>();
            CreateMap<BusinessLogic.Models.User, Web.Models.LoginResponse>();
        }
    }
}
