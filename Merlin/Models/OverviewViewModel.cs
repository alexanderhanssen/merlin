using System.Collections.Generic;

namespace Merlin.Models
{
    public class OverviewViewModel
    {
        public IList<object> MeasurementPoints { get; set; }
        public long TotalRows { get; set; }
    }
}