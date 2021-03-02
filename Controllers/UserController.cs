using JobFinder.Constants;
using JobFinder.Enums;
using JobFinder.Extensions;
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
    public class UserController : APIControllerBase
    {
        [HttpPost]
        public ActionResult Register(RegisterUpdateUserRequest pRegisterUpdateUserRequest)
        {
            if (pRegisterUpdateUserRequest == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);
                return BadRequest(this.APIResponse);
            }

            // Validations
            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);

            this.ValidateUserType(pRegisterUpdateUserRequest);
            this.ValidateNmUser(pRegisterUpdateUserRequest);
            this.ValidateDeEmail(pRegisterUpdateUserRequest);
            this.ValidateIdRole(pRegisterUpdateUserRequest);
            this.ValidateDePassword(pRegisterUpdateUserRequest);

            if (this.APIResponse.Errors.Count > 0)
            {
                return BadRequest(this.APIResponse);
            }

            // create new user
            if (pRegisterUpdateUserRequest.UserType == (int)UserType.Customer)
            {
                ((IBaseRepository<Customer>)RepositorySingleton.instance.CustomerRepository).Add(new Customer
                {
                    NmUser = pRegisterUpdateUserRequest.NmUser,
                    DeEmail = pRegisterUpdateUserRequest.DeEmail,
                    DePassword = pRegisterUpdateUserRequest.DePassword,
                    IdRole = pRegisterUpdateUserRequest.IdRole,
                    NuTelephone = pRegisterUpdateUserRequest.NuTelephone,
                    DtRegister = DateTime.Now
                });
            }
            else if (pRegisterUpdateUserRequest.UserType == (int)UserType.Company)
            {
                ((IBaseRepository<Company>)RepositorySingleton.instance.CompanyRepository).Add(new Company
                {
                    NmUser = pRegisterUpdateUserRequest.NmUser,
                    DeEmail = pRegisterUpdateUserRequest.DeEmail,
                    DePassword = pRegisterUpdateUserRequest.DePassword,
                    NuTelephone = pRegisterUpdateUserRequest.NuTelephone,
                    DtRegister = DateTime.Now
                });
            }

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            return Ok(this.APIResponse);
        }

        [HttpGet("Profile")]
        [Authorize]
        public ActionResult GetProfile()
        {
            string deToken = this.GetToken();

            // Validate User By Token
            Customer customer = this.GetCustomer(deToken);
            Company company = this.GetCompany(deToken);

            if (customer == null && company == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
                return Unauthorized(APIResponse);
            }

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);

            if (customer != null)
            {
                customer.DePassword = string.Empty;
                this.APIResponse.Data = customer;
            }
            else // company
            {
                company.DePassword = string.Empty;
                this.APIResponse.Data = company;
            }

            return Ok(this.APIResponse);
        }

        [HttpPut("Profile")]
        [Authorize]
        public ActionResult UpdateProfile(RegisterUpdateUserRequest pRegisterUpdateUserRequest)
        {
            string deToken = this.GetToken();

            // Validate User By Token
            Customer customer = this.GetCustomer(deToken);
            Company company = this.GetCompany(deToken);

            if (customer == null && company == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
                return Unauthorized(APIResponse);
            }

            // Validate Body Request
            if (pRegisterUpdateUserRequest == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);
                return BadRequest(this.APIResponse);
            }

            // Validations
            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);

            // set userType for validations
            pRegisterUpdateUserRequest.UserType = (customer == null) ? (int)UserType.Company : (int)UserType.Customer;

            this.ValidateNmUser(pRegisterUpdateUserRequest);
            this.ValidateDeEmail(pRegisterUpdateUserRequest, (customer == null) ? (UserBase)company : (UserBase)customer);
            this.ValidateIdRole(pRegisterUpdateUserRequest);
            //this.ValidateDePassword(pRegisterUpdateUserRequest);

            if (this.APIResponse.Errors.Count > 0)
            {
                return BadRequest(this.APIResponse);
            }

            // update user
            if (customer != null)
            {
                customer.NmUser = pRegisterUpdateUserRequest.NmUser;
                customer.DeEmail = pRegisterUpdateUserRequest.DeEmail;
                //customer.DePassword = pRegisterUpdateUserRequest.DePassword;
                customer.IdRole = pRegisterUpdateUserRequest.IdRole;
                customer.NuTelephone = pRegisterUpdateUserRequest.NuTelephone;

                ((IBaseRepository<Customer>)RepositorySingleton.instance.CustomerRepository).Update(customer);
            }
            else // company
            {
                company.NmUser = pRegisterUpdateUserRequest.NmUser;
                company.DeEmail = pRegisterUpdateUserRequest.DeEmail;
                //company.DePassword = pRegisterUpdateUserRequest.DePassword;
                company.NuTelephone = pRegisterUpdateUserRequest.NuTelephone;

                ((IBaseRepository<Company>)RepositorySingleton.instance.CompanyRepository).Update(company);
            }

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            return Ok(this.APIResponse);
        }

        [HttpPut("Password")]
        [Authorize]
        public ActionResult UpdatePassword(RegisterUpdateUserRequest pRegisterUpdateUserRequest)
        {

            string deToken = this.GetToken();

            // Validate User By Token
            Customer customer = this.GetCustomer(deToken);
            Company company = this.GetCompany(deToken);

            if (customer == null && company == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized);
                return Unauthorized(APIResponse);
            }

            // Validate Body Request
            if (pRegisterUpdateUserRequest == null)
            {
                this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);
                return BadRequest(this.APIResponse);
            }

            // Validations
            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);

            this.ValidateDePassword(pRegisterUpdateUserRequest);

            if (this.APIResponse.Errors.Count > 0)
            {
                return BadRequest(this.APIResponse);
            }

            // update user
            if (customer != null)
            {
                customer.DePassword = pRegisterUpdateUserRequest.DePassword;

                ((IBaseRepository<Customer>)RepositorySingleton.instance.CustomerRepository).Update(customer);
            }
            else // company
            {
                company.DePassword = pRegisterUpdateUserRequest.DePassword;

                ((IBaseRepository<Company>)RepositorySingleton.instance.CompanyRepository).Update(company);
            }


            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            return Ok(this.APIResponse);
        }

        private bool IsValidEmail(string deEmail)
        {
            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(deEmail);
                return mailAddress.Address == deEmail;
            }
            catch
            {
                return false;
            }
        }

        private void ValidateDePassword(RegisterUpdateUserRequest pRegisterUpdateUserRequest)
        {
            if (string.IsNullOrEmpty(pRegisterUpdateUserRequest.DePassword) ||
                string.IsNullOrEmpty(pRegisterUpdateUserRequest.DePasswordConfirm))
            {
                if (string.IsNullOrEmpty(pRegisterUpdateUserRequest.DePassword))
                    this.APIResponse.AddError("DePassword", ValidationMessageConstant.RequiredField);

                if (string.IsNullOrEmpty(pRegisterUpdateUserRequest.DePasswordConfirm))
                    this.APIResponse.AddError("DePasswordConfirm", ValidationMessageConstant.RequiredField);
            }
            else if (pRegisterUpdateUserRequest.DePassword.Count() < 6)
            {
                this.APIResponse.AddError("DePassword", ValidationMessageConstant.PasswordDoNotContainSixDigitsOrMore);
            }
            else if (pRegisterUpdateUserRequest.DePassword != pRegisterUpdateUserRequest.DePasswordConfirm)
            {
                this.APIResponse.AddError("DePassword", ValidationMessageConstant.PasswordDoNotMatch);
            }
        }

        private void ValidateIdRole(RegisterUpdateUserRequest pRegisterUpdateUserRequest)
        {
            if (pRegisterUpdateUserRequest.UserType == (int)UserType.Customer)
            {
                if (pRegisterUpdateUserRequest.IdRole.ToIntZeroAble() <= 0)
                {
                    this.APIResponse.AddError("IdRole", ValidationMessageConstant.RequiredField);
                }
                else if (((IBaseRepository<Role>)RepositorySingleton.instance.RoleRepository).Get(pRegisterUpdateUserRequest.IdRole) == null)
                {
                    this.APIResponse.AddError("IdRole", ValidationMessageConstant.InvalidValue);
                }
            }
        }

        private void ValidateDeEmail(RegisterUpdateUserRequest pRegisterUpdateUserRequest, UserBase pUserBase = null)
        {
            if (string.IsNullOrEmpty(pRegisterUpdateUserRequest.DeEmail))
            {
                this.APIResponse.AddError("DeEmail", ValidationMessageConstant.RequiredField);
            }
            else if (this.IsValidEmail(pRegisterUpdateUserRequest.DeEmail) == false)
            {
                this.APIResponse.AddError("DeEmail", ValidationMessageConstant.InvalidValue);
            }
            else
            {
                UserBase userBase = RepositorySingleton.instance.UserBaseRepository.Get(pRegisterUpdateUserRequest.DeEmail, (UserType)pRegisterUpdateUserRequest.UserType);

                // the same user and same email
                if (pUserBase != null && userBase != null && userBase.IdUser == pUserBase.IdUser
                    && userBase.DeEmail == pUserBase.DeEmail &&
                        (userBase is Customer && pUserBase is Customer || userBase is Company && pUserBase is Company))
                    return;

                if (userBase != null &&
                    ((userBase is Customer && pRegisterUpdateUserRequest.UserType == (int)UserType.Customer) ||
                     (userBase is Company && pRegisterUpdateUserRequest.UserType == (int)UserType.Company)))
                {
                    this.APIResponse.AddError("DeEmail", ValidationMessageConstant.EmailAlreadyInUse);
                }
            }
        }

        private void ValidateNmUser(RegisterUpdateUserRequest pRegisterUpdateUserRequest)
        {
            if (string.IsNullOrEmpty(pRegisterUpdateUserRequest.NmUser))
            {
                this.APIResponse.AddError("NmUser", ValidationMessageConstant.RequiredField);
            }
        }

        private void ValidateUserType(RegisterUpdateUserRequest pRegisterUpdateUserRequest)
        {
            if (pRegisterUpdateUserRequest.UserType.ToIntZeroAble() <= 0)
            {
                this.APIResponse.AddError("UserType", ValidationMessageConstant.RequiredField);
            }
            else if (pRegisterUpdateUserRequest.UserType != (int)UserType.Customer &&
                pRegisterUpdateUserRequest.UserType != (int)UserType.Company)
            {
                this.APIResponse.AddError("UserType", ValidationMessageConstant.InvalidValue);
            }
        }

    }
}
