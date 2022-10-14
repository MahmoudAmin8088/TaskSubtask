using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSubtask.core.Models;

namespace TaskSubtask.core.IRepository
{
        public interface IAuthRepository
        {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<string> AddAdminAsync(AddRoleModel model);
        Task<string> DeleteAdminAsync(DeleteRoleModel model);
        }
}
