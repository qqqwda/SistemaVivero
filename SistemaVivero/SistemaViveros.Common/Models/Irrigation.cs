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
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public Weather Weather { get; set; }
        public int WallId { get; set; }
    }
}
