using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorFormDesigner.Web.Models
{
    public class QuestionRequest
    {
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        [MaxLength(1000)]
        public string Description { get; set; }

        public string Type { get; set; }

        public bool IsCorrected { get; set; }

        public List<Option> Options { get; set; }
    }
}
