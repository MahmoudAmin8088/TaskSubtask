using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSubtask.core.IRepository;
using TaskSubtask.core.UnitOfWork;
using TaskSubtask.Ef.Data;
using TaskSubtask.Ef.Repository;

namespace TaskSubtask.Ef.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ITaskRepository Tasks { get; private set; }

        public ISubtaskRepository Subtasks { get; private set; }

        public IPriorityRepository Prioritys { get; private set; }
        public IUserRepository Users { get; private set; }



        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Tasks = new TaskRepository(_context);
            Subtasks = new SubtaskRepository(_context);
            Prioritys = new PriorityRepository(_context);
            Users = new UserRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
