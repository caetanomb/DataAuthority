using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAuthority.SqlServerEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DataAuthority.Base64Result.API.Controllers
{
    [Route("v1/diff")]
    public class ValuesController : Controller
    {
        private readonly DataAuthorityContext _dataAuthorityContext;

        public ValuesController(DataAuthorityContext dataAuthorityContext)
        {
            _dataAuthorityContext = dataAuthorityContext;
        }        

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var diffResult =
                await _dataAuthorityContext.PayLoads.SingleOrDefaultAsync(a => a.ProvidedPayLoadId == id &&
                                                                               a.Origin == "DiffResult");
            if (diffResult != null)
            {
                return Ok(JsonConvert.DeserializeObject(diffResult.Data));
            }

            return NotFound();
        }        
    }
}
