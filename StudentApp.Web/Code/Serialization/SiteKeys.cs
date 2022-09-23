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
        public static string MailgunDomain
        {
            get { return ConfigurationManager.AppSettings["MailgunDomain"]; }
        }
        public static string MailgunAPIKey
        {
            get { return ConfigurationManager.AppSettings["MailgunAPIKey"]; }
        }
        public static string MailgunSender
        {
            get { return ConfigurationManager.AppSettings["MailgunSender"]; }
        }
        public static string MailgunFrom
        {
            get { return ConfigurationManager.AppSettings["MailgunFrom"]; }
        }
    }
}