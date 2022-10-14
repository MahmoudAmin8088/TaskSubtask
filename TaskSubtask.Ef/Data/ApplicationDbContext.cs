using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSubtask.core.Models;
using Task = TaskSubtask.core.Models.Task;

namespace TaskSubtask.Ef.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Task>? Tasks { get; set; }
        public DbSet<Subtask>? SubTasks { get; set; }
        public DbSet<Priority>? Priorities { get; set; }
        public DbSet<ApplicationUser>  Users { get; set; }
        public DbSet<ApplicationUser> UserRoles { get; set; }

        

    }
}
