﻿using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public interface IUserTokenRepository
    {

        UserToken Get(string pDeToken);

    }
}
