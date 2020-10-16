using System.Collections.Generic;

namespace BlazorFormDesigner.BusinessLogic.Models
{
    public class Answer
    {
        public string Id { get; set; }

        public string QuestionId { get; set; }

        public List<string> SelectedOptions { get; set; }
    }
}
