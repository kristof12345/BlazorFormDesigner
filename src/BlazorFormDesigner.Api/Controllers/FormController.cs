using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Models;
using BlazorFormDesigner.BusinessLogic.Services;
using BlazorFormDesigner.Web.Responses;
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
        public async Task<ActionResult<List<FormResponse>>> GetAll()
        {
            ValidateUser();

            var forms = await FormService.GetAll();

            return Ok(forms);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<FormResponse>> GetById([FromRoute] string id)
        {
            ValidateUser();

            var forms = await FormService.GetById(id);

            return Ok(forms);
        }

        [HttpPost]
        public async Task<ActionResult<Form>> Create(Form form)
        {
            var user = ValidateUser();

            var result = await FormService.Create(form, user);

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
    }
}
