using Microsoft.AspNetCore.Http;

namespace astute.Repository
{
    public partial interface IEmailSender
    {
        string ForgetPasswordBodyTemplate(string userEmail, string employeeName);
        void SendEmail(string toEmail = "", string externalLink = "", string subject = "", IFormFile formFile = null, string strBody = "");
    }
}
