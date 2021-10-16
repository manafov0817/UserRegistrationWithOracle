using System;
using System.Collections.Generic;
using System.Text;

namespace OracleTask.Entity.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MarkAs { get; set; }
        public int UserId { get; set; }
        public string UserUsername { get; set; }

    }
}
