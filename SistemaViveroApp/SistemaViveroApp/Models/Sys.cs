using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaViveroApp.Models
{
    public class Sys
    {
        [JsonProperty("type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Type { get; set; }

        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("message", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Message { get; set; }

        [JsonProperty("country", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("sunrise", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Sunrise { get; set; }

        [JsonProperty("sunset", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Sunset { get; set; }
    }
}
