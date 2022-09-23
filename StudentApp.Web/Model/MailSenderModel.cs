using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Web.Model
{
    public class MailSenderModel
    {
        public MailSenderModel()
        {
            ToEmails = new List<string>();
        }
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}