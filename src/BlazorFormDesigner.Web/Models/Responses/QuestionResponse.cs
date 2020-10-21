using BlazorFormDesigner.Web.Models;
using System.Collections.Generic;

namespace BlazorFormDesigner.Web.Responses
{
    public class QuestionResponse
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public QuestionType Type { get; set; }

        public bool IsCorrected { get; set; }

        public List<Option> Options { get; set; }

        public List<string> SelectedOptions { get; set; }
    }
}
