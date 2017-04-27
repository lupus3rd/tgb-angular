using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Http;  
using Microsoft.AspNetCore.Mvc;  
using AngularDemo.Domain;  
using AngularDemo.DTO;  
using Microsoft.EntityFrameworkCore;  
  
namespace AngularDemo.Controllers  
{  
    [Produces("application/json")]
    [Route("api/BlogInfo")]
    public class BlogInfoController : Controller
    {
        // GET: api/BlogInfo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BlogInfo/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/BlogInfo
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/BlogInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
