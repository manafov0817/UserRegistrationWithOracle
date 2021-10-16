using OracleTask.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OracleTask.Data.Abstract
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        Image GetByUserId(int id);
    }
}
