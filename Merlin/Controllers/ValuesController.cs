using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MyCouch;
using MyCouch.Requests;

namespace Merlin.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<object> Get()
        {
            using (var client = new MyCouchClient("http://db-couchdb.cloudapp.net:5984", "bekk4"))
            {
                var query = new QueryViewRequest("test", "all").Configure(q => q.Reduce(false));
                var result = client.Views.QueryAsync(query).Result;
                return result.Rows.Select(row => row.Value);
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
