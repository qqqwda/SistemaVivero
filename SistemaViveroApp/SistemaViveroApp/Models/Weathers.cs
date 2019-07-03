using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaViveroApp.Models
{
    public class Weathers
    {
        [JsonProperty("coord", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Coord Coord { get; set; }

        [JsonProperty("weather", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Weather> Weather { get; set; }

        [JsonProperty("base", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Base { get; set; }

        [JsonProperty("main", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Main Main { get; set; }

        [JsonProperty("visibility", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Visibility { get; set; }

        [JsonProperty("wind", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Wind Wind { get; set; }

        [JsonProperty("clouds", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Clouds Clouds { get; set; }

        [JsonProperty("dt", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Dt { get; set; }

        [JsonProperty("sys", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Sys Sys { get; set; }

        [JsonProperty("timezone", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Timezone { get; set; }

        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("cod", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Cod { get; set; }
    }
}
