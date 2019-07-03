using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaViveros.Common.Models
{
    public class Irrigation
    {
        [Key]
        public int IrrigationId { get; set; }
        
        public DateTime Day { get; set; }

        [JsonIgnore]
        public virtual Weather Weather { get; set; }
        public int WallId { get; set; }
    }
}
