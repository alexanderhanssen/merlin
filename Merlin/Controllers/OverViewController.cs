using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Merlin.Models;
using MyCouch;
using MyCouch.Requests;
using MyCouch.Responses;

namespace Merlin.Controllers
{
    public class OverviewController : Controller
    {
        // GET: OverView
        public async Task<ActionResult> Index()
        {
            var viewModel = new OverviewViewModel();
            using (var client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984", "bekk4"))
            {
                var query = new QueryViewRequest("test", "measurepoints").Configure(x => x.Group(true).Limit(1000));
                var result = await client.Views.QueryAsync(query);
                viewModel.MeasurementPoints = result.Rows.Select(x => x.Key);
            }
            return View(viewModel);
        }

        public async Task<ActionResult> GetResult(int measurePoint, int minutesAgo)
        {
            var viewModel = new TrafficQueryViewModel();
            using (var client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984", "bekk4"))
            {
                var startTime = DateTime.Now.Subtract(new TimeSpan(0, minutesAgo, 0)).ToString("O");
                var startKey = new object[] {measurePoint, startTime};
                var endKey = new object[] {measurePoint, DateTime.Now.ToString("O")};
                var carCountQuery = new QueryViewRequest("stats", "count").Configure(x => x.StartKey(startKey).EndKey(endKey));
                var carCountResult = await client.Views.QueryAsync(carCountQuery);
                var averageSpeedQuery = new QueryViewRequest("stats", "speed").Configure(x => x.StartKey(startKey).EndKey(endKey));
                var averageSpeedResult = await client.Views.QueryAsync(averageSpeedQuery);
                viewModel.MeasurePoint = measurePoint;
                var carCount = carCountResult.Rows.FirstOrDefault();
                if (carCount != null)
                    viewModel.NumberOfCars = int.Parse(carCount.Value);
                var averageSpeed = averageSpeedResult.Rows.FirstOrDefault();
                if (averageSpeed != null)
                {
                    viewModel.AverageSpeed = float.Parse(averageSpeed.Value, CultureInfo.InvariantCulture);
                }
            }

            return PartialView(viewModel);
        } 
    }
}