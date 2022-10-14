using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSubtask.core.Models;

namespace TaskSubtask.core.IRepository
{
    public interface ISubtaskRepository:IBaseRepository<Subtask>
    {
        ICollection<Subtask> GetUsersSubTasks(string userId);
        ICollection<Subtask> GetSubTasksOfTask(string taskId);
        List<Subtask> GetAll(string? title, bool? isAse, int? pageindex, int? pagesize);

    }
}
