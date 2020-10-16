using System;
using System.Collections.Generic;

namespace BlazorFormDesigner.BusinessLogic.Models
{
    public class Form
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsProtected { get; set; }

        public string Password { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        public List<Question> Questions { get; set; }
    }
}
