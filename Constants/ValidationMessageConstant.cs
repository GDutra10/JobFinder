using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Constants
{
    public static class ValidationMessageConstant
    {
        public const string CadidateAlreadyRegisteredForTheJob = "You are already a candidate for this job";
        public const string CandidateIsNotFromThisJob = "Candidate is not from this job '{0}'";
        public const string EmailAlreadyInUse = "Email is already in use";
        public const string InvalidValue = "Invalid value";
        public const string JobIsNotFromThisCompany = "Job is not from the company '{0}'";
        public const string JobIsNotActive = "Job is not active";
        public const string LoginEmailOrPasswordWrong = "Invalid E-mail or/and Password";
        public const string PasswordDoNotContainSixDigitsOrMore = "Password do not contain six digits or more";
        public const string PasswordDoNotMatch = "Passwords do not match";
        public const string RequiredField = "Field is required";
    }
}
