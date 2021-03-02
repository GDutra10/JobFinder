using JobFinder.Constants;
using JobFinder.Enums;
using JobFinder.Factories;
using JobFinder.Infrastructure.Repositories;
using JobFinder.Models;
using JobFinder.Models.API.Requests;
using JobFinder.Models.API.Responses;
using JobFinder.Singletons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Controllers
{
    [Route("api/[controller]")]
    public class SecurityController : APIControllerBase
    {

        // POST: api/Security/
        [HttpPost()]
        public ActionResult TryLogin(LoginRequest pLoginRequest)
        {
            var user = RepositorySingleton.instance.UserBaseRepository.Get(pLoginRequest.DeEmail, pLoginRequest.DePassword, (UserType)pLoginRequest.UserType);
            APIResponse apiResponse;

            if (user == null)
            {
                apiResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.BadRequest);
                apiResponse.AddError("validation", ValidationMessageConstant.LoginEmailOrPasswordWrong);

                return BadRequest(apiResponse);
            }
            else
            {
                string deToken = this.GenerateToken();
                
                user.DeToken = deToken;
                UserToken userToken = new UserToken
                {
                    DeToken = deToken,
                    DtExpire = DateTime.Now.AddHours(AppSettingsSingleton.instance.JWT_ExpiresHours),
                    IsActive = true
                };

                if ((UserType)pLoginRequest.UserType == UserType.Company)
                {
                    userToken.IdCompany = user.IdUser;
                }
                else if ((UserType)pLoginRequest.UserType == UserType.Customer)
                {
                    userToken.IdCustomer = user.IdUser;
                }

                ((IBaseRepository<UserToken>)RepositorySingleton.instance.UserTokenRepository).Add(userToken);

                // for security
                user.DePassword = null;
                user.UserType = (UserType)pLoginRequest.UserType;
            }

            this.APIResponse = APIResponseFactory.Create(System.Net.HttpStatusCode.OK);
            this.APIResponse.Data = user;

            return Ok(this.APIResponse);
        }

        private string GenerateToken()
        {
            var issuer = AppSettingsSingleton.instance.JWT_Issuer;
            var audience = AppSettingsSingleton.instance.JWT_Audience;
            var expiry = DateTime.Now.AddHours(AppSettingsSingleton.instance.JWT_ExpiresHours);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsSingleton.instance.JWT_Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: expiry, signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }
}
