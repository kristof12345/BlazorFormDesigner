using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorFormDesigner.Web.Models
{
    public class FormRequest
    {
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public List<QuestionRequest> Questions { get; set; }

        public bool IsProtected { get; set; }

        public string Password { get; set; }
    }
}
