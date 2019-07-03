using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaViveroApp.Models
{
    public class Coord
    {
        [JsonProperty("lon", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Lon { get; set; }

        [JsonProperty("lat", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Lat { get; set; }
    }
}
