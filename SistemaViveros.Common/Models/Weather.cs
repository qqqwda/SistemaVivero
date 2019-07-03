using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaViveros.Common.Models
{
    public class Weather
    {
        [Key]
        public int WeatherId { get; set; }
        public String Description { get; set; }
        public double Temperature { get; set; }
        public long Pressure { get; set; }
        public long Humidity { get; set; }
        public int IrrigationId { get; set; }
        public String Icon { get; set; }
    }
}
