using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaConsoleApp.Models
{
    public class Main
    {
        [JsonProperty("temp", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Temp { get; set; }

        [JsonProperty("pressure", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Pressure { get; set; }

        [JsonProperty("humidity", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Humidity { get; set; }

        [JsonProperty("temp_min", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? TempMin { get; set; }

        [JsonProperty("temp_max", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? TempMax { get; set; }
    }
}
