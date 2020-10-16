using System.Collections.Generic;

namespace BlazorFormDesigner.Web.Requests
{
    public class FormRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsProtected { get; set; }

        public string Password { get; set; }

        public List<QuestionRequest> Questions { get; set; }
    }
}
