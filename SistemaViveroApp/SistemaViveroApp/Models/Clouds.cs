using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaViveroApp.Models
{
    public class Clouds
    {
        [JsonProperty("all", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? All { get; set; }
    }
}
