using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSubtask.core.Models
{
    public class DeleteRoleModel
    {
        [Required]
        public string UserId { get; set; }
        public string Role = "Admin";
    }
}
