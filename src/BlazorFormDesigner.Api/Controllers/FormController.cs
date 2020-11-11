using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Services;
using BlazorFormDesigner.Web.Models;
using BlazorFormDesigner.Api.Converters;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                ValidateUser();
            }
            catch (Exception)
            {
                return Unauthorized();
            }

            var forms = await FormService.GetAll(User);

            return Ok(forms.ToDTO(mapper));
        }

        [HttpGet]
        [Route("my")]
        public async Task<ActionResult<List<Form>>> GetMy()
        {
            try
            {
                ValidateUser();
            }
            catch (Exception)
            {
                return Unauthorized();
            }

            var forms = await FormService.GetMy(User);

            return Ok(forms.ToDTO(mapper));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Form>> GetById([FromRoute] string id)
        {
            try
            {
                ValidateUser();
            }
            catch (Exception)
            {
                return Unauthorized();
            }

            var form = await FormService.GetById(id);

            return Ok(form.ToDTO(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<Form>> Create(FormRequest form)
        {
            try
            {
                ValidateUser();
            }
            catch (Exception)
            {
                return Unauthorized();
            }

            var result = await FormService.Create(form.ToModel(mapper), User);

            return Ok(result);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult> Start([FromRoute] string id)
        {
            try
            {
                ValidateUser();
                await FormService.Start(id, User);
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update([FromRoute] string id, FormRequest form)
        {
            try
            {
                ValidateUser();
                await FormService.Update(id, form.ToModel(mapper), User);
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Form>> Delete([FromRoute] string id)
        {
            try
            {
                ValidateUser();
                var result = await FormService.Delete(id, User);
                return Ok(result);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("{id}/dismiss")]
        public async Task<ActionResult> Dismiss([FromRoute] string id)
        {
            try
            {
                ValidateUser();
            }
            catch (Exception)
            {
                return Unauthorized();
            }

            await FormService.Dismiss(id, User);

            return Ok();
        }
    }
}
