using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaViveros.Common.Models
{
    public class IrrigationWeather
    {

        public DateTime? Day { get; set; }
        public int? WallId { get; set; }
        public string Description { get; set; }
        public long? Humidity { get; set; }
        public string Icon { get; set; }
        public int? IrrigationId { get; set; }
        public int? WeatherId { get; set; }
        public double? Temperature { get; set; }
        public long? Pressure { get; set; }


    }
}
