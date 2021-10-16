using OracleTask.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OracleTask.Data.Abstract
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Location GetByUserId(int id);
    }
}
