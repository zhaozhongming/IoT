using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Message
    {
        [JsonProperty(PropertyName = @"Location ID")]
        public int LocationID { get; set; }
        [JsonProperty(PropertyName = @"Node number")]
        public int NodeNumber { get; set; }
        public float Ch0 { get; set; }
        public float Ch1 { get; set; }
        public float Ch2 { get; set; }
        public float Ch3 { get; set; }
        public float Ch4 { get; set; }
        public float Ch5 { get; set; }
        public float Ch6 { get; set; }
        public float Ch7 { get; set; }
        public float Ch8 { get; set; }
        public float Ch9 { get; set; }
        public float Ch10 { get; set; }
        public float Ch11 { get; set; }
        public float Ch12 { get; set; }
        public float Ch13 { get; set; }
        public float Ch14 { get; set; }
        public float Ch15 { get; set; }
        [JsonProperty(PropertyName = @"UTC Timestamp")]
        public DateTime UTCTimestamp { get; set; }

    }
}
