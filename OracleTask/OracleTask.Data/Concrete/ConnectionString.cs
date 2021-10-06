using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleTask.Data.Concrete
{
    public static class ConnectionString
    {
        private static string DataSource { get; } = @"Data Source = (DESCRIPTION =
       (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))
       (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = ORCL)));";
        private static string DefaultConnection { get; set; } = $"{DataSource}; User ID=oracletask; Password=oracletask12";

        public static string GetConnectionString()
        {
            return DefaultConnection;
        }
    }
}
