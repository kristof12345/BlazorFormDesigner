using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorFormDesigner.Database.Converters
{
    public static class EntityToModelConverter
    {
        public static User ToModel(this Entities.User entity, IMapper mapper)
        {
            return mapper.Map<User>(entity);
        }

        public static List<User> ToModel(this IEnumerable<Entities.User> entities, IMapper mapper)
        {
            return entities.Select(entity => entity.ToModel(mapper)).ToList();
        }

        public static Form ToModel(this Entities.Form entity, IMapper mapper)
        {
            return mapper.Map<Form>(entity);
        }

        public static List<Form> ToModel(this IEnumerable<Entities.Form> entities, IMapper mapper)
        {
            return entities.Select(entity => entity.ToModel(mapper)).ToList();
        }

        public static Question ToModel(this Entities.Question entity, IMapper mapper)
        {
            return mapper.Map<Question>(entity);
        }

        public static List<Question> ToModel(this IEnumerable<Entities.Question> entities, IMapper mapper)
        {
            return entities.Select(entity => entity.ToModel(mapper)).ToList();
        }

        public static Option ToModel(this Entities.Option entity, IMapper mapper)
        {
            return mapper.Map<Option>(entity);
        }

        public static List<Option> ToModel(this IEnumerable<Entities.Option> entities, IMapper mapper)
        {
            return entities.Select(entity => entity.ToModel(mapper)).ToList();
        }

        public static Response ToModel(this Entities.Response entity, IMapper mapper)
        {
            return mapper.Map<Response>(entity);
        }

        public static List<Response> ToModel(this IEnumerable<Entities.Response> entities, IMapper mapper)
        {
            return entities.Select(entity => entity.ToModel(mapper)).ToList();
        }
    }
}