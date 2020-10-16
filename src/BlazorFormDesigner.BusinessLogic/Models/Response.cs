using System.Collections.Generic;

namespace BlazorFormDesigner.BusinessLogic.Models
{
    public class Response
    {
        public string UserId { get; set; }
        public string FormId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
