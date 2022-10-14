using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSubtask.core.IRepository;
using TaskSubtask.core.Models;
using TaskSubtask.Ef.Data;

namespace TaskSubtask.Ef.Repository
{
    public class SubtaskRepository : BaseRepository<Subtask>, ISubtaskRepository
    {

        private readonly ApplicationDbContext _context;

        public SubtaskRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public List<Subtask> GetAll(string? title, bool? isAse, int? pageindex, int? pagesize)
        {
            var result = base.GetAll().AsQueryable();

            if(title is not null)
                result= result.Where(s=>s.Title.Contains(title));
            if (isAse is true)
                result = result.OrderBy(s => s.Date).AsQueryable();
            else
                result = result.OrderByDescending(s => s.Date).AsQueryable();

            result = result.Skip(pageindex.Value * pagesize.Value).Take(pagesize.Value);

            return result.ToList();
        }

        public ICollection<Subtask> GetSubTasksOfTask(string taskId)
        {
            return _context.SubTasks.Include(s => s.Task).Where(s => s.TaskId == taskId).ToList();
        }

        public ICollection<Subtask> GetUsersSubTasks(string userId)
        {
            return _context.SubTasks.Include(s=>s.User).Where(s=>s.UserId == userId).ToList();  
        }
    }
}
