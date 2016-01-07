using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Merlin.Models;
using MyCouch;
using Newtonsoft.Json;

namespace Merlin.Controllers
{
    public class TrafficController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]IEnumerable<TrafficMeasurement> trafficMeasurements)
        {
            using (var client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984","bekk4"))
            {
                foreach (var trafficMeasurement in trafficMeasurements)
                {
                   client.Documents.PostAsync(JsonConvert.SerializeObject(trafficMeasurement));
                }
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}