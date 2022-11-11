using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSubtask.core.Models.ModelsDto
{
    public class TaskUpdateDto
    {
        [Required]
        public string? TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
