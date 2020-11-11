using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Services;
using BlazorFormDesigner.Web.Models;
using BlazorFormDesigner.Api.Converters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System;
using System.IO;

namespace BlazorFormDesigner.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : AppController
    {
        private readonly AnswerService AnswerService;

        public AnswerController(AnswerService answerService, IMapper mapper) : base(mapper)
        {
            AnswerService = answerService;
        }

        [HttpPost]
        public async Task<ActionResult> Save(Response request)
        {
            try
            {
                ValidateUser();

                var result = await AnswerService.Save(User, request.FormId, request.Answers.ToModel(mapper));

                return Ok(result);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("{formid}/{questionid}")]
        public async Task<ActionResult<AnswerDetails>> GetByFormIdAndQuestionId([FromRoute] string formid, [FromRoute] string questionid)
        {
            try
            {
                ValidateUser();
            }
            catch (Exception)
            {
                return Unauthorized();
            }

            var details = await AnswerService.GetDetails(formid, questionid);

            return Ok(details.ToDTO(mapper));
        }

        [HttpGet]
        [Route("{formid}")]
        public async Task<IActionResult> DownloadExcelDocument([FromRoute] string formid)
        {
            try
            {
                ValidateUser();
            }
            catch (Exception)
            {
                return Unauthorized();
            }

            try
            {
                XLWorkbook workbook = await AnswerService.CreateExelDocument(formid);

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "answers.xlsx";

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, contentType, fileName);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }
    }
}
