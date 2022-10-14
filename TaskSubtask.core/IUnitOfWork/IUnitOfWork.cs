using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSubtask.core.IRepository;

namespace TaskSubtask.core.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        ITaskRepository Tasks { get; }
        ISubtaskRepository Subtasks { get; }
        IPriorityRepository Prioritys { get; }
        IUserRepository Users { get; }

        int Complete();
    }
}
