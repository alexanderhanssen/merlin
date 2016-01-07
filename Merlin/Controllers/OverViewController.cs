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
            long totalRows;
            using (var client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984", "bekk4"))
            {
                var toTime = DateTime.Now;
                var fromTime = toTime.Subtract(new TimeSpan(0, 0, 10));
                var query = new QueryViewRequest("test", "countall");
                totalRows = client.Views.QueryAsync(query).Result.Rows.Select(x => long.Parse(x.Value)).First();
            }
            var viewModel = new OverviewViewModel()
            {
                MeasurementPoints = new List<int>() {1500201, 1500202},
                TotalRows = totalRows
            };
            return View(viewModel);
        }
    }
}