using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaViveros.Common.Models
{
    public class Wall
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String Place { get; set; }
        public List<Irrigation> Irrigations { get; set; }
        public int AppUserId { get; set; }

        [NotMapped]
        public bool IsWatering { get; set; }

        

    }
}
