using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using TaskSubtask.core.IRepository;
using TaskSubtask.core.Models;
using TaskSubtask.Ef.Data;

namespace TaskSubtask.Ef.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser>,IUserRepository
    {
        private readonly ApplicationDbContext _context;


        public UserRepository(ApplicationDbContext context ):base(context)
        {
            _context = context;
        }

        


       
    }
}
