using JobFinder.Extensions;
using JobFinder.Factories;
using JobFinder.Infrastructure.Repositories;
using JobFinder.Models;
using JobFinder.Models.API.Responses;
using JobFinder.Models.Contexts;
using JobFinder.Singletons;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Controllers
{
    [ApiController]
    public class APIControllerBase : ControllerBase
    {
        protected APIResponse APIResponse;

        protected string GetToken()
        {
            return Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
        }

        protected Company GetCompany(string pDeToken)
        {
            UserToken userToken = RepositorySingleton.instance.UserTokenRepository.Get(pDeToken);

            if (userToken == null || userToken.IdCompany == null)
            {
                return null;
            }

            Company company = ((IBaseRepository<Company>)RepositorySingleton.instance.CompanyRepository).Get(userToken.IdCompany.ToIntZeroAble());

            if (company == null)
            {
                return null;
            }

            return company;
        }

        protected Customer GetCustomer(string pDeToken)
        {
            UserToken userToken = RepositorySingleton.instance.UserTokenRepository.Get(pDeToken);

            if (userToken == null || userToken.IdCustomer == null)
            {
                return null;
            }

            Customer customer = ((IBaseRepository<Customer>)RepositorySingleton.instance.CustomerRepository).Get(userToken.IdCustomer.ToIntZeroAble());

            if (customer == null)
            {
                return null;
            }

            return customer;
        }

    }
}
