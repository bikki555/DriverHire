using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services
{
    public interface IEmailServices
    {
        Task<bool> SendEmail(string to, string subject, string msg);
    }
    public class EmailServices:IEmailServices
    {
        private readonly IConfiguration _configuration;
        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmail(string to, string subject, string msg)
        {
            try
            {
                //this should be configured somewere in table settings// but now in appsettings// 
                var smtpServer = _configuration["EmailSettings:smtp"];
                var port = int.Parse(_configuration["EmailSettings:port"]);
                var userName= _configuration["EmailSettings:username"];
                var password= _configuration["EmailSettings:password"];
                var from= _configuration["EmailSettings:from"];
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Host = smtpServer;
                    smtpClient.Port = port;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = new NetworkCredential(userName,password);
                    smtpClient.EnableSsl = true;
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(from);
                    mailMessage.To.Add(to);
                    mailMessage.Subject = subject;
                    mailMessage.Body = msg;
                    mailMessage.IsBodyHtml = true;
                    await smtpClient.SendMailAsync(mailMessage);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }  
        }



    }
}
