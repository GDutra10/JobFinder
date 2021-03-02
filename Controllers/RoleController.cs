using JobFinder.Factories;
using JobFinder.Infrastructure.Repositories;
using JobFinder.Models;
using JobFinder.Models.API.Responses;
using JobFinder.Singletons;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : APIControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            this.APIResponse.Data = await ((IBaseRepository<Role>)RepositorySingleton.instance.RoleRepository).GetAllAsync();
            return Ok(this.APIResponse);
        }
    }
}
