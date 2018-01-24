using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace Stage_Plan.Bll
{
    public class Email
    {
        private readonly string _from = "support@stage-plan.com";
        private readonly string _host = "mail.stage-plan.com";

        #region Methods


        /// <summary>
        /// Send the email
        /// </summary>
        /// <param name="emailAddressTo">The list of email address you are sending to.</param>
        /// <param name="emailContent">The content to be inserted into the design.</param>
        /// <param name="attachment">A single attachment</param>
        public void SendEmailToDaveAndTelis(string emailContent, string subject, Attachment attachment = null)
        {
            new Thread(() =>
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
                message.To.Add(_from);

                if (attachment != null)
                    message.Attachments.Add(attachment);

                message.From = new MailAddress(_from, "Stageplan");

                client.Send(message);
            }).Start();
        }

        /// <summary>
        /// Send the email
        /// </summary>
        /// <param name="emailAddressTo">The email address you are sending to.</param>
        /// <param name="emailContent">The content to be inserted into the design.</param>
        /// <param name="attachment">A single attachment</param>
        public void SendEmail(string emailAddressTo, IEnumerable<string> cc, string emailContent, string subject, Attachment attachment = null)
        {
            if (String.IsNullOrEmpty(subject))
                subject = "Thank you";

            if (attachment == null)
            {
                new Thread(() =>
                {
                    Send(emailAddressTo, cc, emailContent, subject, null);
                }).Start();
            }
            else
            {
                Send(emailAddressTo, cc, emailContent, subject, attachment);
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
        
        public string GetPassword()
        {
            return "r765HfhE*";
        }

        private void TryDifferentEmail(string content, string email, Exception ex)
        {
            SmtpClient client = new SmtpClient("mail.lmsites.co.uk", 25);
            client.Credentials = new NetworkCredential("faults@lmsites.co.uk", "faultS123");

            MailMessage message = new MailMessage();
            message.Subject = "ERROR contacting Stageplan.";
            message.IsBodyHtml = true;
            message.Body = "<p>There was a fault sending an email</p><p>Content: " + content + "</p>Email Addres: " + email + "<p>Error:" + ex.ToString() + "</p>";
            message.To.Add(_from);
            message.From = new MailAddress("faults@lmsites.co.uk");

            client.Send(message);

        }//errorEmail£123


        private string GetEmailBody(string content, string subject)
        {
            string emailTemplate =  Stage_Plan.Bll.Properties.Resources.EmailTemplate;

            string result = emailTemplate.Replace("@@@Content", content);
            result = result.Replace("@@@Heading", subject);

            return result;
        }

        private void Send(string emailAddressTo, IEnumerable<string> cc, string emailContent, string subject, Attachment attachment = null)
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

                if (cc != null)
                {
                    foreach (var c in cc)
                    {
                        message.CC.Add(c);
                    }
                }
                if (attachment != null)
                    message.Attachments.Add(attachment);

                message.From = new MailAddress(_from, "Stageplan");

                client.Send(message);
            }
            catch (Exception ex)
            {
                try
                {
                    Thread.Sleep(8000);
                    TryDifferentEmail(emailContent, emailAddressTo, ex);
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