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
    public class PriorityRepository:BaseRepository<Priority>,IPriorityRepository
    {
        private readonly ApplicationDbContext _context;
        public PriorityRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
