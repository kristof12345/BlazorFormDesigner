using System;
using System.Collections.Generic;

namespace BlazorFormDesigner.BusinessLogic.Models
{
    public class Form
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int AvailableMinutes { get; set; }

        public FormStatus Status { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        public List<Question> Questions { get; set; }

        public double Points { get; set; }

        public double MaxPoints { get; set; }
    }
}
