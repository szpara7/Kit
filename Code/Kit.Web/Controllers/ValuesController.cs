﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kit.Data.Tools.Dispatcher;
using Kit.Web.Events;
using Microsoft.AspNetCore.Mvc;

namespace Kit.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Dispatcher.Dispatch(new CounterEvent());
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get([FromQuery]int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete([FromQuery]int id)
        {
        }
    }
}
