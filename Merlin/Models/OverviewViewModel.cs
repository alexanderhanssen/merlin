using System.Collections.Generic;

namespace Merlin.Models
{
    public class OverviewViewModel
    {
        public IEnumerable<object> MeasurementPoints { get; set; }
        public List<int> MinuteIntervals => new List<int> {1,5,15,60}; 
    }
}