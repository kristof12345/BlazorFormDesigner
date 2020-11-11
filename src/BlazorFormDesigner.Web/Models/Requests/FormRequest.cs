using System;
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

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        [Range(1, 100000)]
        public int AvailableMinutes { get; set; }
    }
}
