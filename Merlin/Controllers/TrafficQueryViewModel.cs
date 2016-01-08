using System.ComponentModel.DataAnnotations;

namespace Merlin.Controllers
{
    public class TrafficQueryViewModel
    {
        [Display(Name = "Antall biler")]
        public float NumberOfCars { get; set; }
        [Display(Name = "Gjennomsnittlig fart")]
        public float AverageSpeed { get; set; }
        [Display(Name = "Målingspunkt")]
        public int MeasurePoint { get; set; }
    }
}