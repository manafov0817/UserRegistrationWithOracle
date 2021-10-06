using OracleTask.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleTask.Data.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {   
    public bool ExistById(int id);
    }
}
