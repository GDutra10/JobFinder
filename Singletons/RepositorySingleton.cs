using JobFinder;
using JobFinder.Extensions;
using JobFinder.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Singletons
{
    public class RepositorySingleton
    {
        private static RepositorySingleton _instance;
        public static RepositorySingleton instance
        {
            get
            {
                return _instance;
            }
        }


        public static void Instantiate(
            IUserBaseRepository pIUserBaseRepository,
            IJobRepository pJobRepository,
            ICompanyRepository pCompanyRepository,
            IUserTokenRepository pUserTokenRepository,
            IRoleRepository pRoleRepository,
            ICustomerRepository pCustomerRepository,
            ICandidateRepository pCandidateRepository)
        {
            if (_instance != null)
            {
                return;
            }

            _instance = new RepositorySingleton();

            pIUserBaseRepository.IsNullThrowException();
            _userBaseRepository = pIUserBaseRepository;

            pJobRepository.IsNullThrowException();
            _jobRespository = pJobRepository;

            pCompanyRepository.IsNullThrowException();
            _companyRepository = pCompanyRepository;

            pUserTokenRepository.IsNullThrowException();
            _userTokenRepository = pUserTokenRepository;

            pRoleRepository.IsNullThrowException();
            _roleRepository = pRoleRepository;

            pCustomerRepository.IsNullThrowException();
            _customerRepository = pCustomerRepository;

            pCandidateRepository.IsNullThrowException();
            _candidateRepository = pCandidateRepository;
        }

        private static IUserBaseRepository _userBaseRepository;
        public IUserBaseRepository UserBaseRepository => _userBaseRepository;

        private static IJobRepository _jobRespository;
        public IJobRepository JobRepository => _jobRespository;

        private static ICompanyRepository _companyRepository;
        public ICompanyRepository CompanyRepository => _companyRepository;

        private static IUserTokenRepository _userTokenRepository;
        public IUserTokenRepository UserTokenRepository => _userTokenRepository;

        private static IRoleRepository _roleRepository;
        public IRoleRepository RoleRepository => _roleRepository;

        private static ICustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository => _customerRepository;

        private static ICandidateRepository _candidateRepository;
        public ICandidateRepository CandidateRepository => _candidateRepository;
    }
}
