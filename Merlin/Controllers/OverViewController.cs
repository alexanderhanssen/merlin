using System;
using System.Collections.Generic;
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
                var query = new QueryViewRequest("test", "count_cars").Configure(x => x.StartKey(new object [] {measurePoint, startTime}).EndKey(new object[] {measurePoint ,DateTime.Now.ToString("O")}));
                var result = await client.Views.QueryAsync(query);
                viewModel.MeasurePoint = measurePoint;
                var firstOrDefault = result.Rows.FirstOrDefault();
                if (firstOrDefault != null)
                    viewModel.NumberOfCars = int.Parse(firstOrDefault.Value);
            }

            return PartialView(viewModel);
        } 
    }
}