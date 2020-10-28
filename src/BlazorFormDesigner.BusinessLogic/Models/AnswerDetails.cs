using System.Collections.Generic;

namespace BlazorFormDesigner.BusinessLogic.Models
{
    public class AnswerDetails
    {
        public string QuestionId { get; set; }

        public Dictionary<string, int> Answers { get; set; }

        public int CorrectAnswers { get; set; }

        public int IncorrectAnswers { get; set; }
    }
}
