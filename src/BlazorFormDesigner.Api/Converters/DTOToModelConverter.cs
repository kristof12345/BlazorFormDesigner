using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace BlazorFormDesigner.Api.Converters
{
    public static class DTOToModelConverter
    {
        public static BusinessLogic.Models.User ToModel(this Web.Models.UserRequest dto, IMapper mapper)
        {
            return mapper.Map<BusinessLogic.Models.User>(dto);
        }

        public static BusinessLogic.Models.Form ToModel(this Web.Models.FormRequest dto, IMapper mapper)
        {
            return mapper.Map<BusinessLogic.Models.Form>(dto);
        }

        public static BusinessLogic.Models.Question ToModel(this Web.Models.QuestionRequest dto, IMapper mapper)
        {
            return mapper.Map<BusinessLogic.Models.Question>(dto);
        }

        public static BusinessLogic.Models.Option ToModel(this Web.Models.Option dto, IMapper mapper)
        {
            return mapper.Map<BusinessLogic.Models.Option>(dto);
        }

        public static BusinessLogic.Models.Answer ToModel(this Web.Models.Answer dto, IMapper mapper)
        {
            return mapper.Map<BusinessLogic.Models.Answer>(dto);
        }

        public static List<BusinessLogic.Models.Answer> ToModel(this IEnumerable<Web.Models.Answer> dtos, IMapper mapper)
        {
            return dtos.Select(dto => dto.ToModel(mapper)).ToList();
        }
    }
}
