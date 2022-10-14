using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSubtask.core.Models;
using Task = TaskSubtask.core.Models.Task;

namespace TaskSubtask.core.IRepository
{
    public interface ITaskRepository:IBaseRepository<Task>
    {
        ICollection<Task> GetUsersTasks(string userId);
        List<Task> GetAll(string? title, bool? isAse, int? pageindex, int? pagesize);

    }
}
