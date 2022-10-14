using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSubtask.core.Models.ModelsDto
{
    public class TaskDto
    {
        public string? TaskId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string? UserId { get; set; }

    }
}
