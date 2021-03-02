using JobFinder.Enums;
using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public interface IUserBaseRepository
    {
        UserBase Get(string pDeEmail, string pDePassword, UserType pUserType);
        UserBase Get(string pDeEmail, UserType pUserType);
    }
}
