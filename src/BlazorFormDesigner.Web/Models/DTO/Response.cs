using System.Collections.Generic;

namespace BlazorFormDesigner.Web.Models
{
    public class Response
    {
        public string FormId { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
