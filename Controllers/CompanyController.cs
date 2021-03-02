using JobFinder.Factories;
using JobFinder.Infrastructure.Repositories;
using JobFinder.Models;
using JobFinder.Singletons;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : APIControllerBase
    {
        [HttpGet]
        public ActionResult Get(int pIdCompany)
        {
            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            this.APIResponse.Data = ((IBaseRepository<Company>)RepositorySingleton.instance.RoleRepository).Get(pIdCompany);
            return Ok(this.APIResponse);
        }
    }
}
