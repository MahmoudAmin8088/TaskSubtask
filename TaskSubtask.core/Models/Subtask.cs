using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSubtask.core.Models
{
    public class Subtask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? SubTaskId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        public string? TaskId { get; set; }
        [ForeignKey("TaskId")]
        [Required]
        public Task? Task { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public string? PriorityId { get; set; }
        [ForeignKey("PriorityId")]

        public Priority? Priority { get; set; }
    }
}
