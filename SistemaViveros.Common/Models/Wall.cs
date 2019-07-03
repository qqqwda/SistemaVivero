using Newtonsoft.Json;
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
        public int WallId { get; set; }
        public String Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String Place { get; set; }

        [JsonIgnore]
        public virtual ICollection<Irrigation> Irrigations { get; set; }
        
        [NotMapped]
        public bool IsWatering { get; set; }

        [Required]
        public String UserId { get; set; }

        public override String ToString()
        {
            return this.Name;
        }



    }
}
