using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Models;
using BlazorFormDesigner.Web.Requests;
using System.Collections.Generic;
using System.Linq;

namespace BlazorFormDesigner.Api.Converters
{
    public static class DTOToModelConverter
    {
        public static User ToModel(this UserRequest dto, IMapper mapper)
        {
            return mapper.Map<User>(dto);
        }

        public static Form ToModel(this FormRequest dto, IMapper mapper)
        {
            return mapper.Map<Form>(dto);
        }

        public static Question ToModel(this QuestionRequest dto, IMapper mapper)
        {
            return mapper.Map<Question>(dto);
        }

        public static Option ToModel(this Web.Models.Option dto, IMapper mapper)
        {
            return mapper.Map<Option>(dto);
        }

        public static Answer ToModel(this Web.Models.Answer dto, IMapper mapper)
        {
            return mapper.Map<Answer>(dto);
        }

        public static List<Answer> ToModel(this IEnumerable<Web.Models.Answer> dtos, IMapper mapper)
        {
            return dtos.Select(dto => dto.ToModel(mapper)).ToList();
        }
    }
}
