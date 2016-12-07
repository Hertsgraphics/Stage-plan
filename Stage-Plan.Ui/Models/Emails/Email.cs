using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace EsRaco.Ui.Models.Emails
{
    public class Email
    {
        private readonly string _from = "dave@lmsites.co.uk";
        private readonly string _host = "mail.lmsites.co.uk";

        #region Methods

        /// <summary>
        /// Send the email
        /// </summary>
        /// <param name="emailAddressTo">The email address you are sending to.</param>
        /// <param name="emailContent">The content to be inserted into the design.</param>
        /// <param name="attachment">A single attachment</param>
        public void SendEmail(string emailAddressTo, string emailContent, string subject, Attachment attachment = null)
        {
            if (String.IsNullOrEmpty(subject))
                subject = "Gracias";

            if (attachment == null)
            {
                new Thread(() =>
                {
                    Send(emailAddressTo, emailContent, subject, null);
                }).Start();
            }
            else
            {
                Send(emailAddressTo, emailContent, subject, attachment);
            }
        }

        /// <summary>
        /// Sends an email to faults@lmsites
        /// </summary>
        /// <param name="body"></param>
        public void SendEmailOnError(string body)
        {
            new Thread(() =>
            {
                try
                {
                    SmtpClient client = new SmtpClient();
                    NetworkCredential nc = new NetworkCredential(_from, GetPassword());

                    client.UseDefaultCredentials = false;
                    client.Credentials = nc;
                    client.EnableSsl = true;
                    client.Host = _host;

                    MailMessage message = new MailMessage();
                    message.Subject = "Error with lmsites";
                    message.IsBodyHtml = true;
                    message.Body = body;
                    message.To.Add("dave@lmsites.co.uk");
                    message.From = new MailAddress(_from);
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    //supress error message
                }
            }).Start();
        }

        private string GetPassword()
        {
            return "fu&jP75E37F4";
        }

        private void TryDifferentEmail(string content, Exception ex)
        {
            SmtpClient client = new SmtpClient("mail.lmsites.co.uk", 25);
            client.Credentials = new NetworkCredential("faults@lmsites.co.uk", "faultS123");

            MailMessage message = new MailMessage();
            message.Subject = "ERROR contacting Es Raco.";
            message.IsBodyHtml = true;
            message.Body = "<p>There was a fault sending an email</p><p>Content: " + content + "</p><p>Error:" + ex.ToString() + "</p>";
            message.To.Add(_from);
            message.From = new MailAddress("faults@lmsites.co.uk");

            client.Send(message);

        }//errorEmail£123


        private string GetEmailBody(string content, string subject)
        {
            string emailTemplate = Stage_Plan.Ui.Properties.Resources.EmailTemplate;

            string result = emailTemplate.Replace("@@@Content", content);
            result = result.Replace("@@@Heading", subject);

            return result;
        }

        private void Send(string emailAddressTo, string emailContent, string subject, Attachment attachment = null)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                NetworkCredential nc = new NetworkCredential(_from, GetPassword());
                client.UseDefaultCredentials = false;
                client.Credentials = nc;
                client.EnableSsl = false;
                client.Host = _host;


                MailMessage message = new MailMessage();
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = GetEmailBody(emailContent, subject);
                message.To.Add(emailAddressTo);
                message.Bcc.Add(_from);

                if (attachment != null)
                    message.Attachments.Add(attachment);

                message.From = new MailAddress(_from, "Es Raco");

                client.Send(message);
            }
            catch (Exception ex)
            {
                try
                {
                    Thread.Sleep(8000);
                    TryDifferentEmail(emailContent, ex);
                }
                catch
                {
                    //supress error message
                }
            }
        }

        #endregion

    }

}