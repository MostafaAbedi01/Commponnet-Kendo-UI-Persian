using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLibrary
{
    public class CommonSendMail
    {
        public void Send(MailMessage mailMessage, string sendMailHost, bool sendMailEnableSsl, string sendMailUserName, string sendMailPassword,int port=587)
        {
            var smtpServer = new SmtpClient
            {
                Host = sendMailHost,
                Port = port,
                EnableSsl = sendMailEnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sendMailUserName, sendMailPassword),
            };
            mailMessage.From = new MailAddress(sendMailUserName);
            mailMessage.IsBodyHtml = true;
            mailMessage.SubjectEncoding = mailMessage.BodyEncoding = Encoding.UTF8;
            smtpServer.Send(mailMessage);
        }
    }
}
