
using RestSharp;
using RestSharp.Authenticators;
using SchoolApp.Web.Code.Serialization;
using SchoolApp.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SchoolApp.Web.Code
{
    public class ExtensionAndCommonCode
    {
        public async Task<IRestResponse> SendEmail(MailSenderModel userEmailOptions)
        {

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", SiteKeys.MailgunAPIKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", SiteKeys.MailgunDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            //request.AddParameter("from", cloudConfig.EmailSenderDisplayName + " " + cloudConfig.EmailSenderAddress);
            request.AddParameter("from", SiteKeys.MailgunSender + "<" + SiteKeys.MailgunFrom + ">");
            request.AddParameter("to", userEmailOptions.ToEmails.First());
            request.AddParameter("subject", userEmailOptions.Subject);
            request.AddParameter("body", userEmailOptions.Body);
            request.AddParameter("html", userEmailOptions.Body);
            request.Method = Method.POST; 
            return await client.ExecuteAsync(request);
        }
        //public void SendEmail(MailSenderModel userEmailOptions)
        //{


        //}

    }
}