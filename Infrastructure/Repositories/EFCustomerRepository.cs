﻿using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public class EFCustomerRepository : EFBaseRepository<Customer>, ICustomerRepository
    {
    }
}
