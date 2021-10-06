using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleTask.Data.Abstract
{
    public interface IGenericRepository<T>
    {
        ICollection<T> GetAll();
        bool Create(T entity);
        void Update(T entity);
        void Delete(int id);
        T GetById(int id);
    }
}
