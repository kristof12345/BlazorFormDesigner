using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace BlazorFormDesigner.Api.Converters
{
    public static class ModelToDTOConverter
    {
        public static Web.Models.User ToDTO(this BusinessLogic.Models.User model, IMapper mapper)
        {
            return mapper.Map<Web.Models.User>(model);
        }

        public static IEnumerable<Web.Models.User> ToDTO(this IEnumerable<BusinessLogic.Models.User> model, IMapper mapper)
        {
            return model.Select(item => item.ToDTO(mapper));
        }

        public static Web.Models.LoginResponse ToDTO(this BusinessLogic.Models.User model, IMapper mapper, string token)
        {
            var dto = mapper.Map<Web.Models.LoginResponse>(model);
            dto.Token = token;
            return dto;
        }
    }
}
