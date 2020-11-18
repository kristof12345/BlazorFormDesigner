using BlazorFormDesigner.BusinessLogic.Exceptions;
using BlazorFormDesigner.BusinessLogic.Extensions;
using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormDesigner.BusinessLogic.Services
{
    public class AnswerService
    {
        private readonly IFormRepository FormRepository;
        private readonly IAnswerRepository AnswerRepository;
        private readonly IUserRepository UserRepository;

        private readonly XLColor red = XLColor.FromArgb(246, 201, 207);
        private readonly XLColor green = XLColor.FromArgb(202, 232, 204);

        public AnswerService(IAnswerRepository answerRepository, IUserRepository userRepository, IFormRepository formRepository)
        {
            AnswerRepository = answerRepository;
            UserRepository = userRepository;
            FormRepository = formRepository;
        }

        public async Task<Response> Save(User user, string formId, List<Answer> answers)
        {
            var form = await FormRepository.GetById(formId);

            if (form.StartDate > DateTime.Now || form.EndDate < DateTime.Now) throw new FormException("This form is currently not available.");

            await UserRepository.RegisterAnswer(user.Username, formId);

            return await AnswerRepository.Create(new Response { FormId = formId, Answers = answers, UserId = user.Username });
        }

        public async Task<AnswerDetails> GetDetails(string formid, string questionid)
        {
            var responses = await AnswerRepository.GetByFormId(formid);

            var answers = new Dictionary<string, int>();
            var correct = 0;

            foreach (var response in responses)
            {
                var a = response.Answers.Where(a => a.QuestionId == questionid).First();
                correct += await calculatePoints(formid, questionid, a);
                foreach (var o in a.SelectedOptions)
                {
                    if (!answers.ContainsKey(o)) answers.Add(o, 1);
                    else answers[o]++;
                }
            }

            var details = new AnswerDetails
            {
                Answers = answers,
                IncorrectAnswers = responses.Count - correct,
                CorrectAnswers = correct
            };

            return details;
        }

        private async Task<int> calculatePoints(string formid, string questionid, Answer answer)
        {
            var form = await FormRepository.GetById(formid);
            return calculatePoints(questionid, answer, form);
        }

        private static int calculatePoints(string questionid, Answer answer, Form form)
        {
            var correctAnswer = form.Questions.Where(q => q.Id == questionid).First().Options.Where(o => o.IsCorrect).Select(o => o.Content).ToList();

            if (ListExtensions.Same(answer.SelectedOptions, correctAnswer)) return 1;

            return 0;
        }

        public async Task<XLWorkbook> CreateExelDocument(string formId)
        {
            var form = await FormRepository.GetById(formId);
            var answers = await AnswerRepository.GetByFormId(formId);

            var workbook = new XLWorkbook();

            CreateSummaryTab(form, answers, workbook);
            CreateQuestionTabs(form, answers, workbook);

            return workbook;
        }

        private void CreateSummaryTab(Form form, List<Response> answers, XLWorkbook workbook)
        {
            IXLWorksheet worksheet = workbook.Worksheets.Add("Summary");
            worksheet.Cell(1, 1).Value = form.Title;
            worksheet.Cell(1, 1).Style.Font.FontSize = 30;
            worksheet.Cell(2, 1).Value = "https://localhost:5002/myforms/" + form.Id;
            worksheet.Cell(2, 1).Style.Font.FontSize = 15;

            worksheet.Cell(4, 1).Value = "Username";
            var col = 2;
            foreach (var q in form.Questions)
            {
                worksheet.Cell(4, col++).Value = q.Title;
            }
            worksheet.Cell(4, col).Value = "sum";

            for (int index = 1; index <= answers.Count; index++)
            {
                var sum = 0;
                col = 2;
                worksheet.Cell(index + 4, 1).Value = answers[index - 1].UserId;
                foreach (var q in form.Questions)
                {
                    var points = calculatePoints(q.Id, answers[index - 1].Answers.Where(a => a.QuestionId == q.Id).Single(), form);
                    worksheet.Cell(index + 4, col).Value = points;
                    if (q.IsCorrected)
                    {
                        worksheet.Cell(index + 4, col).Style.Fill.BackgroundColor = points == 1 ? green : red;
                    }
                    col++;
                    sum += points;
                }
                worksheet.Cell(index + 4, col).Value = sum;
            }
        }

        private void CreateQuestionTabs(Form form, List<Response> answers, XLWorkbook workbook)
        {
            foreach (var q in form.Questions)
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add(q.Title);
                worksheet.Cell(1, 1).Value = "Username";
                worksheet.Cell(1, 2).Value = "Point";
                worksheet.Cell(1, 3).Value = "Answer";
                for (int index = 1; index <= answers.Count; index++)
                {
                    worksheet.Cell(index + 1, 1).Value = answers[index - 1].UserId;
                    var answer = answers[index - 1].Answers.Where(a => a.QuestionId == q.Id).Single();
                    var points = calculatePoints(q.Id, answer, form);
                    worksheet.Cell(index + 1, 2).Value = points;
                    worksheet.Cell(index + 1, 2).Style.Fill.BackgroundColor = points == 1 ? green : red;
                    for (int row = 0; row < answer.SelectedOptions.Count; row++)
                    {
                        worksheet.Cell(index + 1, row + 3).Value = answer.SelectedOptions[row];
                        if (q.IsCorrected)
                        {
                            worksheet.Cell(index + 1, row + 3).Style.Fill.BackgroundColor = isCorrectOption(q.Id, answer.SelectedOptions[row], form) ? green : red;
                        }
                    }
                }
            }
        }

        private static bool isCorrectOption(string questionid, string option, Form form)
        {
            var correctAnswer = form.Questions.Where(q => q.Id == questionid).First().Options.Where(o => o.IsCorrect).Select(o => o.Content).ToList();
            return correctAnswer.Contains(option);
        }
    }
}
