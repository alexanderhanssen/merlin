using Newtonsoft.Json;

namespace Merlin.Models
{
    public class TrafficMeasurement
    {
        [JsonProperty("measure_point_number")]
        public int MeasurePointNumber { get; set; }
        public string TimeStamp { get; set; }
        [JsonProperty("event_number")]
        public int EventNumber { get; set; }
        public float Speed { get; set; }
        public int Lane { get; set; }
    }
}