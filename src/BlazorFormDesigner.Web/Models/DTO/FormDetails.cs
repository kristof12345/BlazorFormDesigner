using System.Collections.Generic;

namespace BlazorFormDesigner.Web.Models
{
    public class FormDetails
    {
        public string Id { get; set; }

        public List<Question> Questions { get; set; }

        public int NumberOfAnswers { get; set; }

        public double AveragePoints { get; set; }

        public double MaxPoints { get; set; }
    }
}
