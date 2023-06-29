using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System;
using System.Text;
using astute.CoreServices;

namespace astute.Repository
{
    public partial class EmailSender : IEmailSender
    {
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion

        #region Ctor
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Methods
        public string ForgetPasswordBodyTemplate(string userName, string employeeName)
        {
            var baseUrl = _configuration["FrontUrl"];
            var redirectUrl = baseUrl + "/reset-password?code=" + CoreService.Encrypt(userName);
            StringBuilder sb = new StringBuilder();
            sb.Append(@"Dear " + employeeName + ",");
            sb.Append(@"You have requested to change your password. You can do this through the link below or copy paste the url in browser if the link is not visible.,");
            sb.Append(@"'Change my password'");
            sb.Append(@"<a href='" + redirectUrl + "'> Click Here </a>");
            sb.Append(@"If you didn't request this, please ignore this email.");

            return sb.ToString();
        }
        public void SendEmail(string toEmail = "", string externalLink = "", string subject = "", IFormFile formFile = null, string strBody = "")
        {
            MailMessage mailMessage = new MailMessage(_configuration["EmailSetting:FromEmail"], toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = strBody;
            if (formFile != null && formFile.Length > 0)
            {
                string fileName = Path.GetFileName(formFile.FileName);
                mailMessage.Attachments.Add(new Attachment(formFile.OpenReadStream(), fileName));
            }
            mailMessage.IsBodyHtml = true;

            using (SmtpClient smptClient = new SmtpClient())
            {
                smptClient.Host = _configuration["EmailSetting:SmtpHost"];
                smptClient.EnableSsl = true;
                smptClient.UseDefaultCredentials = false;
                smptClient.Credentials = new NetworkCredential(_configuration["EmailSetting:FromEmail"], _configuration["EmailSetting:FromPassword"]);
                smptClient.Port = Convert.ToInt32(_configuration["EmailSetting:SmtpPort"]);
                smptClient.Send(mailMessage);
            }
        }
        #endregion
    } 
}
