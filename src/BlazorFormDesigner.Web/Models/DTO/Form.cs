using System;
using System.Collections.Generic;
using BlazorFormDesigner.Web.Extensions;

namespace BlazorFormDesigner.Web.Models
{
    public class Form
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int AvailableMinutes { get; set; }

        public FormStatus Status { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        public List<Question> Questions { get; set; }

        public double Points { get; set; }

        public double MaxPoints { get; set; }

        public DateTime RemainingTime { get; set; }

        internal FormRequest ToRequest()
        {
            var request = new FormRequest
            {
                Title = Title,
                StartDate = StartDate,
                EndDate = EndDate,
                AvailableMinutes = AvailableMinutes,
                Description = Description,
                Questions = new List<QuestionRequest>()
            };

            foreach (var question in Questions)
            {
                var q = new QuestionRequest
                {
                    Title = question.Title,
                    Type = question.Type.GetAttributeValue()
                };
                ;
                q.Description = question.Description;
                q.IsCorrected = question.IsCorrected;
                q.Options = question.Options;
                request.Questions.Add(q);
            }
            return request;
        }
    }
}
