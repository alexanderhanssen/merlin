using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Merlin.Models
{
    public class OverviewViewModel
    {
        [Display(Name = "Målingspunkt")]
        public IEnumerable<object> MeasurementPoints { get; set; }
        [Display(Name = "Minutter siden")]
        public List<int> MinuteIntervals => new List<int> {1,5,15,60}; 
    }
}