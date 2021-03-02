using JobFinder.Constants;
using JobFinder.Extensions;
using JobFinder.Factories;
using JobFinder.Infrastructure.Repositories;
using JobFinder.Models;
using JobFinder.Models.API.Requests;
using JobFinder.Models.API.Responses;
using JobFinder.Singletons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Controllers
{
    [Route("api/[controller]")]
    public class JobController : APIControllerBase
    {

        [HttpGet("GetByCompany")]
        [Authorize]
        public ActionResult GetByCompany()
        {
            string deToken = this.GetToken();

            // Validate User by Token
            Company company = this.GetCompany(deToken);

            if (company != null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
                this.APIResponse.Data = RepositorySingleton.instance.JobRepository.GetJobsByCompany(company.IdUser).OrderByDescending(j => j.DtRegister).ToList();
                return Ok(this.APIResponse);
            }

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
            return Unauthorized(this.APIResponse);
        }

        [HttpGet("GetByRole/{pIdRole}")]
        [Authorize]
        public ActionResult<List<Job>> GetByRole(int pIdRole)
        {
            string deToken = this.GetToken();

            // Validate User By Token
            Customer customer = this.GetCustomer(deToken);

            if (customer != null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
                 List<Job> jobs = RepositorySingleton.instance.JobRepository.GetJobsByRole(pIdRole).Where(j => j.IsActive == true).OrderByDescending(j => j.DtRegister).ToList();

                foreach (Job job in jobs)
                {
                    job.Company.DePassword = null;
                }

                this.APIResponse.Data = jobs;

                return Ok(this.APIResponse);
            }

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
            return Unauthorized(this.APIResponse);
        }

        [HttpPut]
        [Authorize]
        public ActionResult Close(int pIdJob)
        {
            string deToken = this.GetToken();

            // Validate User by Token
            Company company = this.GetCompany(deToken);

            if (company != null)
            {
                Job job = ((IBaseRepository<Job>)RepositorySingleton.instance.JobRepository).Get(pIdJob);

                if (job == null || job.IdCompany != company.IdUser)
                    return BadRequest();

                RepositorySingleton.instance.JobRepository.Close(job);

                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
                return Ok(this.APIResponse);
            }
            else
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
                return Unauthorized(this.APIResponse);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateJob(JobAddRequest pJobAddRequest)
        {
            try
            {
                string deToken = this.GetToken();

                // Validate User by Token
                Company company = this.GetCompany(deToken);

                if (company == null)
                {
                    this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
                    return Unauthorized(this.APIResponse);
                }

                // Validate Request
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);
                if (string.IsNullOrEmpty(pJobAddRequest.DeTitle))
                {
                    this.APIResponse.AddError("DeTitle", ValidationMessageConstant.RequiredField);
                }
                if (string.IsNullOrEmpty(pJobAddRequest.DeDescription))
                {
                    this.APIResponse.AddError("DeDescription", ValidationMessageConstant.RequiredField);
                }
                if (pJobAddRequest.IdRole <= 0)
                {
                    this.APIResponse.AddError("IdRole", ValidationMessageConstant.RequiredField);
                }
                else
                {
                    Role role = ((IBaseRepository<Role>)RepositorySingleton.instance.RoleRepository).Get(pJobAddRequest.IdRole);
                    if (role == null)
                    {
                        this.APIResponse.AddError("IdRole", ValidationMessageConstant.InvalidValue);
                    }
                }

                if (this.APIResponse.Errors.Count() > 0)
                {
                    return BadRequest(this.APIResponse);
                }

                // Create New Job
                Job job = new Job {
                    IdCompany = company.IdUser,
                    DtRegister = DateTime.Now,
                    IsActive = true,
                    DeDescription = pJobAddRequest.DeDescription,
                    DeTitle = pJobAddRequest.DeTitle,
                    IdRole = pJobAddRequest.IdRole,
                    VlSalaryMin = pJobAddRequest.VlSalaryMin,
                    VlSalaryMax = pJobAddRequest.VlSalaryMax
                };

                // Insert in DB
                ((IBaseRepository<Job>)RepositorySingleton.instance.JobRepository).Add(job);
            }
            catch(Exception ex)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);
                this.APIResponse.AddError("Exception", $"Message: {ex.Message.ToStringOrEmpty()}");
                this.APIResponse.AddError("Exception", $"Stack Trace: {ex.StackTrace.ToStringOrEmpty()}");
                this.APIResponse.AddError("Exception", $"Inner Exception: {ex.InnerException.ToStringOrEmpty()}");

                return BadRequest(this.APIResponse);
            }

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            return Ok(this.APIResponse);
        }

    }
}
