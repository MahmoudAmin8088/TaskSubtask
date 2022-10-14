using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSubtask.core.IRepository
{
    public interface IBaseRepository<T>where T : class
    {
        ICollection<T> GetAll();
        T GetById(string id);
        T Create(T entity);
        T Delete(T entity);
        T Update(T entity);

    }
}
