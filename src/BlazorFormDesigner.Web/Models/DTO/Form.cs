using BlazorFormDesigner.Web.Models;
using System;
using System.Collections.Generic;

namespace BlazorFormDesigner.Web.Models
{
    public class Form
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsProtected { get; set; }

        public string Password { get; set; }

        public FormStatus Status { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        public List<Question> Questions { get; set; }

        public double Points { get; set; }

        public double MaxPoints { get; set; }
    }
}
