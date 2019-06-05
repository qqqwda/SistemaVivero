using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaConsoleApp.Models
{
    public class Wind
    {
        [JsonProperty("speed", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Speed { get; set; }

        [JsonProperty("deg", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Deg { get; set; }
    }
}
