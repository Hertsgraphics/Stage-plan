using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Stage_Plan.Ui.Models.Contact
{
    public abstract class AbstractContact
    {
        
        [Display(Name = "Your full name")]
        [Required]
        public string Name { get; set; }

        internal string GetContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<p>Hi " + this.Name + ", </p>");
            sb.Append("<p>Thank you for your email. We will get back to you within 24 hours.</p>");
            sb.Append("<p>Please find a copy of the email you sent us:</p>");
            sb.Append("<p>");
            
            if (!String.IsNullOrEmpty(this.Email))
                sb.Append("Email: "+ this.Email + "<br />");
            
            if (!String.IsNullOrEmpty(this.Message))
                sb.Append("Message: "+ this.Message.Replace(Environment.NewLine, "<br />")+ "<br />");

            sb.Append("</p>");

            return sb.ToString();
        }
        
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "The answer is 7!")]
        public string Verification { get; set; }
        
        public string Message { get; set; }

        public bool DidSend { get; internal set; }

        public abstract bool Send(string subject);


        //protected string Content { get; set; }
    }
}