using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PoslovnaLogika
{
    public class PoslovnaLogikaConfig
    {
        [JsonProperty("pravila_odobrenja")]
        public PravilaOdobrenjaConfig PravilaOdobrenja { get; set; }
    }

    public class PravilaOdobrenjaConfig
    {
        [JsonProperty("limit_troska_za_odobrenje")]
        public decimal LimitTroskaZaOdobrenje { get; set; }
    }
}
