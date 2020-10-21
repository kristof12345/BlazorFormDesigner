using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Services;
using BlazorFormDesigner.Web.Models;
using BlazorFormDesigner.Api.Converters;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFormDesigner.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : AppController
    {
        private readonly FormService FormService;

        public FormController(FormService formService, IMapper mapper) : base(mapper)
        {
            FormService = formService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Form>>> GetAll()
        {
            var user = ValidateUser();

            var forms = await FormService.GetAll(user);

            return Ok(forms.ToDTO(mapper));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Form>> GetById([FromRoute] string id)
        {
            ValidateUser();

            var form = await FormService.GetById(id);

            return Ok(form.ToDTO(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<Form>> Create(FormRequest form)
        {
            var user = ValidateUser();

            var result = await FormService.Create(form.ToModel(mapper), user);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Form>> Delete([FromRoute] string id)
        {
            var user = ValidateUser();

            var result = await FormService.Delete(id, user);

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}/dismiss")]
        public async Task<ActionResult> Dismiss([FromRoute] string id)
        {
            var user = ValidateUser();

            await FormService.Dismiss(id, user);

            return Ok();
        }
    }
}
