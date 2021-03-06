﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Merlin.Models;
using MyCouch;
using Newtonsoft.Json;

namespace Merlin.Controllers
{
    public class TrafficCollection
    {
        private MyCouchClient _client;

        public TrafficCollection()
        {
            _client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984", "bekk4");
        }

        public void Add(IList<TrafficMeasurement> trafficMeasurements)
        {
            foreach (var trafficMeasurement in trafficMeasurements)
            {
                try
                {
                    var result = _client.Documents.PostAsync(JsonConvert.SerializeObject(trafficMeasurement)).Result;
                    if(!result.IsSuccess) throw new Exception();
                }
                catch (Exception)
                {
                    _client.Dispose();
                    _client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984", "bekk4");
                }
            }
        }
    }

    public class TrafficController : ApiController
    {
        public static TrafficCollection TrafficCollection;

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
        public void Post([FromBody]IList<TrafficMeasurement> trafficMeasurements)
        {
            TrafficCollection.Add(trafficMeasurements);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        private MyCouchClient Save()
        {
            return new MyCouchClient("http://db-couchdb.cloudapp.net:5984", "bekk4");
        }
    }
}