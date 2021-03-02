using JobFinder.Constants;
using JobFinder.Factories;
using JobFinder.Infrastructure.Repositories;
using JobFinder.Models;
using JobFinder.Models.API.Requests;
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
    public class CandidateController : APIControllerBase
    {
        [HttpPost("{pIdJob}")]
        [Authorize]
        public ActionResult Post(int pIdJob)
        {
            string deToken = this.GetToken();

            // Validate User By Token
            Customer customer = this.GetCustomer(deToken);

            if (customer == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
                return Unauthorized(this.APIResponse);
            }

            // Validate if already is a candidate
            if (RepositorySingleton.instance.CandidateRepository.Get(customer.IdUser, pIdJob) != null)
            {
                this.APIResponse = Factories.APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);
                this.APIResponse.AddError("AlreadyRegister", ValidationMessageConstant.CadidateAlreadyRegisteredForTheJob);
                return BadRequest(this.APIResponse);
            }

            // Create candidate
            Candidate candidate = new Candidate
            {
                DtRegister = DateTime.Now,
                IdCustomer = customer.IdUser,
                IdJob = pIdJob,
                WasAccept = false,
                WasReject = false
            };

            ((IBaseRepository<Candidate>)RepositorySingleton.instance.CandidateRepository).Add(candidate);

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            return Ok(this.APIResponse);
        }

        [HttpGet("{pIdJob}")]
        [Authorize]
        public ActionResult Get(int pIdJob)
        {
            string deToken = this.GetToken();

            // Validate User by Token
            Company company = this.GetCompany(deToken);

            if (company == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
                return Unauthorized(this.APIResponse);
            }

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            this.APIResponse.Data = RepositorySingleton.instance.CandidateRepository.GetAll(pIdJob);
            
            // security password
            foreach (Candidate candidate in (List<Candidate>)this.APIResponse.Data)
            {
                candidate.Customer.DePassword = string.Empty;
            }

            return Ok(APIResponse);
        }

        [HttpPut("Reject")]
        [Authorize]
        public ActionResult Reject(AcceptRejectCandidateRequest pAcceptRejectCandidateRequest)
        {
            string deToken = this.GetToken();

            // Validate User by Token
            Company company = this.GetCompany(deToken);
            Candidate candidate = ((IBaseRepository<Candidate>)RepositorySingleton.instance.CandidateRepository).Get(pAcceptRejectCandidateRequest.IdCandidate);
            Job job = ((IBaseRepository<Job>)RepositorySingleton.instance.JobRepository).Get(pAcceptRejectCandidateRequest.IdJob);

            if (company == null || candidate == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
                return Unauthorized(this.APIResponse);
            }
            else
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);

                if (candidate.IdJob != job.IdJob)
                {
                    this.APIResponse.AddError("", string.Format(ValidationMessageConstant.CandidateIsNotFromThisJob, job.DeTitle));
                }
                else if (job.IdCompany != company.IdUser)
                {
                    this.APIResponse.AddError("", string.Format(ValidationMessageConstant.JobIsNotFromThisCompany, company.NmUser));
                }
                else if (job.IsActive == false)
                {
                    this.APIResponse.AddError("", ValidationMessageConstant.JobIsNotActive);
                }

                // Return Bad Request
                if (this.APIResponse.Errors.Count > 0)
                {
                    return BadRequest(this.APIResponse);
                }
            }

            candidate.WasReject = true;
            ((IBaseRepository<Candidate>)RepositorySingleton.instance.CandidateRepository).Update(candidate);

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            return Ok(this.APIResponse);
        }

        [HttpPut("Accept")]
        [Authorize]
        public ActionResult Accept(AcceptRejectCandidateRequest pAcceptRejectCandidateRequest)
        {
            string deToken = this.GetToken();

            // Validate User by Token
            Company company = this.GetCompany(deToken);
            Candidate candidate = ((IBaseRepository<Candidate>)RepositorySingleton.instance.CandidateRepository).Get(pAcceptRejectCandidateRequest.IdCandidate);
            Job job = ((IBaseRepository<Job>)RepositorySingleton.instance.JobRepository).Get(pAcceptRejectCandidateRequest.IdJob);

            if (company == null || candidate == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
                return Unauthorized(this.APIResponse);
            }
            else
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);

                if (candidate.IdJob != job.IdJob)
                {
                    this.APIResponse.AddError("", string.Format(ValidationMessageConstant.CandidateIsNotFromThisJob, job.DeTitle));
                }
                else if (job.IdCompany != company.IdUser)
                {
                    this.APIResponse.AddError("", string.Format(ValidationMessageConstant.JobIsNotFromThisCompany, company.NmUser));
                }
                else if (job.IsActive == false)
                {
                    this.APIResponse.AddError("", ValidationMessageConstant.JobIsNotActive);
                }

                // Return Bad Request
                if (this.APIResponse.Errors.Count > 0)
                {
                    return BadRequest(this.APIResponse);
                }
            }
            

            candidate.WasAccept = true;
            job.IsActive = false;

            ((IBaseRepository<Candidate>)RepositorySingleton.instance.CandidateRepository).Update(candidate);
            ((IBaseRepository<Job>)RepositorySingleton.instance.JobRepository).Update(job);

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            return Ok(this.APIResponse);
        }

    }
}
