using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Singletons
{
    public class AppSettingsSingleton
    {
        private static AppSettingsSingleton _instance;
        public static AppSettingsSingleton instance
        {
            get
            {
                return _instance;
            }
        }

        private static IConfiguration Configuration;

        public static void Instantiate(IConfiguration pConfiguration)
        {
            if (_instance == null)
            {
                _instance = new AppSettingsSingleton();
                Configuration = pConfiguration;
            }
        }


        public string GetConnectionString(string pConnectionStringName)
        {
            return Configuration.GetConnectionString(pConnectionStringName);
        }


        public string JWT_Issuer
        {
            get
            {
                return Configuration["Jwt:Issuer"].ToString();
            }
        }

        public string JWT_Audience
        {
            get
            {
                return Configuration["Jwt:Audience"].ToString();
            }
        }

        public string JWT_Key
        {
            get
            {
                return Configuration["Jwt:Key"].ToString();
            }
        }

        public int JWT_ExpiresHours
        {
            get
            {
                return Convert.ToInt32(Configuration["Jwt:ExpiresHours"]);
            }
        }

        public string ORMToUse
        {
            get
            {
                return Configuration["ORMToUse"].ToString();
            }
        }
    }
}
