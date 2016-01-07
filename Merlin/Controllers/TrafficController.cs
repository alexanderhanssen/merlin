using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async void Post([FromBody]IEnumerable<TrafficMeasurement> trafficMeasurements)
        {
            using (var client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984", "logg"))
            {
                using (var stream = Request.Content.ReadAsStreamAsync().Result)
                {
                    if (stream.CanSeek)
                    {
                        stream.Position = 0;
                    }

                    client.Documents.PostAsync(JsonConvert.SerializeObject(new
                    {
                        melding = "Mottatt",
                        tid = DateTime.Now,
                        data = Request.Content.ReadAsStringAsync().Result
                    })).Wait();
                }
              
            }

            using (var client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984","bekk4"))
            {
                //foreach (var trafficMeasurement in trafficMeasurements)
                //{
                //   await client.Documents.PostAsync(JsonConvert.SerializeObject(trafficMeasurement));
                //}
                Parallel.ForEach(trafficMeasurements,
                    trafficMeasurement =>
                        client.Documents.PostAsync(JsonConvert.SerializeObject(trafficMeasurement)).Wait());
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