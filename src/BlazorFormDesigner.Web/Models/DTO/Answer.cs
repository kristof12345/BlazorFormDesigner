using System.Collections.Generic;

namespace BlazorFormDesigner.Web.Models
{
    public class Answer
    {
        public string QuestionId { get; set; }

        public List<string> SelectedOptions { get; set; } = new List<string>();
    }
}
