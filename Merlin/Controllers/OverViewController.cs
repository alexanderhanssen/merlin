using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Merlin.Models;
using MyCouch;
using MyCouch.Requests;

namespace Merlin.Controllers
{
    public class OverviewController : Controller
    {
        // GET: OverView
        public ActionResult Index()
        {
            IEnumerable<object> measurePoints;
            
            using (var client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984", "bekk4"))
            {
                var toTime = DateTime.Now;
                var fromTime = toTime.Subtract(new TimeSpan(0, 0, 10));
                var query = new QueryViewRequest("test", "measurepoints").Configure(x => x.Group(true).Limit(1000));
                measurePoints = client.Views.QueryAsync(query).Result.Rows.Select(x => x.Key);
            }
            var viewModel = new OverviewViewModel()
            {
                MeasurementPoints = measurePoints.ToList(),
            };
            return View(viewModel);
        }
    }
}