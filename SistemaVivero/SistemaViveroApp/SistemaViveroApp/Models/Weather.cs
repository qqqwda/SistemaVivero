using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaViveroApp.Models
{
    public class Weather
    {
        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("main", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Main { get; set; }

        [JsonProperty("description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("icon", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }
    }
}
