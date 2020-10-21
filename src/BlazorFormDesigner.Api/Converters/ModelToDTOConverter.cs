using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Models;
using BlazorFormDesigner.Web.Responses;
using System.Collections.Generic;
using System.Linq;

namespace BlazorFormDesigner.Api.Converters
{
    public static class ModelToDTOConverter
    {
        public static UserResponse ToDTO(this User model, IMapper mapper)
        {
            return mapper.Map<UserResponse>(model);
        }

        public static IEnumerable<UserResponse> ToDTO(this IEnumerable<User> model, IMapper mapper)
        {
            return model.Select(item => item.ToDTO(mapper));
        }

        public static LoginResponse ToDTO(this User model, IMapper mapper, string token)
        {
            var dto = mapper.Map<LoginResponse>(model);
            dto.Token = token;
            return dto;
        }
    }
}
