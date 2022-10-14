using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSubtask.core.IRepository;
using TaskSubtask.core.Models;
using TaskSubtask.Ef.Data;
using Task = TaskSubtask.core.Models.Task;

namespace TaskSubtask.Ef.Repository
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context):base(context)
        {
                _context = context;
        }
        public List<Task> GetAll(string? title, bool? isAse, int? pageindex, int? pagesize)
        {
            var result = base.GetAll().AsQueryable();
            if (title is not null)
                result = result.Where(s => s.Title.Contains(title));
            if (isAse is true)
                result = result.OrderBy(s => s.Date).AsQueryable();
            else
                result = result.OrderByDescending(s => s.Date).AsQueryable();

            result=result.Skip(pageindex.Value * pagesize.Value).Take(pagesize.Value);

            return result.ToList();
            

            
        }

        public ICollection<Task> GetUsersTasks(string userId)
        {
            return _context.Tasks.Include(T=>T.User).Where(t => t.UserId == userId).ToList();
        }
    }
}
