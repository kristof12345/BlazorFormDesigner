using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Models;

namespace BlazorFormDesigner.Database.Converters
{
    public static class ModelToEntityConverter
    {
        public static Entities.User WithPassword(this Entities.User entity, string password, byte[] salt)
        {
            entity.Password = password;
            entity.Salt = salt;
            return entity;
        }

        public static Entities.User ToEntity(this User model, IMapper mapper)
        {
            return mapper.Map<Entities.User>(model);
        }

        public static Entities.Form ToEntity(this Form model, IMapper mapper)
        {
            return mapper.Map<Entities.Form>(model);
        }

        public static Entities.Question ToEntity(this Question model, IMapper mapper)
        {
            return mapper.Map<Entities.Question>(model);
        }

        public static Entities.Option ToEntity(this Option model, IMapper mapper)
        {
            return mapper.Map<Entities.Option>(model);
        }

        public static Entities.Response ToEntity(this Response model, IMapper mapper)
        {
            return mapper.Map<Entities.Response>(model);
        }
    }
}
