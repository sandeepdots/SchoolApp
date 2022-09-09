using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SchoolApp.Web.Code.Serialization
{
    public static class SiteKeys
    {
        public static string MainDomain
        {
            get { return ConfigurationManager.AppSettings["Domain"]; }
        }
    }
}